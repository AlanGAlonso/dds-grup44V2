using System.Web.Mvc;
using System.Linq;
using TPDDSGrupo44.Models;
using TPDDSGrupo44.ViewModels;
using System.Diagnostics;
using TPDDSGrupo44.Helpers;

namespace TPDDSGrupo44.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            SearchViewModel modeloVista = SearchViewModel.Instance;
            return View(modeloVista);
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
                SearchViewModel modeloVista = MotorDeBusqueda.buscar(palabraBusqueda, SearchViewModel.Instance);
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
