using AutoMapper;
using Dominio.Modelos;
using ProjetoWEB19NET.Dominio.ModelViews;

namespace Dominio.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<Receita, ReceitaView>().ReverseMap();
            this.CreateMap<Categoria, CategoriaView>().ReverseMap();
        }
    }
}
