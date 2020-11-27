using CamadaLogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace CamadaLogicaNegocios.Negocios
{
    public class NegociosLinguagem
    {
        public List<Linguagem> ListaLinguagens()
        {
            var lista = new List<Linguagem>();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string query = "SELECT id_linguagens,descricao from linguagens";

                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())//FACO WHILE PARA PERCORRER , LENDO O QUE ESTA NO BANCO DE DADOS 
                    {
                        lista.Add(new Linguagem
                        {
                            IdLinguagem = Convert.ToInt32(sdr["id_linguagens"]),
                            Descricao = Convert.ToString(sdr["descricao"])

                            //criar uma classe nova para popular e coloquei uma lista de classe dentro da model 

                        });
                    }
                }
                con.Close();

            }
            return lista;
        }


        public Linguagem Read(int IdLinguagem)
        {

            var LinguagemUsuario = new Linguagem();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            string query = "select id_linguagens,descricao from linguagens where id_linguagens = @id_linguagens";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_linguagens", IdLinguagem);



                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            LinguagemUsuario.IdLinguagem = Convert.ToInt32(sdr["id_linguagens"]);
                            LinguagemUsuario.Descricao = Convert.ToString(sdr["descricao"]);

                        }
                    }

                }
                con.Close();



            }
            return LinguagemUsuario;
        }

    }
}

