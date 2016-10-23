using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Helpers
{
    public class GetJsonError
    {
        public void getJsonError() //devuelve el status_code y el error 
        {
            string url = "http://demo3537367.mockable.io/trash/pois_bad";
            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<ErrorJson> errorJson = (List<ErrorJson>)javaScriptSerializer.Deserialize(jsonString, typeof(List<ErrorJson>));
            //ErrorJson errorJson = javaScriptSerializer.Deserialize<>;

            //return errorJson.status_code + errorJson.error;

            
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, string>>(jsonString);

            Console.WriteLine(dict["id"]);

        }
    }
}