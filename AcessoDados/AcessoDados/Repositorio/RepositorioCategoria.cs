using Dapper;
using Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using ProjetoWEB19NET.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetoWEB19NET.Estrutura.AcessoDados.Repositorio
{
    public class RepositorioCategoria : ICategoria
    {
        private readonly string connectionString;

        public RepositorioCategoria(IConfiguration connectionString)
        {
            this.connectionString = connectionString.GetConnectionString("ProjetoReceita");
        }

        public async Task<Categoria> Adicionar(Categoria objeto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Descricao", objeto.Descricao, DbType.String);

                string sql = "INSERT INTO CATEGORIA(Descricao) " +
                             "VALUES(@Descricao) " +
                             "SELECT CAST(SCOPE_IDENTITY() AS INT) ";
                objeto.IdCategoria = await connection.QueryFirstOrDefaultAsync<int>(sql, parametros);
            }
            return objeto;
        }

        public async Task Alterar(Categoria objeto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Id", objeto.IdCategoria, DbType.Int32);
                parametros.Add("@Descricao", objeto.Descricao, DbType.String);

                string sql = "UPDATE CATEGORIA SET Descricao = @Descricao " +
                             "WHERE ID = @ID ";
                await connection.QueryFirstOrDefaultAsync(sql, parametros);
            }
        }

        public async Task Excluir(int id)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int32);

                string sql = "DELETE FROM CATEGORIA " +
                             "WHERE ID = @ID ";
                await connection.QueryFirstOrDefaultAsync(sql, parametros);
            }
        }

        public async Task<Categoria> GetT(int id)
        {
            Categoria categoria = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int32);

                string sql = "SELECT Id AS IdCategoria,Descricao FROM CATEGORIA " +
                             "WHERE Id = @Id";
              categoria =  await connection.QueryFirstOrDefaultAsync<Categoria>(sql, parametros);
            }

            return categoria;
        }

        public async Task<List<Categoria>> GetTs()
        {
            List<Categoria> lista = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                string sql = "SELECT Id AS IdCategoria,Descricao FROM CATEGORIA ";
                lista = connection.Query<Categoria>(sql, parametros).ToList();
            }

            return lista;
        }
    }
}
