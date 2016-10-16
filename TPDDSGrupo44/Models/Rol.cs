using System.Collections.Generic;

namespace TPDDSGrupo44.Models
{
    public class Rol
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public virtual List<FuncionalidadUsuario> funcionalidades { get; set; }

        public Rol (){ }

        public Rol (string nombreRol, List<FuncionalidadUsuario> funcionalidadesRol)
        {
            nombre = nombreRol;
            funcionalidades = funcionalidadesRol;
        }

    }
}