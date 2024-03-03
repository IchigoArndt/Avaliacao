using Avaliacao.Dominio.Produtos;
using System;
using Avaliacao.Repositorio.Repositorios.ProdutosRepositorio;
using System.Collections.Generic;
using Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio;
using Avaliacao.Aplicacao.Produtos.DTO;
using AutoMapper;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Avaliacao.Aplicacao.Produtos
{
    public class ProdutoServico : IProdutoServico
    {
        IProdutoRepositorio _repositorio;
        IFornecedorRepositorio _repositorioFornecedor;
        IMapper _mapper;
        ILogger<ProdutoServico> _log;

        public ProdutoServico(IProdutoRepositorio repositorio, IFornecedorRepositorio fornecedorRepositorio, IMapper mapper, ILogger<ProdutoServico> log)
        {
            _repositorio = repositorio;
            _repositorioFornecedor = fornecedorRepositorio;
            _mapper = mapper;
            _log = log;
        }

        public string Adicionar(ProdutoAdicionarDTO produtoDTO)
        {
            _log.LogInformation("Convertendo ProdutoAdicionarDTO em Produto");
            var produto = _mapper.Map<Produto>(produtoDTO);

            string errosValidacao = "";
            bool existeFornecedor;

            _log.LogInformation("Verificando se existe o código do fornecedor");
            existeFornecedor = _repositorioFornecedor.verificaSeExisteFornecedor(produto.FornecedorId);

            _log.LogInformation("Validando as regras de negócio do Produto");
            var erros = produto.Validate();

            if (erros.Count > 0)
            {
                errosValidacao = "Não foi possivel inserir o produto, por que foi encontrado os seguintes erros :\n";
                erros.ForEach(erro =>
                {
                    errosValidacao += erro + "\n";
                });

                _log.LogWarning($"Encontrado os seguintes erros de regra de negócio \n {errosValidacao}");

                return errosValidacao;
            }
            else if (!existeFornecedor)
            {
                _log.LogWarning($"Não encontrado o fornecedor com o código : {produto.FornecedorId}");
                return "Não existe fornecedor com esse código no sistema. Por favor cadastre esse fornecedor ou informe outro código";
            }
            else
            {
                try
                {
                    _log.LogInformation("Adicionando o produto na base de dados");
                    _repositorio.Adicionar(produto);

                    return "";
                }
                catch (Exception ex)
                {
                    _log.LogCritical($"Ocorreu um erro ao adicionar esse produto na base de dados. Erro : {ex.Message}");
                    return $"Ocorreu um erro ao tentar inserir esse produto. Erro : {ex.Message}";
                }
            }
        }

        public string Alterar(int id, ProdutoAdicionarDTO produtoDTO)
        {
            _log.LogInformation("Convertendo ProdutoAdicionarDTO em Produto");
            var produto = _mapper.Map<Produto>(produtoDTO);

            _log.LogInformation("Buscando o produto pelo código informado");
            var produtoAntigo = _repositorio.BuscarPorId(id);

            var produtoEditado = _mapper.Map<Produto>(produto);

            _log.LogInformation("Verificando se existe o produto no banco");
            if (produtoAntigo == null)
                return "Produto não encontrado com esse código";
            else
                produtoEditado.Codigo = id;

            _log.LogInformation("Verificando se existe o fornecedor com o código informado");
            bool existeFornecedor = _repositorioFornecedor.verificaSeExisteFornecedor(produto.FornecedorId);

            string errosValidacao = "";

            var erros = produtoEditado.Validate();

            if (erros.Count > 0)
            {
                errosValidacao = "Não foi possivel autualizar o produto, por que foi encontrado os seguintes erros :\n";
                erros.ForEach(erro =>
                {
                    errosValidacao += erro + "\n";
                });

                _log.LogWarning($"Encontrado os seguintes erros de regra de negócio \n {errosValidacao}");
                return errosValidacao;
            }
            else if (!existeFornecedor)
            {
                return "Não existe fornecedor com esse código no sistema. Por favor cadastre esse fornecedor ou informe outro código";
            }
            else
            {
                try
                {
                    _log.LogInformation("Editando o produto na base de dados");
                    _repositorio.Editar(produtoEditado);

                    return "";
                }
                catch (Exception ex)
                {
                    _log.LogCritical($"Ocorreu um erro ao editar esse produto na base de dados. Erro : {ex.Message}");
                    return $"Ocorreu um erro ao tentar atualizar esse produto. Erro : {ex.Message}";
                }
            }
        }

        public ProdutoDTO BuscarPorId(int id)
        {
            _log.LogInformation($"Buscando o produto pelo código : {id}");
            var produtoEncontrado = _mapper.Map<ProdutoDTO>(_repositorio.BuscarPorId(id));
            return produtoEncontrado;
        }

        public ProdutoLerDTO BuscarTodos(int skip, int take, int CodigoFornecedor, bool Ativo, bool Inativo)
        {
            if (skip < 0)
            {
                _log.LogWarning("propiedade SKIP informada com valor negativo. \n Adicionando o valor default de 0");
                skip = 0;
            }
            if (take < 0)
            {
                _log.LogWarning("propiedade TAKE informada com valor negativo. \n Adicionando o valor default de 10");
                take = 10;
            }

            if (Ativo && Inativo)
            {
                _log.LogInformation("foram informado os filtros de situação com o mesmo valor");
                return new ProdutoLerDTO { Mensagem = "Por favor selecione apenas um tipo de situação.", Produtos = null };
            }
            else if (CodigoFornecedor < 0)
            {
                _log.LogInformation("foi informado um código de fornecedor com valor negativo");
                return new ProdutoLerDTO { Mensagem = "O codigo do fornecedor filtrado não pode ser menor que 0.", Produtos = null };
            }
            else if (CodigoFornecedor != 0 && !Ativo && !Inativo)
            {
                _log.LogInformation("Realizado a busca de todos os produtos pelo filtro do codigo do fornecedor");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor).Skip(skip).Take(take)) };
            }
            else if (Ativo && CodigoFornecedor == 0)
            {
                _log.LogInformation("Realizado a busca de todos os produtos pelo filtro de situação ATIVO");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(Ativo).Skip(skip).Take(take)) };
            }
            else if (Inativo && CodigoFornecedor == 0)
            {
                _log.LogInformation("Realizado a busca de todos os produtos pelo filtro de situação INATIVO");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(!Inativo).Skip(skip).Take(take)) };
            }
            else if (CodigoFornecedor != 0 && Ativo)
            {
                _log.LogInformation("Realizado a busca de todos os produtos pelos filtros código do fornecedor e tipo de situação com ATIVO");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor, Ativo).Skip(skip).Take(take)) };
            }
            else if (CodigoFornecedor != 0 && Inativo)
            {
                _log.LogInformation("Realizado a busca de todos os produtos pelos filtros código do fornecedor e tipo de situação com INATIVO");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor, !Inativo).Skip(skip).Take(take)) };
            }
            else
            {
                _log.LogInformation("Realizado a busca de todos os produtos");
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos().Skip(skip).Take(take)) };
            }
        }

        public string Deletar(int id)
        {
            _log.LogInformation($"Removendo o produto pelo código : {id}");
            var codigoRetorno = _repositorio.Deletar(id);

            if (codigoRetorno == -1)
                return "Produto não encontrado com esse código";
            else if (codigoRetorno == -2)
                return "Esse produto já foi removido do sistema";
            else
                return "";
        }
    }
}
