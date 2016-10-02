using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPDDSGrupo44.Models;

namespace TPDDSGrupo44.Controllers
{
    public class RolController : ApiController
    {
        // GET: api/Rol
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rol/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rol
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rol/5
        public void Put(int idRol, [FromBody] List<FuncionalidadUsuario> funcionalidadesExtras)
        {
            /* E4 - Proceso 3: Agregar acciones para todos los usuarios
             Dada una lista de Acciones por Usuario el proceso debe asignar / actualizar las acciones que puede realizar cada Usuario*/

            Rol rolModificar = new Rol(); // rol base de datos by id
            rolModificar.funcionalidades.AddRange(funcionalidadesExtras);

            //actualizar en la base de datos. * ver el sabado a la tarde
            
        }

        // DELETE: api/Rol/5
        public void Delete(int id)
        {
        }
    }
}
