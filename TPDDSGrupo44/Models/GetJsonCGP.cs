using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TPDDSGrupo44.Models

{
    public class GetJsonCGP
    {
        public List<JsonCGP> getJsonData()
        {

            //string url = "http://trimatek.org/Consultas/centro?zona=" + zona;
            string url = "http://trimatek.org/Consultas/centro";

            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<JsonCGP> listCGP = (List<JsonCGP>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonCGP>));


            List<CGP> cgps = new List<CGP>();
            //foreach(JsonCGP cgp in listCGP)
            //{
            //    CGP nuevoCGP = new CGP(cgp.comuna, cgp.director, cgp.domicilio, cgp.servicios, cgp.telefono, cgp.zonas);
            //    cgps.Add(nuevoCGP);
            //}
            //return cgps;

            return listCGP;
        
        }

        

        
    }
}






