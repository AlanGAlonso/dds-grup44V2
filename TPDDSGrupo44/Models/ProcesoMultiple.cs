using System.Collections.Generic;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class ProcesoMultiple : ActualizacionAsincronica
    {
        public Dictionary<ActualizacionAsincronica, string> actualizaciones { get; set; }

        public ProcesoMultiple () {
              actualizaciones = new Dictionary<ActualizacionAsincronica, string>();
        }

        public override void actualizar() {


            using (var db = new BuscAR())
            {
                LogAction log = new LogAction("Proceso Múltiple Asinc", BaseViewModel.usuario.nombre);
                
                try
                {
                    foreach (KeyValuePair<ActualizacionAsincronica, string> a in actualizaciones)
                    {
                        if (a.Value == "" || a.Value == null)
                        {
                            a.Key.actualizar();
                        }
                        else
                        {
                            a.Key.actualizar(a.Value);
                        }
                    }

                }
                catch
                {
                    log.finalizarProceso("Error", "Hubo un problema inesperado en la ejecución del proceso, y el mismo no se pudo completar.");
                    db.LogProcesosAsincronicos.Add(log);
                    db.SaveChanges();
                }
                log.finalizarProceso("Exito");
                db.LogProcesosAsincronicos.Add(log);

                db.SaveChanges();

            }

        }
    }
}
