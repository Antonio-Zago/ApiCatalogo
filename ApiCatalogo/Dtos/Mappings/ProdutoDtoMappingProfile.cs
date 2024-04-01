using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.Dtos.Mappings
{
    public class ProdutoDtoMappingProfile : Profile
    {
        public ProdutoDtoMappingProfile()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            CreateMap<Produto, ProdutoDto>().ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria.Nome))
                .ReverseMap();
            
        }
    }
}
