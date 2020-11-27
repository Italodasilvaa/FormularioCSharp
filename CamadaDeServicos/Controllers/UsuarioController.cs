using CamadaLogicaNegocios.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace CamadaDeServicos.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        public IEnumerable<Usuario> Get()//Assinatura do metodo
        {
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();


            return UsuarioDados.Read();
        }

        // GET: api/Usuario/5
        public Usuario Get(int id)
        {
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();

            return UsuarioDados.ObterUsuario(id);
        }

        // POST: api/Usuario
        public void Post([FromBody] Usuario value)
        {
          
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();   

            UsuarioDados.Insert(value);


        }

        // PUT: api/Usuario
        public void Put([FromBody] Usuario value)
        {
           
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            UsuarioDados.Update(value);

        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            UsuarioDados.Delete(id);
        }
    }
}
