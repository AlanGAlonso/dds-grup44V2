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
    }
}
