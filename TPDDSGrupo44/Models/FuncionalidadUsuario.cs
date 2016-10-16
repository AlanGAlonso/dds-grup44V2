using System;

namespace TPDDSGrupo44.Models
{
    public class FuncionalidadUsuario
    {
       
        public int id { get; set; }
        public string nombre { get; set; }

        public FuncionalidadUsuario () { }

        public FuncionalidadUsuario (string nombreFuncionalidad)
        {
            nombre = nombreFuncionalidad;
        }

        public virtual void cargarPOI() { }

        public virtual void realizarTramite() { }
        
    }
}