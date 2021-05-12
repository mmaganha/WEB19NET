using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoWEB19NET.Dominio.Interfaces
{
    public interface IGenerica<T> where T : class
    {
        Task<T> Adicionar(T objeto);
        Task Alterar(T objeto);
        Task Excluir(int id);
        Task<T> GetT(int id);
        Task<List<T>> GetTs();
    }
}
