using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public int dni { get; set; }
        public byte[] contrasenia { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public virtual Rol rol { get; set; }

        public Usuario ()
        {

        }

        // CONSTRUCTOR
        public Usuario (int documento, string usuario, string contrasena, string mail)
        {
            //datos básicos
            dni = documento;
            nombre = usuario;
            email = mail;

            //encriptación del password
            contrasenia = encriptar(contrasena);

            //rol por defecto
            using (var db = new BuscAR())
            {
                rol = db.Roles.Where(r => r.nombre == "Terminal").Single();
            }

        }


        //REGISTRARSE
        public static bool registrarse(string nombre, string password, string password2, string email, int dni)
        {
            using (var db = new BuscAR())
            {
                List<Usuario> usuarios = db.Usuarios.Where(u=> u.nombre == nombre).ToList();
                if (usuarios.Count() != 0) return false;

                usuarios = db.Usuarios.Where(u => u.dni == dni).ToList();
                if (usuarios.Count() != 0) return false;

                usuarios = db.Usuarios.Where(u => u.email == email).ToList();
                if (usuarios.Count() != 0) return false;

                if (password != password2) return false;

                Usuario usuario = new Usuario(dni, nombre, password, email);
                db.Usuarios.Add(usuario);

                db.SaveChanges();
            }

            return true;
        }


        //LOGIN
        public static bool autenticarse(string usuario, string contrasena) {
            using (var db = new BuscAR())
            {
                Usuario user = db.Usuarios.Include("rol").Include("rol.funcionalidades").Where(u => u.nombre == usuario).Single();
                if (user.Equals(null))
                {
                    return false;
                }
                
                byte[] pass = Usuario.encriptar(contrasena);
                if (pass.SequenceEqual(user.contrasenia))
                {
                    BaseViewModel.usuario = user;
                    return true;
                }
                return false;
            }

        }


        //ACTUALIZAR DATOS
        public bool actualizarDatos(string nombre, string passwordActual, string passwordNueva, string passwordNueva2, int dni, string email)
        {
            using (var db = new BuscAR())
            {
                List<Usuario> usuarios = db.Usuarios.Where(u => u.nombre == nombre).ToList();
                foreach (Usuario u in usuarios)
                {
                    if (u != null && u.id != id)
                    {
                        return false;
                    }
                }

                Usuario usuario = db.Usuarios.Find(id);
                usuario.email = email;
                usuario.dni = dni;
                usuario.nombre = nombre;

                this.email = email;
                this.dni = dni;
                this.nombre = nombre;

                if (passwordActual != "" && passwordNueva != "")
                {
                   
                    byte[] pass = encriptar(passwordActual);
                    if (pass.SequenceEqual(this.contrasenia) && passwordNueva == passwordNueva2)
                    {
                        this.contrasenia = pass;
                        usuario.contrasenia = pass;
                    }
                    else
                    {
                        return false;
                    }
                }

                db.SaveChanges();
            }

            return true;
        }


        public static void salir()
        {
            BaseViewModel.usuario = null;
        }

        public static byte[] encriptar(string texto)
        {
            //encriptación del password
            var provider = new SHA256CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            return provider.ComputeHash(encoding.GetBytes(texto));
        }
    }
}