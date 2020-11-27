using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Formulario_Novo.Models
{
    public class FormularioViewModel
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
        public string Linguagens { get; set; }

        [Required(ErrorMessage = "Selecione o Sexo ")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Selecione o estado ")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Digite uma mensagem ")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "{0} dev conter entre  {2} e {1}")]
        public string Mensagem { get; set; }


        public FormularioViewModel()
        {

        }

        public FormularioViewModel(int id, string nome, DateTime dataNascimento, string sexo, string estado, string mensagem)
        {
            IdCliente = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Estado = estado;
            Mensagem = mensagem;

        }

        

        


    }
}