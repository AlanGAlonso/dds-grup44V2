using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Models

{
    public class GetJsonCGP
    {

        public List<CGP> getJsonData()
        {
            //string url = "http://trimatek.org/Consultas/centro?zona=" + zona;
            //try
            //{
                string url = "http://trimatek.org/Consultas/centro";

                var jsonString = string.Empty;

                var client = new WebClient();
                jsonString = client.DownloadString(url);

                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                List<JsonCGP> listCGP = (List<JsonCGP>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonCGP>));
                List<CGP> cgps = new List<CGP>();
                foreach (JsonCGP cgp in listCGP)
                {
                    //CGP nuevoCGP = new CGP(cgp.comuna, cgp.serviciosJSON);

                    //servicio = new Models.Servicio(cgp.servicios.nombre);
                    //servicio.horarioAbierto.Add(new Models.HorarioAbierto(System.DayOfWeek.(Enum.GetName(typeof(DayOfWeek), cgp.diaSemana)), cgp.horaDesde, cgp.horaHasta));
                    //serviciosJSON.Add(servicio);
                    //cgps.Add(nuevoCGP);
                }
                return cgps;
            //}catch {
            //string url = http://demo3537367.mockable.io/trash/pois_bad;
            //     var jsonString = string.Empty;

            //    var client = new WebClient();
            //    jsonString = client.DownloadString(url);

            //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //    ErrorJson errorjson = javaScriptSerializer.Deserialize(jsonString, typeof(ErrorJson));
            //    return errorjson.status_code + errorjson.error;
            //}
        }

    }
}






