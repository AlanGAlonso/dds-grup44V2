using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Controllers
{
    public class BusquedasController : ApiController
    {
        private BuscAR db = new BuscAR();

        // GET: api/Busquedas
        public IQueryable<Busqueda> GetBusquedas()
        {
            return db.Busquedas;
        }

        // GET: api/Busquedas/5
        [ResponseType(typeof(Busqueda))]
        public IHttpActionResult GetBusqueda(int id)
        {
            Busqueda busqueda = db.Busquedas.Find(id);
            if (busqueda == null)
            {
                return NotFound();
            }

            return Ok(busqueda);
        }
        
    }
}