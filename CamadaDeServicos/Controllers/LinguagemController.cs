using CamadaLogicaNegocios.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace CamadaDeServicos.Controllers
{
    public class LinguagemController : ApiController
    {
        // GET: api/Linguagem
        public IEnumerable<Linguagem> Get()//Assinatura do metodo
        {
            var ListarLinguagem = new CamadaLogicaNegocios.Negocios.NegociosLinguagem();
            

            return ListarLinguagem.ListaLinguagens();
        }

        // GET: api/Linguagem/5
        public Linguagem Get(int id)
        {
            var ListarLinguagem = new CamadaLogicaNegocios.Negocios.NegociosLinguagem();


            return ListarLinguagem.Read(id);
        }

        // POST: api/Linguagem
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Linguagem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Linguagem/5
        public void Delete(int id)
        {
        }
    }
}
