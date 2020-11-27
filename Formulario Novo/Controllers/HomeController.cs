using Formulario_Novo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Formulario_Novo.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            var cliente = UsuarioDados.Read();



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
            var linguagem = new CamadaLogicaNegocios.Negocios.NegociosLinguagem();
            var model = new Formulario_Novo.Models.SaveViewModel();

            model.Linguagens = new List<SelectListItem>();
            model.Estados = new List<SelectListItem>();

            var estado = new CamadaLogicaNegocios.Negocios.NegociosEstado();




            foreach (var item in linguagem.ListaLinguagens())
            {
                var ling = new SelectListItem();
                ling.Text = item.Descricao;
                ling.Value = item.IdLinguagem.ToString();
                model.Linguagens.Add(ling);
            }

            foreach (var item in estado.ListaEstado())
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
            var inserir = new CamadaLogicaNegocios.Entidades.Usuario();




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

            var save = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            save.Insert(inserir);






            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {

            var UsuarioLinguagem = new CamadaLogicaNegocios.Negocios.NegociosUsuarioLinguagem();

            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            var cliente = UsuarioDados.ObterUsuario(id);

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

        [HttpPost]
        public ActionResult Editar(SaveViewModel usuario)
        {
            var inserir = new CamadaLogicaNegocios.Entidades.Usuario();




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

            var save = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            save.Update(inserir);






            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            UsuarioDados.Delete(id);


            return RedirectToAction("Index");

        }

        public ActionResult Detalhe(int id)
        {

            var UsuarioLinguagem = new CamadaLogicaNegocios.Negocios.NegociosUsuarioLinguagem();

            var UsuarioDados = new CamadaLogicaNegocios.Negocios.NegociosUsuario();
            var cliente = UsuarioDados.ObterUsuario(id);

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




    }
}

