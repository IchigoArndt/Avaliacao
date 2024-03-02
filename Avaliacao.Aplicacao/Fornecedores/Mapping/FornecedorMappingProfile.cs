using AutoMapper;
using Avaliacao.Aplicacao.Fornecedores.DTO;
using Avaliacao.Dominio.Fornecedores;

namespace Avaliacao.Aplicacao.Fornecedores.Mapping
{
    public class FornecedorMappingProfile : Profile
    {
        public FornecedorMappingProfile()
        {
            CreateMap<FornecedorAdicionarDTO, Fornecedor>();
            CreateMap<Fornecedor, FornecedorDTO>()
                 .ForMember(dest => dest.CNPJ, map => map.MapFrom(src => mascararCNPJ(src.CNPJ)));
            CreateMap<Fornecedor, Fornecedor>();
        }

        private string mascararCNPJ(string cnpj)
        {
            return cnpj.Substring(0, 2) + "." + cnpj.Substring(3, 3) + "." + cnpj.Substring(6, 3) + "/" + cnpj.Substring(8, 4) + '-' + cnpj.Substring(12, 2);
        }
    }
}
