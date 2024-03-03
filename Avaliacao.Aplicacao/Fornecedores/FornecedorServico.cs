using AutoMapper;
using Avaliacao.Aplicacao.Fornecedores.DTO;
using Avaliacao.Aplicacao.Produtos;
using Avaliacao.Aplicacao.Produtos.DTO;
using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Aplicacao.Fornecedores
{
    public class FornecedorServico : IFornecedorServico
    {
        IFornecedorRepositorio _repositorio;
        IMapper _mapper;
        ILogger<FornecedorServico> _log;

        public FornecedorServico(IFornecedorRepositorio repositorio, IMapper mapper, ILogger<FornecedorServico> log)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _log = log;
        }

        public string Adicionar(FornecedorAdicionarDTO fornecedorDTO)
        {
            _log.LogInformation("Convertendo FornecedorAdicionarDTO em Fornecedor");
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

                _log.LogWarning($"Encontrado os seguintes erros de regra de negócio \n {errosValidacao}");
                return errosValidacao;
            }
            else
            {
                try
                {
                    _log.LogInformation("Adicionando o fornecedor na base de dados");
                    _repositorio.Adicionar(fornecedor);

                    return "";
                }
                catch (Exception ex)
                {
                    _log.LogCritical($"Ocorreu um erro ao adicionar esse fornecedor na base de dados. Erro : {ex.Message}");
                    return $"Ocorreu um erro ao tentar inserir esse fornecedor. Erro : {ex.Message}";
                }
            }
        }

        public string Alterar(int id, FornecedorAdicionarDTO fornecedorDTO)
        {
            _log.LogInformation("Convertendo FornecedorAdicionarDTO em Fornecedor");
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);

            _log.LogInformation("Buscando o fornecedor pelo código informado");
            var fornecedorAntigo = _repositorio.BuscarPorId(id);

            var fornecedorEditado = _mapper.Map<Fornecedor>(fornecedor);

            _log.LogInformation("Verificando se existe o fornecedor no banco");
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

                _log.LogWarning($"Encontrado os seguintes erros de regra de negócio \n {errosValidacao}");
                return errosValidacao;
            }
            else
            {
                try
                {
                    _log.LogInformation("Editando o fornecedor na base de dados");
                    _repositorio.Editar(fornecedorEditado);

                    return "";
                }
                catch (Exception ex)
                {
                    _log.LogCritical($"Ocorreu um erro ao editar esse fornecedor na base de dados. Erro : {ex.Message}");
                    return $"Ocorreu um erro ao tentar atualizar esse fornecedor. Erro : {ex.Message}";
                }
            }
        }

        public FornecedorDTO BuscarPorId(int id)
        {
            _log.LogInformation($"Buscando o fornecedor pelo código : {id}");
            var fornecedor = _repositorio.BuscarPorId(id);
            var produtoEncontrado = _mapper.Map<FornecedorDTO>(fornecedor);
            return produtoEncontrado;
        }

        public FornecedorLerDTO BuscarTodos(int skip, int take, string CNPJ)
        {
            if (skip < 0)
            {
                _log.LogWarning("propiedade SKIP informada com valor negativo. \n Adicionando o valor default de 0");
                skip = 0;
            }

            if(take < 0)
            {
                _log.LogWarning("propiedade TAKE informada com valor negativo. \n Adicionando o valor default de 10");
                take = 10;
            }

            if (!string.IsNullOrEmpty(CNPJ))
            {
                _log.LogInformation("Realizado a busca do fornecedor pelo filtro de CNPJ");
                return new FornecedorLerDTO { Mensagem = "", Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(_repositorio.BuscarTodos(CNPJ).Skip(skip).Take(take)) };
            }
            else
            {
                _log.LogInformation("Realizado a busca de todos os fornecedores");
                return new FornecedorLerDTO { Mensagem = "", Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(_repositorio.BuscarTodos().Skip(skip).Take(take)) };
            }
        }

        public string Deletar(int id)
        {
            _log.LogInformation($"Removendo o fornecedor pelo código : {id}");
            var codigoRetorno = _repositorio.Deletar(id);

            if (codigoRetorno == -1)
                return "Não existe fornecedor com esse código";
            else
                return "Fornecedor removido com sucesso";
        }
    }
}
