using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        [ResponseType(typeof(List<PuntoDeInteres>))]
        public IHttpActionResult GetPuntoDeInteres(string id)
        {
            SearchViewModel modelo;
            List<PuntoDeInteres> puntos = new List<PuntoDeInteres>();

            if (id.Contains(","))
            {
                List<string> palabrasClave = id.Split(new char[] { ',' }).ToList();
                foreach (string p in palabrasClave)
                {
                    modelo = MotorDeBusqueda.buscar(p);
                    puntos.AddRange(modelo.bancosEncontrados);
                    puntos.AddRange(modelo.bancosEncontradosCerca);
                    puntos.AddRange(modelo.cgpsEncontrados);
                    puntos.AddRange(modelo.cgpsEncontradosCerca);
                    puntos.AddRange(modelo.localesEncontrados);
                    puntos.AddRange(modelo.localesEncontradosCerca);
                    puntos.AddRange(modelo.paradasEncontradas);
                    puntos.AddRange(modelo.paradasEncontradasCerca);
                }
            } else
            {
                modelo = MotorDeBusqueda.buscar(id);
                if (modelo.resultados == 0)
                {
                    return NotFound();
                }
                puntos.AddRange(modelo.bancosEncontrados);
                puntos.AddRange(modelo.bancosEncontradosCerca);
                puntos.AddRange(modelo.cgpsEncontrados);
                puntos.AddRange(modelo.cgpsEncontradosCerca);
                puntos.AddRange(modelo.localesEncontrados);
                puntos.AddRange(modelo.localesEncontradosCerca);
                puntos.AddRange(modelo.paradasEncontradas);
                puntos.AddRange(modelo.paradasEncontradasCerca);
            }

            return Ok(puntos);
        }
    }
}