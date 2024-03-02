using AutoMapper;
using Avaliacao.Aplicacao.Fornecedores.DTO;
using Avaliacao.Aplicacao.Produtos.DTO;
using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Aplicacao.Fornecedores
{
    public class FornecedorServico : IFornecedorServico
    {
        IFornecedorRepositorio _repositorio;
        IMapper _mapper;

        public FornecedorServico(IFornecedorRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public string Adicionar(FornecedorAdicionarDTO fornecedorDTO)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
            string errosValidacao = "";

            var erros = fornecedor.Validate();

            if (erros.Count > 0)
            {
                errosValidacao = "Não foi possivel inserir o fornecedor, por que foi encontrado os seguintes erros :\n";
                erros.ForEach(erro =>
                {
                    errosValidacao += erro + "\n";
                });

                return errosValidacao;
            }
            else
            {
                try
                {
                    _repositorio.Adicionar(fornecedor);

                    return "";
                }
                catch (Exception ex)
                {
                    return $"Ocorreu um erro ao tentar inserir esse fornecedor. Erro : {ex.Message}";
                }
            }
        }

        public string Alterar(int id, FornecedorAdicionarDTO fornecedorDTO)
        {
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);

            var fornecedorAntigo = _repositorio.BuscarPorId(id);

            var fornecedorEditado = _mapper.Map<Fornecedor>(fornecedor);

            if (fornecedorAntigo == null)
                return "Fornecedor não encontrado com esse código";
            else
                fornecedorEditado.Codigo = id;

            string errosValidacao = "";

            var erros = fornecedorEditado.Validate();

            if (erros.Count > 0)
            {
                errosValidacao = "Não foi possivel autualizar o fornecedor, por que foi encontrado os seguintes erros :\n";
                erros.ForEach(erro =>
                {
                    errosValidacao += erro + "\n";
                });

                return errosValidacao;
            }
            else
            {
                try
                {
                    _repositorio.Editar(fornecedorEditado);

                    return "";
                }
                catch (Exception ex)
                {
                    return $"Ocorreu um erro ao tentar atualizar esse fornecedor. Erro : {ex.Message}";
                }
            }
        }

        public FornecedorDTO BuscarPorId(int id)
        {
            var fornecedor = _repositorio.BuscarPorId(id);
            var produtoEncontrado = _mapper.Map<FornecedorDTO>(fornecedor);
            return produtoEncontrado;
        }

        public FornecedorLerDTO BuscarTodos(int skip, int take, string CNPJ)
        {
            if (skip < 0)
                skip = 0;
            if(take < 0)
                take = 10;

            if (!string.IsNullOrEmpty(CNPJ))
                return new FornecedorLerDTO { Mensagem = "", Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(_repositorio.BuscarTodos(CNPJ).Skip(skip).Take(take)) };
            else
                return new FornecedorLerDTO { Mensagem = "", Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(_repositorio.BuscarTodos().Skip(skip).Take(take)) };
        }

        public string Deletar(int id)
        {
            var codigoRetorno = _repositorio.Deletar(id);

            if (codigoRetorno == -1)
                return "Não existe fornecedor com esse código";
            else
                return "Fornecedor removido com sucesso";
        }
    }
}
