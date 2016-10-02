using System.Collections.Generic;

namespace TPDDSGrupo44.Models
{
    public class ProcesoMultiple : ActualizacionAsincronica
    {
        public List<ActualizacionAsincronica> actualizaciones { get; set; }

        public ProcesoMultiple () { }

        public override void actualizar() { }
    }
}