using System.Collections.Generic;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.DataModels
{
    public class JsonCGP
	{
        public int comuna { get; set; }
        //public List<ServicioCGP> serviciosJSON { get; set; }
        //public object servicios { get; set; }
        public List<ServiciosJSON> serviciosJson { get; set; }

        public class ServiciosJSON {
            public string nombre { get; set; }
            public List<HorariosJSON> horariosJson { get; set; }
        }

        public class HorariosJSON {
            public int diaSemana { get; set; }
            public int horaDesde { get; set; }
            public int horaHasta { get; set; }
        }

    }
}
