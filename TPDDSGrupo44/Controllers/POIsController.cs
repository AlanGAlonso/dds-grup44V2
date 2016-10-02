using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.Controllers
{
    public class POIsController : ApiController
    {
        int manejoError = 1;
        POI[] listaPois = new POI[] {
            new POI { id=1,fechaBaja= DateTime.Parse("2015-05-01T07:34:42-5:00") },
            new POI { id=2,fechaBaja= DateTime.Parse("2014-05-01T07:34:42-5:00") },
            new POI { id=3,fechaBaja= DateTime.Parse("2008-05-01T07:34:42-5:00") }
        };

        List<LogAction> logs = new List<LogAction>();

        // GET: api/POIs
        public IEnumerable<POI> Get()
        {
            //Hacer peticion a la base de datos y traer todos los pois
            return this.listaPois;
        }
       

        // GET: api/POIs/5
        public POI Get(int id)
        {
            POI poiBuscado = null;
            foreach (var POI in this.listaPois)
            {
                if (POI.id == id)
                {
                    poiBuscado = POI;
                };
            }

            if (poiBuscado != null)
            {

                logs.Add(new LogAction { id = (logs.ToString().Length) + 1, fechaInicio = poiBuscado.fechaBaja, fechaFin = DateTime.Now, procesoEjecutado = "2 - Baja de POIs", nombreUsuario = "abc", result = "Ok", mensajeDeError = "" });
                return poiBuscado;
            }
            else
            {
                switch (manejoError) {
                    case 1:
                        //Se envía un mail al Usuario avisando el fallo.
                        EnvioDeMails mail = new EnvioDeMails();
                        mail.enviarMail(new TimeSpan(DateTime.Now.Millisecond), 1);
                         break;
                    case 2:
                        //Se reintenta n veces el proceso. Si se ejecuta n veces en forma errónea queda en estado “error”.
                        break;
                    case 3:
                        //No se realiza ninguna acción.
                        break;

                };
                logs.Add(new LogAction { id = (logs.ToString().Length) + 1, fechaInicio = DateTime.Now, fechaFin = DateTime.Now, procesoEjecutado = "2 - Baja de POIs", nombreUsuario = "abc", result = "Error", mensajeDeError = "Critical Exception" });

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "Critical Exception"
                });
            }
            
        }

        // POST: api/POIs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/POIs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/POIs/5
        public POI Delete(int id)
        {
            POI poiEliminado = null;
            foreach (var POI in this.listaPois)
            {
                if (POI.id == id)
                {
                    POI.fechaBaja = DateTime.Now;
                    poiEliminado = POI;
                };
            }

            if (poiEliminado != null)
            {

                logs.Add(new LogAction { id = (logs.ToString().Length) +1, fechaInicio = poiEliminado.fechaBaja, fechaFin = DateTime.Now, procesoEjecutado = "2 - Baja de POIs", nombreUsuario = "abc", result = "Ok", mensajeDeError = "" });
                return poiEliminado;
            }
            else
            {
                logs.Add(new LogAction { id = (logs.ToString().Length) + 1, fechaInicio = DateTime.Now, fechaFin = DateTime.Now, procesoEjecutado = "2 - Baja de POIs", nombreUsuario = "abc", result = "Error", mensajeDeError = "Critical Exception" });

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "Critical Exception"
                });
            }
            

        }
    }
}
