using CamadaLogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CamadaDeServicos.Controllers
{
    public class EstadoController : ApiController
    {
        // GET: api/Estado
        public IEnumerable<Estado> Get()
        {
            var ListarEstado = new CamadaLogicaNegocios.Negocios.NegociosEstado();


            return ListarEstado.ListaEstado();
        }

        // GET: api/Estado/5
        public Estado Get(int id)
        {
            var ListarEstado = new CamadaLogicaNegocios.Negocios.NegociosEstado();


            return ListarEstado.Read(id);
        }

        // POST: api/Estado
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Estado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Estado/5
        public void Delete(int id)
        {
        }
    }
}
