using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using TPDDSGrupo44.Models;
using TPDDSGrupo44.ViewModels;
using System.Diagnostics;
using TPDDSGrupo44.Helpers;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection search)
        {
            Stopwatch contador = new Stopwatch();
            contador.Start();
            ViewBag.Message = "Buscá puntos de interés, descubrí cuáles están cerca.";


            string palabraBusqueda = search["palabraClave"];

            try
            {
                SearchViewModel modeloVista = MotorDeBusqueda.buscar(palabraBusqueda);
                int resultados = modeloVista.bancosEncontrados.Count() + modeloVista.bancosEncontradosCerca.Count() + modeloVista.cgpsEncontrados.Count() + modeloVista.localesEncontrados.Count() + modeloVista.localesEncontradosCerca.Count() + modeloVista.paradasEncontradas.Count() + modeloVista.paradasEncontradasCerca.Count();
                if (resultados == 0)
                {
                    ViewBag.Search = "error";
                    ViewBag.SearchText = "Disculpa, pero no encontramos ningún punto con esa palabra clave.";

                }
                return View(modeloVista);
                

            }
            catch
            {
                EnvioDeMails mailer = new EnvioDeMails();
                mailer.enviarMail(contador.Elapsed, 1);
                return View();
            }
        }
    }
}
