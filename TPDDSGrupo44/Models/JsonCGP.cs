using System.Collections.Generic;

namespace TPDDSGrupo44.Models
{
    public class JsonCGP
	{
        public int comuna { get; set; }
        public List<ServicioCGP> serviciosJSON { get; set; }

        public object servicios { get; set; }
                   
    }
}
