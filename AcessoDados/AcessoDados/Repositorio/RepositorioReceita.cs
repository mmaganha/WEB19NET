using Microsoft.Extensions.Configuration;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using ProjetoWEB19NET.Dominio.Interfaces;
using Dominio.Modelos;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Dominio.ModelViews;

namespace ProjetoWEB19NET.Estrutura.AcessoDados.Repositorio
{
    public class RepositorioReceita : IReceita
    {
        private readonly string connectionString;

        public RepositorioReceita(IConfiguration connectionString)
        {
            this.connectionString = connectionString.GetConnectionString("ProjetoReceita");
        }

        public async Task<Receita> Adicionar(Receita objeto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Titulo", objeto.Titulo, DbType.String);
                parametros.Add("@Descricao", objeto.Descricao, DbType.String);
                parametros.Add("@Ingredientes", objeto.Ingredientes, DbType.String);
                parametros.Add("@ModoPreparo", objeto.ModoPreparo, DbType.String);
                parametros.Add("@Foto", this.ImagemArray(objeto.Foto).ToArray(), DbType.Binary);
                parametros.Add("@Tags", objeto.Tags, DbType.String);
                parametros.Add("@IdCategoria", objeto.IdCategoria, DbType.Int32);
                string sql = "INSERT INTO RECEITA(Titulo, Descricao, Ingredientes, ModoPreparo, Foto, Tags, IdCategoria) " +
                             "VALUES(@Titulo,@Descricao,@Ingredientes,@ModoPreparo,@Foto,@Tags,@IdCategoria) " +
                             "SELECT CAST(SCOPE_IDENTITY() AS INT) ";

                objeto.Id = await connection.QueryFirstOrDefaultAsync<int>(sql, parametros);
            }
            return objeto;
        }

        public async Task Alterar(Receita objeto)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();

                var parametros = new DynamicParameters();
                parametros.Add("@Id", objeto.Id, DbType.Int32);
                parametros.Add("@Titulo", objeto.Titulo, DbType.String);
                parametros.Add("@Descricao", objeto.Descricao, DbType.String);
                parametros.Add("@Ingredientes", objeto.Ingredientes, DbType.String);
                parametros.Add("@ModoPreparo", objeto.ModoPreparo, DbType.String);
                parametros.Add("@Foto", this.ImagemArray(objeto.Foto).ToArray(), DbType.Binary);
                parametros.Add("@Tags", objeto.Tags, DbType.String);
                parametros.Add("@IdCategoria", objeto.IdCategoria, DbType.Int32);

                string sql = "UPDATE RECEITA SET " +
                             "Titulo = @Titulo, Descricao = @Descricao, Ingredientes = @Ingredientes, " +
                             "ModoPreparo = @ModoPreparo, Foto = @Foto, Tags = @Tags, IdCategoria = @IdCategoria " +
                             "WHERE Id = @Id";

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

                string sql = "DELETE FROM RECEITA " +
                             "WHERE ID = @ID";

                await connection.QueryFirstOrDefaultAsync<int>(sql, parametros);
            }
        }

        public async Task<Receita> GetT(int id)
        {
            Receita receita = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int32);

                string sql = "SELECT Id, Titulo, Descricao, Ingredientes, ModoPreparo, Tags, IdCategoria FROM RECEITA " +
                             "WHERE Id = @Id";
                receita = await connection.QueryFirstOrDefaultAsync<Receita>(sql, parametros);
            }

            return receita;
        }

        public Task<List<Receita>> GetTs()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReceitaViewRetorno>> GetTsReceita()
        {
            List<ReceitaViewRetorno> lista = null;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametros = new DynamicParameters();

                string sql = "SELECT * FROM RECEITA ";
                lista = connection.Query<ReceitaViewRetorno>(sql, parametros).ToList();
            }

            return lista;
        }

        private MemoryStream ImagemArray(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);           
            return ms;
        }
    }
}
