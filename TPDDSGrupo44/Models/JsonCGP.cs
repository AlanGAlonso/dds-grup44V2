using System.Collections.Generic;

namespace TPDDSGrupo44.Models
{
    public class JsonCGP
	{
        public int comuna { get; set; }
        public string zonas { get; set; }
        public string director { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }
        public List<string> servicios { get; set; }
        // nroDiaDeLaSemana - horarioDesde - minutosDesde - horarioHasta - minutosHasta 

        //public List<object> servicios { get; set; }

    }
}