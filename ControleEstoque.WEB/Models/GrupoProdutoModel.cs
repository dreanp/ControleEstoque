using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.WEB.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        public String Nome { get; set; }
        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "SELECT * FROM grupo_produto ORDER BY nome";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }
            return ret;
        }


        public static GrupoProdutoModel RecuperarPeloId(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = String.Format("SELECT * FROM grupo_produto WHERE (id = {0})", id);
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new GrupoProdutoModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        };
                    }
                }
            }
            return ret;
        }


        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = String.Format("DELETE FROM grupo_produto WHERE (id = {0})", id);
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }
            return ret;
        }


        public int Salvar()
        {
            var ret = 0;
            var model = RecuperarPeloId(this.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (model == null)
                    {
                        comando.CommandText = String.Format("INSERT INTO grupo_produto (nome, ativo) VALUES ('{0}', {1}); SELECT CONVERT (INT, SCOPE_IDENTITY())", this.Nome, this.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = String.Format(
                            "UPDATE grupo_produto SET nome='{1}', ativo={2} WHERE id = {0}",
                            this.Id, this.Nome, this.Ativo ? 1 : 0);

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }

            }

                return ret;
            }




        }
    }