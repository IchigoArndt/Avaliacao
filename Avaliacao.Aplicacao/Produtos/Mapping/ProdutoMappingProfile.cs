using AutoMapper;
using Avaliacao.Aplicacao.Produtos.DTO;
using Avaliacao.Dominio.Produtos;
using System.Collections.Generic;

namespace Avaliacao.Aplicacao.Produtos.Mapping
{
    public class ProdutoMappingProfile : Profile
    {
        public ProdutoMappingProfile()
        {
            CreateMap<ProdutoAdicionarDTO, Produto>()
                .ForMember(dest => dest.FornecedorId, map => map.MapFrom(src => src.CodigoFornecedor))
                .ForMember(dest => dest.Situacao, map => map.MapFrom(src => src.Ativo));

            CreateMap<Produto, ProdutoDTO>()
                .ForMember(dest => dest.CodigoFornecedor, map => map.MapFrom(src => src.FornecedorId))
                .ForMember(dest => dest.DataValidade, map => map.MapFrom(src => src.DataValidade.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.DataFabricacao, map => map.MapFrom(src => src.DataFabricacao.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.Situacao, map => map.MapFrom(src => src.Situacao ? "Ativo" : "Inativo"));

            CreateMap<Produto, Produto>();
        }
    }
}
