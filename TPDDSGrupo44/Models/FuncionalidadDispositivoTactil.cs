namespace TPDDSGrupo44.Models
{
    public class FuncionalidadDispositivoTactil
    {

        public int id { get; set; }
        public string nombre { get; set; }

        public FuncionalidadDispositivoTactil() { }

        public FuncionalidadDispositivoTactil(string nombreFuncionalidad)
        {
            nombre = nombreFuncionalidad;
        }


    }
}