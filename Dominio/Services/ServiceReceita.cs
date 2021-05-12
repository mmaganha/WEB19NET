using Dominio.Modelos;
using Dominio.ModelViews;
using ProjetoWEB19NET.Dominio.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoWEB19NET.Dominio.Services
{
    public class ServiceReceita
    {
        private readonly IReceita receita;

        public ServiceReceita(IReceita receita)
        {
            this.receita = receita;
        }

        public async Task<Receita> Adicionar(Receita rec)
        {
            var resultado = await this.receita.Adicionar(rec);
            return resultado;
        }
        public async Task Alterar(Receita rec)
        {
            await this.receita.Alterar(rec);
        }
        public async Task Excluir(int id)
        {
            await this.receita.Excluir(id);
        }
        public async Task<Receita> GetT(int id)
        {
            var resultado = await this.receita.GetT(id);
            return resultado;
        }
        public async Task<List<ReceitaViewRetorno>> GetTs()
        {
            var resultado = await this.receita.GetTsReceita();
            return resultado;
        }
    }
}
