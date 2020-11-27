using CamadaLogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;


namespace CamadaLogicaNegocios.Negocios
{
    public class NegociosEstado
    {
        public List<Estado> ListaEstado()
        {
            var list = new List<Estado>();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string query = "select id_estados,descricao,uf from estados";
                //SELECIONA OS VALORES DO ESTADOS

                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())//FACO WHILE PARA PERCORRER , LENDO O QUE ESTA NO BANCO DE DADOS 
                    {
                        //var estado = new Estado();//COM PARENTES PASSO OS PARAMETROS 
                        list.Add(new Estado//SEM PARENTES PASSO OS ATRIBUTOS
                        {
                            IdEstado = Convert.ToInt32(sdr["id_estados"]),
                            Descricao = Convert.ToString(sdr["descricao"]),
                            Uf = Convert.ToString(sdr["uf"])
                        });



                    }

                }

            }

            return list;
        }



        public Estado Read(int IdEstado)
        {

            var EstadoUsuario = new Estado();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            string query = "select uf,descricao,id_estados from estados where id_estados = @id_estados";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_estados", IdEstado);



                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EstadoUsuario.Uf = Convert.ToString(sdr["uf"]);
                            EstadoUsuario.IdEstado = Convert.ToInt32(sdr["id_estados"]);
                            EstadoUsuario.Descricao = Convert.ToString(sdr["descricao"]);
                        }
                    }

                }

                con.Close();


            }
            return EstadoUsuario;
        }
    }
}

