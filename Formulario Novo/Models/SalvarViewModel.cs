using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Formulario_Novo.Models
{
    public class SalvarViewModel
    {

        public int IdCliente { get; set; }

        public int Id_estado { get; set; }


        [Required(ErrorMessage = "Digite o Nome ")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve possuir entre  {2} e {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a data de Nascimento ")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Selecione uma linguagem")]
        public List<SelectListItem> Linguagens { get; set; }

        [Required(ErrorMessage = "Selecione o Sexo ")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Selecione o estado ")]
        public List<SelectListItem> Estado { get; set; }
        [Required(ErrorMessage = "Digite uma mensagem ")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "{0} dev conter entre  {2} e {1}")]
        public string Mensagem { get; set; }


        public SalvarViewModel()
        {

        }

        public SalvarViewModel(int idCliente, int id_estado, string nome, DateTime dataNascimento, List<SelectListItem> linguagens, string sexo, List<SelectListItem> estado, string mensagem)
        {
            IdCliente = idCliente;
            Id_estado = id_estado;
            Nome = nome;
            DataNascimento = dataNascimento;
            Linguagens = linguagens;
            Sexo = sexo;
            Estado = estado;
            Mensagem = mensagem;
        }
    }
}