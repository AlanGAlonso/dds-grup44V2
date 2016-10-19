using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace TPDDSGrupo44.DataModels
{
    public class ErrorJson
   {
        public int status_code { get; set; }
        public string error { get; set; }
    }
}