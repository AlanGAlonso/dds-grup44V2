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
            
                //string url = "http://trimatek.org/Consultas/centro";
            
            var jsonString = string.Empty;

                var client = new WebClient();
            //jsonString = client.DownloadString(url);
            jsonString = "[{'comuna':15,'zonas':'Chacarita, Villa Crespo, Paterna, Villa Ortuzar, Agronomía, Parque Chas','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':4,'zonas':'La Boca, Barracas, Parque Patricios, Nueva Pompeya','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':13,'zonas':'Belgrano, Nuñez, Colegiales','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':3,'zonas':'San Cristobal, Balvanera','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':10,'zonas':'Villa Real, Montecastro, Versalles, Floresta, Velez Sarsfield, Villa Luro','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':6,'zonas':'Caballito','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':14,'zonas':'Palermo','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':8,'zonas':'Villa Soldati, Villa Riachuelo, Villa Lugano','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':5,'zonas':'Almagro, Boedo','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':9,'zonas':'Parque Avellaneda, Liniers, Mataderos','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':2,'zonas':'Recoleta','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':12,'zonas':'Coghlan, Saavedra, Villa Urquiza, Villa Pueyrredón','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':7,'zonas':'Flores, Parque Chacabuco','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':11,'zonas':'Villa General Mitre, Villa Devoto, Villa del Parque, Villa Santa Rita','director':'','domicilio':'','telefono':'','servicios':[]},{'comuna':1,'zonas':'Retiro, San Telmo, San Nicolás, Puerto Madero, Monserrat, Constitución','director':'Roberto Rodriguez','domicilio':'','telefono':'','servicios':[{'nombre':'Atención ciudadana','horarios':[{'diaSemana':1,'horaDesde':9,'minutosDesde':0,'horaHasta':15,'minutosHasta':0}]}]}]";


                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                List<JsonCGP> listCGP = (List<JsonCGP>)javaScriptSerializer.Deserialize(jsonString, typeof(List<JsonCGP>));
                List<CGP> cgps = new List<CGP>();
                foreach (JsonCGP cgp in listCGP)
                {
                    cgps.Add(cgp.mapear());

            }
                return cgps;
       
        }

    }
}






