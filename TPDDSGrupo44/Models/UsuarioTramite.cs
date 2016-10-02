using System;

namespace TPDDSGrupo44.Models
{
    public class UsuarioTramite
    {
        ////////////////Atributos////////////////
        public int dni { get; set; }
        public string contrasenia { get; set; }
        public Rol rolUsuario { get; set; }


        public Boolean existeAcciones(int idAccion)
        {
            Boolean exist = false;
            foreach (var accion in this.rolUsuario.funcionalidades)
            {
                if (accion.id == idAccion)
                {
                    exist = true;
                }
            }

            return exist;

        }
    }
}
