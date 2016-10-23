using System.Collections.Generic;

namespace TPDDSGrupo44.Models
{
    public class ProcesoMultiple : ActualizacionAsincronica
    {
        public Dictionary<ActualizacionAsincronica, string> actualizaciones { get; set; }

        public ProcesoMultiple () {
              actualizaciones = new Dictionary<ActualizacionAsincronica, string>();
        }

        public override void actualizar() {

            foreach (KeyValuePair<ActualizacionAsincronica, string> a in actualizaciones)
            {
                if (a.Value == "" || a.Value == null) { 
                    a.Key.actualizar();
                }
                else
                {
                    a.Key.actualizar(a.Value);
                }
            }

        }
    }
}