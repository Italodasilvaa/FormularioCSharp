using CamadaLogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CamadaLogicaNegocios.Negocios
{
    public class NegociosUsuarioLinguagem
    {
        public List<UsuarioLinguagem> Read(int IdUsuario)
        {

            List<UsuarioLinguagem> listaIdLinguagens = new List<UsuarioLinguagem>();

            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            string query = "select id_linguagens from usuarioslinguagens where id_clientes = @id_cliente";

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_cliente", IdUsuario);



                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        
                        var SelectNegociosLinguagem = new NegociosLinguagem();

                        while (sdr.Read())
                        {
                            var SelectLinguagem = new UsuarioLinguagem();
                            SelectLinguagem.Linguagem = SelectNegociosLinguagem.Read(Convert.ToInt32(sdr["id_linguagens"]));

                            
                            listaIdLinguagens.Add(SelectLinguagem);


                        }
                    }

                }
                con.Close();
            }


            return listaIdLinguagens;

        }

        public void InserirLinguagem(Usuario usuario)
        {

            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;


            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                foreach (var item in usuario.ListaUsuarioLinguagens)
                {
                    string query = "INSERT INTO  usuarioslinguagens VALUES(@id_clientes , @id_linguagens)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_clientes", usuario.IdCliente);
                    cmd.Parameters.AddWithValue("@id_linguagens", item.Linguagem.IdLinguagem);
                    cmd.ExecuteNonQuery();
                }
                con.Close();

            }
        }

        public void DeleteLinguagem(int idUsuario)
        {
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string query = "DELETE FROM usuarioslinguagens WHERE id_clientes= @id_cliente;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_cliente", idUsuario);
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }

        }
    }

}