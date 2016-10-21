using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPDDSGrupo44.DataModels
{
    public class Configuracion 
    {
        public int id { get; set; }
        public int duracionMaximaBusquedas { get; set; }

        public Configuracion ()
        {

        }
    }
}