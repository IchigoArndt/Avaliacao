using Avaliacao.Dominio.Produtos;
using System;
using Avaliacao.Repositorio.Repositorios.ProdutosRepositorio;
using System.Collections.Generic;
using Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio;
using Avaliacao.Aplicacao.Produtos.DTO;
using AutoMapper;
using System.Linq;

namespace Avaliacao.Aplicacao.Produtos
{
    public class ProdutoServico : IProdutoServico
    {
        IProdutoRepositorio _repositorio;
        IFornecedorRepositorio _repositorioFornecedor;
        IMapper _mapper;

        public ProdutoServico(IProdutoRepositorio repositorio, IFornecedorRepositorio fornecedorRepositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _repositorioFornecedor = fornecedorRepositorio;
            _mapper = mapper;
        }

        public string Adicionar(ProdutoAdicionarDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);

            string errosValidacao = "";
            bool existeFornecedor;

            existeFornecedor = _repositorioFornecedor.verificaSeExisteFornecedor(produto.FornecedorId);

            var erros = produto.Validate();

            if (erros.Count > 0)
            {
                errosValidacao = "Não foi possivel inserir o produto, por que foi encontrado os seguintes erros :\n";
                erros.ForEach(erro =>
                {
                    errosValidacao += erro + "\n";
                });

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
                    _repositorio.Adicionar(produto);

                    return "";
                }
                catch (Exception ex)
                {
                    return $"Ocorreu um erro ao tentar inserir esse produto. Erro : {ex.Message}";
                }
            }
        }

        public string Alterar(int id, ProdutoAdicionarDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);

            var produtoAntigo = _repositorio.BuscarPorId(id);

            var produtoEditado = _mapper.Map<Produto>(produto);

            if (produtoAntigo == null)
                return "Produto não encontrado com esse código";
            else
                produtoEditado.Codigo = id;

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
                    _repositorio.Editar(produtoEditado);

                    return "";
                }
                catch (Exception ex)
                {
                    return $"Ocorreu um erro ao tentar atualizar esse produto. Erro : {ex.Message}";
                }
            }
        }

        public ProdutoDTO BuscarPorId(int id)
        {
            var produtoEncontrado = _mapper.Map<ProdutoDTO>(_repositorio.BuscarPorId(id));
            return produtoEncontrado;
        }

        public ProdutoLerDTO BuscarTodos(int skip, int take, int CodigoFornecedor, bool Ativo, bool Inativo)
        {
            if (skip < 0)
                skip = 0;
            if (take < 0)
                take = 10;

            if (Ativo && Inativo)
                return new ProdutoLerDTO { Mensagem = "Por favor selecione apenas um tipo de situação.", Produtos = null };
            else if (CodigoFornecedor < 0)
                return new ProdutoLerDTO { Mensagem = "O codigo do fornecedor filtrado não pode ser menor que 0.", Produtos = null };
            else if (CodigoFornecedor != 0 && !Ativo && !Inativo)
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor).Skip(skip).Take(take)) };
            else if (Ativo && CodigoFornecedor == 0)
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(Ativo).Skip(skip).Take(take)) };
            else if (Inativo && CodigoFornecedor == 0)
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(!Inativo).Skip(skip).Take(take)) };
            else if (CodigoFornecedor != 0 && Ativo)
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor, Ativo).Skip(skip).Take(take)) };
            else if (CodigoFornecedor != 0 && Inativo)
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos(CodigoFornecedor, !Inativo).Skip(skip).Take(take)) };
            else
                return new ProdutoLerDTO { Mensagem = "", Produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(_repositorio.BuscarTodos().Skip(skip).Take(take)) };
        }

        public string Deletar(int id)
        {
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
