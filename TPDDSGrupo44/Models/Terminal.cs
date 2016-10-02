using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace TPDDSGrupo44.Models
{
    public class Terminal
    {
        public new int id { get; set; }
        public new DbGeography coordenada { get; set; }
        public new string nombre { get; set; }
        public new Boolean activa { get; set; }
    }
}