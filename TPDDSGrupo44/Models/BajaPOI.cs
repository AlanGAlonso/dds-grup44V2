using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Models
{
    public class BajaPOI : ActualizacionAsincronica
    {
        public BajaPOI() { }

        public int id { get; set; }
        public DateTime fechaBaja { get; set; }


        public override void actualizar(String palabraBusqueda)
        {

            string url = "http://demo3537367.mockable.io/trash/pois";
            var jsonString = string.Empty;

            var client = new WebClient();
            jsonString = client.DownloadString(url);

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            //BajaPOI bp = JsonConvert.DeserializeObject<BajaPOI>(jsonString);

            List<BajaPOI> listbp = (List<BajaPOI>)javaScriptSerializer.Deserialize(jsonString, typeof(List<BajaPOI>));

            //string palabraBusqueda = listbp.Find(id);

            SearchViewModel modeloVista = MotorDeBusqueda.buscar(palabraBusqueda);

            if (modeloVista.paradasEncontradas.Count > 0)
            {
                ParadaDeColectivo parada = new ParadaDeColectivo();
                parada.eliminarParada(id);

                //ParadaDeColectivo.eliminarParada(id);
            }
            else if (modeloVista.bancosEncontrados.Count > 0)
            {
                Banco banco = new Banco();
                banco.eliminarBanco(id);
                //Banco.eliminarBanco(id);
            }
            else if (modeloVista.cgpsEncontrados.Count > 0)
            {
                CGP cgp = new CGP();
                cgp.eliminarCGP(id);

                //CGP.eliminarCGP(id);
            }
            else if (modeloVista.localesEncontrados.Count > 0) {
                LocalComercial local = new LocalComercial();
                local.eliminarLocComercial(id);

                //LocalComercial.eliminarLocComercial(id);
            }


            //if (modeloVista.res == 0)
            //{
            //    //ViewBag.Search = "error";
            //    //ViewBag.SearchText = "Disculpa, pero no encontramos ningún punto con esa palabra clave.";

            //}
            //else
            //{
            //    //BajaPOI.eliminarPOI(id);
            //    foreach (BajaPOI)


            //}
            ////return View(modeloVista);
            //return;


        }
        //public static void eliminarPOI(int id)
        //{
        //    Banco banco = new Banco();
        //    banco.eliminarBanco(id);

        //}

        List<PuntoDeInteres> puntos = new List<PuntoDeInteres>();


    }
}