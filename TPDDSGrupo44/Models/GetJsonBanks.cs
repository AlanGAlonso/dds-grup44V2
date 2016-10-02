using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TPDDSGrupo44.Models

{
   public class GetJsonBanks 
    {
        public List<Banco> getJsonData()
        {
            //depende de la búsqueda
            //string url = "http://trimatek.org/Consultas/banco?banco=" + bank + "&servicio=" + service;

            string url = "http://private-96b476-ddsutn.apiary-mock.com/banks"; 
            // Por si no funciona la url..(caida) string jsonString = "[{'banco':'Galicia','x':9999.0,'y':9999.0,'sucursal':'Flores','gerente':'Roberto Gomez','servicios':['Plazo Fijo','Acciones','Cobros','Pagos']},{'banco':'Santander Rio','x':1234.0,'y':1234.0,'sucursal':'Almagro','gerente':'Lorenzo','servicios':['Moneda extranjera','Cobros','Pagos']}]";
            //string url = "http://trimatek.org/Consultas/banco";

            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);
            
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<JsonBank> listBanks = (List<JsonBank>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonBank>));

            List<Banco> bancos = new List<Banco>();
            foreach (JsonBank banco in listBanks)
            {
                Banco nuevoBanco = new Banco(banco.banco, DbGeography.FromText("POINT(" +banco.x.ToString().Replace(",", ".")+ " "+banco.y.ToString().Replace(",", ".") + ")"), banco.servicios);
                bancos.Add(nuevoBanco);
            }
            return bancos;
        }
        
    }
}






