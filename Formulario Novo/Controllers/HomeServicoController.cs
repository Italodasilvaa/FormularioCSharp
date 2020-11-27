using CamadaLogicaNegocios.Entidades;
using CamadaLogicaNegocios.Negocios;
using Formulario_Novo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Formulario_Novo.Controllers
{
    public class HomeServicoController : Controller
    {
        // GET: HomeServico
        public ActionResult Index()
        {

            var cliente = GetUsuarios();



            List<FormularioViewModel> Formulario = new List<FormularioViewModel>();
            foreach (var item in cliente)
            {
                Formulario.Add(new FormularioViewModel
                {
                    IdCliente = item.IdCliente,
                    Nome = item.Nome,
                    DataNascimento = item.DataNascimento,
                    Sexo = item.Sexo,
                    Linguagens = string.Join(",", item.ListaUsuarioLinguagens.Select(x => x.Linguagem.Descricao).ToList()),
                    Estado = item.Estado.Descricao,
                    Mensagem = item.Mensagem

                }
                    );

            }





            return View(Formulario);
        }



        public ActionResult Create()
        {
            //var linguagem = GetLinguagem();
            var model = new Formulario_Novo.Models.SaveViewModel();

            model.Linguagens = new List<SelectListItem>();
            model.Estados = new List<SelectListItem>();

            // estado = GetEstado();




            foreach (var item in GetLinguagem())
            {
                var ling = new SelectListItem();
                ling.Text = item.Descricao;
                ling.Value = item.IdLinguagem.ToString();
                model.Linguagens.Add(ling);
            }

            foreach (var item in GetEstado())
            {
                var estad = new SelectListItem();
                estad.Text = item.Descricao;
                estad.Value = item.IdEstado.ToString();
                model.Estados.Add(estad);
            }




            return View(model);

        }
        [HttpPost]
        public ActionResult Salvar(SaveViewModel usuario)
        {
            HttpClient client = new HttpClient();
            var inserir = new CamadaLogicaNegocios.Entidades.Usuario();



            HttpResponseMessage response = client.GetAsync($"https://localhost:44339/api/usuario/{usuario}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                inserir = JsonConvert.DeserializeObject<CamadaLogicaNegocios.Entidades.Usuario>(jsonResult);
            }

            inserir.Nome = usuario.Nome;
            inserir.DataNascimento = usuario.DataNascimento;
            inserir.Sexo = usuario.Sexo;
            var selectLinguagem = usuario.Linguagens.Where(x => x.Selected == true);
            inserir.ListaUsuarioLinguagens = new List<CamadaLogicaNegocios.Entidades.UsuarioLinguagem>();

            foreach (var item in selectLinguagem)
            {

                var ling = new CamadaLogicaNegocios.Entidades.UsuarioLinguagem();

                response = client.GetAsync($"https://localhost:44339/api/usuario/{usuario}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    inserir = JsonConvert.DeserializeObject<CamadaLogicaNegocios.Entidades.Usuario>(jsonResult);
                }
                ling.Linguagem = new CamadaLogicaNegocios.Entidades.Linguagem();
                ling.Linguagem.IdLinguagem = Convert.ToInt32(item.Value);

                inserir.ListaUsuarioLinguagens.Add(ling);
            };

            inserir.Estado = new CamadaLogicaNegocios.Entidades.Estado { IdEstado = Convert.ToInt32(usuario.Id_estado) };
            inserir.Mensagem = usuario.Mensagem;

            PostUsuarios(inserir);

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {



            HttpClient client = new HttpClient();
            var cliente = new CamadaLogicaNegocios.Entidades.Usuario();
            HttpResponseMessage response = client.GetAsync($"https://localhost:44339/api/usuario/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<CamadaLogicaNegocios.Entidades.Usuario>(json);
            }


            List<CamadaLogicaNegocios.Entidades.Estado> estado = new List<CamadaLogicaNegocios.Entidades.Estado>();
            response = client.GetAsync("https://localhost:44339/api/Estado").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsons = response.Content.ReadAsStringAsync().Result;
                estado = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.Estado>>(jsons);
            }



            List<CamadaLogicaNegocios.Entidades.UsuarioLinguagem> UsuarioLinguagem = new List<CamadaLogicaNegocios.Entidades.UsuarioLinguagem>();

            response = client.GetAsync($"https://localhost:44339/api/UsuarioLinguagem/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                UsuarioLinguagem = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.UsuarioLinguagem>>(json);
            }




            List<CamadaLogicaNegocios.Entidades.Linguagem> Linguagem = new List<CamadaLogicaNegocios.Entidades.Linguagem>();



            response = client.GetAsync("https://localhost:44339/api/Linguagem").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                Linguagem = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.Linguagem>>(json);
            }


            var model = new Formulario_Novo.Models.SaveViewModel();

            model.Linguagens = new List<SelectListItem>();
            model.Estados = new List<SelectListItem>();

            



            model.IdCliente = cliente.IdCliente;
            model.Nome = cliente.Nome;
            model.DataNascimento = cliente.DataNascimento;
            model.Sexo = cliente.Sexo;
            model.Mensagem = cliente.Mensagem;




            foreach (var item in Linguagem)
            {
                var ling = new SelectListItem();
                ling.Text = item.Descricao;
                ling.Value = item.IdLinguagem.ToString();
                ling.Selected = UsuarioLinguagem.Count(x => x.Linguagem.IdLinguagem == item.IdLinguagem) > 0;


                model.Linguagens.Add(ling);
            }

            foreach (var item in estado)
            {
                var estad = new SelectListItem();
                estad.Text = item.Descricao;
                estad.Value = item.IdEstado.ToString();
                estad.Selected = item.IdEstado == cliente.Estado.IdEstado;

                model.Estados.Add(estad);
            }




            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(SaveViewModel usuario)
        {

            HttpClient client = new HttpClient();
            var inserir = new CamadaLogicaNegocios.Entidades.Usuario();
            HttpResponseMessage response = client.GetAsync($"https://localhost:44339/api/usuario/{usuario}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                inserir = JsonConvert.DeserializeObject<CamadaLogicaNegocios.Entidades.Usuario>(jsonResult);
            }



            inserir.IdCliente = usuario.IdCliente;
            inserir.Nome = usuario.Nome;
            inserir.DataNascimento = usuario.DataNascimento;
            inserir.Sexo = usuario.Sexo;
            var selectLinguagem = usuario.Linguagens.Where(x => x.Selected == true);
            inserir.ListaUsuarioLinguagens = new List<CamadaLogicaNegocios.Entidades.UsuarioLinguagem>();
            foreach (var item in selectLinguagem)
            {

                var ling = new CamadaLogicaNegocios.Entidades.UsuarioLinguagem();
                ling.Linguagem = new CamadaLogicaNegocios.Entidades.Linguagem();
                ling.Linguagem.IdLinguagem = Convert.ToInt32(item.Value);


                inserir.ListaUsuarioLinguagens.Add(ling);
            };

            inserir.Estado = new CamadaLogicaNegocios.Entidades.Estado { IdEstado = Convert.ToInt32(usuario.Id_estado) };
            inserir.Mensagem = usuario.Mensagem;

            PostUsuarios(inserir);






            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            HttpClient client = new HttpClient();




            HttpResponseMessage response = client.DeleteAsync($"https://localhost:44339/api/usuario/{id}").Result;


            return RedirectToAction("Index");

        }

        public ActionResult Detalhe(int id)
        {


            HttpClient client = new HttpClient();



            var cliente = new CamadaLogicaNegocios.Entidades.Usuario();



            HttpResponseMessage response = client.GetAsync($"https://localhost:44339/api/usuario/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<CamadaLogicaNegocios.Entidades.Usuario>(jsonResult);
            }



            var UsuarioLinguagem = new CamadaLogicaNegocios.Negocios.NegociosUsuarioLinguagem();

            //var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            //var cliente = UsuarioDados.ObterUsuario(id);

            var linguagem = new CamadaLogicaNegocios.Negocios.NegociosLinguagem();
            var model = new Formulario_Novo.Models.SaveViewModel();

            model.Linguagens = new List<SelectListItem>();
            model.Estados = new List<SelectListItem>();

            var estado = new CamadaLogicaNegocios.Negocios.NegociosEstado();
            var IdLinguagens = UsuarioLinguagem.Read(id);



            model.IdCliente = cliente.IdCliente;
            model.Nome = cliente.Nome;
            model.DataNascimento = cliente.DataNascimento;
            model.Sexo = cliente.Sexo;
            model.Mensagem = cliente.Mensagem;




            foreach (var item in linguagem.ListaLinguagens())
            {
                var ling = new SelectListItem();
                ling.Text = item.Descricao;
                ling.Value = item.IdLinguagem.ToString();
                ling.Selected = IdLinguagens.Count(x => x.Linguagem.IdLinguagem == item.IdLinguagem) > 0;


                model.Linguagens.Add(ling);
            }

            foreach (var item in estado.ListaEstado())
            {
                var estad = new SelectListItem();
                estad.Text = item.Descricao;
                estad.Value = item.IdEstado.ToString();
                estad.Selected = item.IdEstado == cliente.Estado.IdEstado;
                model.Estados.Add(estad);
            }



            return View(model);

        }
        /// METODOS PARA WEBAPI 
        /// 

        public List<CamadaLogicaNegocios.Entidades.Usuario> GetUsuarios()
        {
            HttpClient client = new HttpClient();
            List<CamadaLogicaNegocios.Entidades.Usuario> retorno = new List<CamadaLogicaNegocios.Entidades.Usuario>();

            HttpResponseMessage response = client.GetAsync("https://localhost:44339/api/Usuario").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                retorno = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.Usuario>>(jsonResult);

            }
            return retorno;
        }


        public bool PostUsuarios(Usuario usuario)
        {
            HttpClient client = new HttpClient();

            var jsonResult = JsonConvert.SerializeObject(usuario);
            StringContent queryString = new StringContent(jsonResult);
            queryString.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            HttpResponseMessage response = client.PostAsync("https://localhost:44339/api/usuario/", queryString).Result;
            if (response.IsSuccessStatusCode == false)
            {
                var json = response.Content.ReadAsStringAsync().Result;

            }
            var retorno = response.IsSuccessStatusCode;



            return retorno;
        }

        public bool PutUsuario(Usuario usuario)
        {
            HttpClient client = new HttpClient();

            var jsonResult = JsonConvert.SerializeObject(usuario);
            StringContent queryString = new StringContent(jsonResult);
            queryString.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            HttpResponseMessage response = client.PostAsync("https://localhost:44339/api/usuario/", queryString).Result;
            if (response.IsSuccessStatusCode == false)
            {
                var json = response.Content.ReadAsStringAsync().Result;

            }
            var retorno = response.IsSuccessStatusCode;



            return retorno;
        }


        public List<CamadaLogicaNegocios.Entidades.Linguagem> GetLinguagem()
        {
            HttpClient client = new HttpClient();
            List<CamadaLogicaNegocios.Entidades.Linguagem> retorno = new List<CamadaLogicaNegocios.Entidades.Linguagem>();

            HttpResponseMessage response = client.GetAsync("https://localhost:44339/api/Linguagem").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                retorno = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.Linguagem>>(jsonResult);

            }
            return retorno;


        }

        public List<CamadaLogicaNegocios.Entidades.Estado> GetEstado()
        {
            HttpClient client = new HttpClient();
            List<CamadaLogicaNegocios.Entidades.Estado> retorno = new List<CamadaLogicaNegocios.Entidades.Estado>();

            HttpResponseMessage response = client.GetAsync("https://localhost:44339/api/Estados").Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                retorno = JsonConvert.DeserializeObject<List<CamadaLogicaNegocios.Entidades.Estado>>(jsonResult);

            }
            return retorno;


        }


    }
}