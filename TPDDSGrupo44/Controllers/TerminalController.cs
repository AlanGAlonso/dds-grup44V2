using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.Controllers
{
    public class TerminalController : ApiController
    {
        // GET: api/Terminal
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Terminal/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Terminal
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Terminal/5
        public void Put(int id, [FromBody] Terminal terminal)
        {
            /* E4 - Proceso 3: Agregar acciones para todos los usuarios
            Activar/desactivar las acciones por Terminal en forma dinámica.*/

            Terminal terminalModificada = new Terminal(); // busco la terminal en la base de datos y la modifico con el orm
            terminalModificada.activa = terminal.activa; //tomo los valores que vienen como parametros en la request. 
             //actualizar en la base de datos. * ver el sabado a la tarde

        }

        // DELETE: api/Terminal/5
        public void Delete(int id)
        {
        }
    }
}
