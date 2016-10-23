using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPDDSGrupo44.DataModels
{
    public class ServiciosJSON
    {
        public string nombre { get; set; }
        public List<HorariosJSON> horarios { get; set; }

    }
}