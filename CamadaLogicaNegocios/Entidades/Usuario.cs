using System;
using System.Collections.Generic;


namespace CamadaLogicaNegocios.Entidades
{
    public class Usuario 
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string Mensagem { get; set; }

        public Estado Estado { get; set; }
       
        public List<UsuarioLinguagem> ListaUsuarioLinguagens { get; set; }

        public Usuario()
        {

        }


        public Usuario(int idCliente, string nome, DateTime dataNascimento, string sexo, string mensagem, Estado estado, List<UsuarioLinguagem> listaUsuarioLinguagens)
        {
            IdCliente = idCliente;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Mensagem = mensagem;
            Estado = estado;
            ListaUsuarioLinguagens = listaUsuarioLinguagens;
        }

    }
}
