using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPDDSGrupo44.DataModels
{
    public class HorariosJSON
    {
        public int diaSemana { get; set; }
        public int horaDesde { get; set; }
        public int minutosDesde { get; set; }
        public int horaHasta { get; set; }
        public int minutosHasta { get; set; }
    }
}