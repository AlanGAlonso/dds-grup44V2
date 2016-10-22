using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace TPDDSGrupo44.Models
{
    public class BajaPOI : ActualizacionAsincronica
    {
        public BajaPOI () { }

        public override void actualizar() {

            string url = "http://demo3537367.mockable.io/trash/pois";
            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            
            //BajaPOI bp = JsonConvert.DeserializeObject<BajaPOI>(jsonString);

            List<BajaPOI> listbp = (List<BajaPOI>)javaScriptSerializer.Deserialize(jsonString, typeof(List<BajaPOI>));



        }


    }
}