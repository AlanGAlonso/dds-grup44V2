using System;

namespace TPDDSGrupo44.Models
{
    public class LogAction
    {
        public int id { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string procesoEjecutado { get; set; }
        public string nombreUsuario { get; set; }
        public string result { get; set; }
        public string mensajeDeError { get; set; }


    }
}