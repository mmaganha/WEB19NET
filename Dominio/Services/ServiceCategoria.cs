using Dominio.Modelos;
using ProjetoWEB19NET.Dominio.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProjetoWEB19NET.Dominio.Services
{
    public class ServiceCategoria
    {
        private readonly ICategoria serviceCategoria;
        public ServiceCategoria(ICategoria serviceCategoria)
        {
            this.serviceCategoria = serviceCategoria;
        }
        public async Task<Categoria> Adicionar(Categoria objeto)
        {
            return await this.serviceCategoria.Adicionar(objeto);
        }
        public async Task Alterar(Categoria objeto)
        {
            await this.serviceCategoria.Alterar(objeto);
        }
        public async Task Excluir(int id)
        {
            await this.serviceCategoria.Excluir(id);
        }
        public async Task<Categoria> GetCategoria(int id)
        {
            return await this.serviceCategoria.GetT(id);
        }
        public async Task<List<Categoria>> GetCategorias()
        {
            return await this.serviceCategoria.GetTs();
        }
    }
}
