using Dominio.Modelos;
using Dominio.ModelViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoWEB19NET.Dominio.Interfaces
{
    public interface IReceita : IGenerica<Receita>
    {
        Task<List<ReceitaViewRetorno>> GetTsReceita();
    }
}
