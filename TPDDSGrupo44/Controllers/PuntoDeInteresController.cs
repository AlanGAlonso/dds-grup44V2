using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using TPDDSGrupo44.DataModels;
using TPDDSGrupo44.Models;
using TPDDSGrupo44.ViewModels;

namespace TPDDSGrupo44.Controllers
{
    public class PuntoDeInteresController : ApiController
    {
        private BuscAR db = new BuscAR();
        
        [HttpGet]
        [ResponseType(typeof(List<PuntoDeInteres>))]
        public IHttpActionResult GetPuntoDeInteres(string id)
        {
            SearchViewModel modelo;
            List<PuntoDeInteres> puntos = new List<PuntoDeInteres>();

            
                    modelo = MotorDeBusqueda.buscar(id,SearchViewModel.Instance);
                    puntos.AddRange(modelo.bancosEncontrados);
                    puntos.AddRange(modelo.bancosEncontradosCerca);
                    puntos.AddRange(modelo.cgpsEncontrados);
                    puntos.AddRange(modelo.cgpsEncontradosCerca);
                    puntos.AddRange(modelo.localesEncontrados);
                    puntos.AddRange(modelo.localesEncontradosCerca);
                    puntos.AddRange(modelo.paradasEncontradas);
                    puntos.AddRange(modelo.paradasEncontradasCerca);
             

            return Ok(puntos);
        }
    }
}