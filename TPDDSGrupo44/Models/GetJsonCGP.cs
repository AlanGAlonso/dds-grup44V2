using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace TPDDSGrupo44.Models

{
    public class GetJsonCGP
    {
        public List<CGP> getJsonData()
        {
            //string url = "http://trimatek.org/Consultas/centro?zona=" + zona;
            string url = "http://trimatek.org/Consultas/centro";

            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<JsonCGP> listCGP = (List<JsonCGP>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonCGP>));
            List<CGP> cgps = new List<CGP>();
            foreach (JsonCGP cgp in listCGP)
            {
                CGP nuevoCGP = new CGP(cgp.comuna, cgp.zonas, cgp.director, cgp.domicilio, cgp.telefono, cgp.serviciosJSON);
                //estos servicios tienen dentro objetos 
                //"servicios":[{"nombre":"Atención ciudadana","horarios":[{"diaSemana":1,"horaDesde":9,"minutosDesde":0,"horaHasta":15,"minutosHasta":0}]}]}]
                cgps.Add(nuevoCGP);
            }
            return cgps;
        }
    }
}






