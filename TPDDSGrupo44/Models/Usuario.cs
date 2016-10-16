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
        public virtual Rol rol { get; set; }

        public Usuario ()
        {

        }

        // CONSTRUCTOR
        public Usuario (int documento, string usuario, string contrasena)
        {
            //datos básicos
            dni = documento;
            nombre = usuario;

            //encriptación del password
            var provider = new SHA256CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            contrasenia = provider.ComputeHash(encoding.GetBytes(contrasena));

            //rol por defecto
            using (var db = new BuscAR())
            {
                rol = db.Roles.Where(r => r.nombre == "Transeunte").Single();
            }

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

                var provider = new SHA256CryptoServiceProvider();
                var encoding = new UnicodeEncoding();
                byte[] pass = provider.ComputeHash(encoding.GetBytes(contrasena));
                if (pass.SequenceEqual(user.contrasenia))
                {
                    BaseViewModel.usuario = user;
                    return true;
                }
                return false;
            }

        }
    }
}