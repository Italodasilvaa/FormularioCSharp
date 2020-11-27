using CamadaLogicaNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CamadaLogicaNegocios.Negocios
{
    public class NegociosUsuario
    {
        public List<Usuario> Read()
        {

            List<Usuario> clientes = new List<Usuario>();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            string query = "SELECT  id_clientes,nome,dataNascimento,sexo,mensagem,id_estados from Usuario ";
            var NegociosUsuarioLinguagem = new NegociosUsuarioLinguagem();
            var NegociosEstado = new NegociosEstado();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            clientes.Add(new Usuario
                            {

                                IdCliente = Convert.ToInt32(sdr["id_clientes"]),
                                Nome = Convert.ToString(sdr["nome"]),
                                DataNascimento = Convert.ToDateTime(sdr["dataNascimento"]),
                                Sexo = Convert.ToString(sdr["sexo"]),
                                Mensagem = Convert.ToString(sdr["mensagem"]),
                                Estado = NegociosEstado.Read(Convert.ToInt32(sdr["id_estados"])),
                                ListaUsuarioLinguagens = NegociosUsuarioLinguagem.Read(Convert.ToInt32(sdr["id_clientes"]))


                            });
                        }
                    }

                }
                con.Close();
            }


            return clientes;
        }

        

        public Usuario ObterUsuario(int id)
        {
            var Selecionar = new Usuario();
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            string query = "SELECT  id_clientes,nome,dataNascimento,sexo,mensagem,id_estados from Usuario  where id_clientes = @id_cliente";
            var NegociosUsuarioLinguagem = new NegociosUsuarioLinguagem();
            var NegociosEstado = new NegociosEstado();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    con.Open();
                    cmd.Parameters.AddWithValue("@id_cliente", id);

                    using (SqlDataReader sdr = cmd.ExecuteReader())

                    {

                        while (sdr.Read())
                        {


                            Selecionar.IdCliente = (Convert.ToInt32(sdr["id_clientes"]));
                            Selecionar.Nome = Convert.ToString(sdr["nome"]);
                            Selecionar.DataNascimento = Convert.ToDateTime(sdr["dataNascimento"]);
                            Selecionar.Sexo = Convert.ToString(sdr["sexo"]);
                            Selecionar.Mensagem = Convert.ToString(sdr["mensagem"]);
                            Selecionar.Estado = NegociosEstado.Read(Convert.ToInt32(sdr["id_estados"]));
                            Selecionar.ListaUsuarioLinguagens = NegociosUsuarioLinguagem.Read(Convert.ToInt32(sdr["id_clientes"]));

                        }


                    }

                    con.Close();

                }

            }
            return Selecionar;
        }

        public void Insert(Usuario InserirDados)
        {

            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();


                string query = "INSERT INTO Usuario VALUES(@nome, @dataNascimento,@sexo,@mensagem,@id_estado);";
                query += "SELECT SCOPE_IDENTITY()";

                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@nome", InserirDados.Nome);
                cmd.Parameters.AddWithValue("@dataNascimento", InserirDados.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo", InserirDados.Sexo);
                cmd.Parameters.AddWithValue("@mensagem", InserirDados.Mensagem);
                cmd.Parameters.AddWithValue("@id_estado", InserirDados.Estado.IdEstado);

                InserirDados.IdCliente = Convert.ToInt32(cmd.ExecuteScalar());

                var negocio = new NegociosUsuarioLinguagem();
                negocio.InserirLinguagem(InserirDados);

            }


        }


        public void Update(Usuario AtualizaDados)
        {
            string query = @"UPDATE Usuario SET nome=@Nome, dataNascimento=@dataNascimento, sexo=@sexo, mensagem=@mensagem, id_estados=@id_estados WHERE id_clientes=@id_clientes";
            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id_clientes", AtualizaDados.IdCliente);
                cmd.Parameters.AddWithValue("@nome", AtualizaDados.Nome);
                cmd.Parameters.AddWithValue("@dataNascimento", AtualizaDados.DataNascimento);
                cmd.Parameters.AddWithValue("@sexo", AtualizaDados.Sexo);
                cmd.Parameters.AddWithValue("@mensagem", AtualizaDados.Mensagem);
                cmd.Parameters.AddWithValue("@id_estados", AtualizaDados.Estado.IdEstado);
                cmd.ExecuteNonQuery();

                var NegocioDel = new NegociosUsuarioLinguagem();
                NegocioDel.DeleteLinguagem(AtualizaDados.IdCliente);

                var negocio = new NegociosUsuarioLinguagem();
                negocio.InserirLinguagem(AtualizaDados);


            }
        }


        public void Delete(int idUsuario)
        {
            var DeleteLinguagemCliente = new NegociosUsuarioLinguagem();
            DeleteLinguagemCliente.DeleteLinguagem(idUsuario);


            string constr = ConfigurationManager.ConnectionStrings["dbFormularioTeste"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                string query = "DELETE FROM Usuario WHERE id_clientes= @id_clientes;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_clientes", idUsuario);

                    cmd.ExecuteNonQuery();
                }



            }
        }
    }
}



