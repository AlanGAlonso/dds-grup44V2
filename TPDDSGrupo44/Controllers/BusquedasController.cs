using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using TPDDSGrupo44.DataModels;

namespace TPDDSGrupo44.Controllers
{
    public class BusquedasController : ApiController
    {
        private BuscAR db = new BuscAR();

        // GET: api/Busquedas/5
        [ResponseType(typeof(object))]
        public IHttpActionResult GetBusqueda(string id)
        {
            IEnumerable<IGrouping<DateTime, Busqueda>> resultadoF;
            Dictionary<DateTime, int> resultadoPorFecha = new Dictionary<DateTime, int>();

            IEnumerable<IGrouping<DispositivoTactil, Busqueda>> resultadoT;
            Dictionary<string, int> resultadoPorTerminal = new Dictionary<string, int>();

            if (id == "fechas")
            {
                using (var db = new BuscAR()) {
                    resultadoF = db.Busquedas.ToList().GroupBy(item => item.fecha);
                    foreach (IGrouping<DateTime, Busqueda> b in resultadoF)
                    {
                        resultadoPorFecha.Add(b.Key, b.Count());
                    }
                }
            } else if (id == "terminal")
            {
                using (var db = new BuscAR())
                {
                    resultadoT = db.Busquedas.ToList().GroupBy(item => item.terminal);
                    foreach (IGrouping<DispositivoTactil, Busqueda> b in resultadoT)
                    {
                        resultadoPorTerminal.Add(b.Key.nombre, b.Count());
                    }
                }
            }


            if (resultadoPorFecha.Count() != 0)
            {
                return Ok(resultadoPorFecha);
            }else if (resultadoPorTerminal.Count() != 0){
                return Ok(resultadoPorTerminal);
            }else
                return NotFound();
            }
        
        }
        
    }
