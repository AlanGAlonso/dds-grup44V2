using System;

namespace TPDDSGrupo44.DataModels
{
    public class LogAction
    {
        public int id { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string procesoEjecutado { get; set; }
        public string nombreUsuario { get; set; }
        public string resultado { get; set; }
        public string mensajeDeError { get; set; }

        public LogAction() { }

        public LogAction(string proceso, string usuario) {
            procesoEjecutado = proceso;
            nombreUsuario = usuario;
            fechaInicio = DateTime.Now;
        }

        public void finalizarProceso(string resultado)
        {
            this.resultado = resultado;
            fechaFin = DateTime.Now;
        }

        public void finalizarProceso(string resultado, string mensaje) {
            this.resultado = resultado;
            mensajeDeError = mensaje;
            fechaFin = DateTime.Now;
        }
    }
}