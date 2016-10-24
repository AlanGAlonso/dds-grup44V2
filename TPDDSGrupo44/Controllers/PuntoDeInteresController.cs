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

        //// GET: api/PuntoDeInteres
        //public IQueryable<PuntoDeInteres> GetPuntoDeInteres()
        //{
        //    return db.PuntoDeInteres;
        //}

        // GET: api/PuntoDeInteres/5
        [ResponseType(typeof(List<PuntoDeInteres>))]
        public IHttpActionResult GetPuntoDeInteres(string palabraBusqueda)
        {
            SearchViewModel modelo = MotorDeBusqueda.buscar(palabraBusqueda);
            if (modelo.resultados == 0)
            {
                return NotFound();
            }

            List<PuntoDeInteres> puntos = new List<PuntoDeInteres>();
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