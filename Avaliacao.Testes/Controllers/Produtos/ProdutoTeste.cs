using AutoMapper;
using Avaliacao.Aplicacao.Fornecedores;
using Avaliacao.Aplicacao.Produtos;
using Avaliacao.Aplicacao.Produtos.DTO;
using Avaliacao.Aplicacao.Produtos.Mapping;
using Avaliacao.Apresentacao.Controllers.Produtos;
using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Dominio.Produtos;
using Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio;
using Avaliacao.Repositorio.Repositorios.ProdutosRepositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace Avaliacao.Testes.Controllers.Produtos
{
    public class ProdutoTeste
    {
        private Mock<IProdutoRepositorio> _repositorioMock;
        private Mock<IFornecedorRepositorio> _repositorioFornecedorMock;
        ProdutoController _controller;
        ProdutoServico _servico;
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _repositorioMock = new Mock<IProdutoRepositorio>();

            var mapperProfile = new ProdutoMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            _mapper = new Mapper(configuration);
            
            _repositorioFornecedorMock = new Mock<IFornecedorRepositorio>();

            _repositorioFornecedorMock.Setup(repositorio => repositorio.BuscarPorId(1)).Returns(new Fornecedor
            {
                CNPJ = "49035354000160",
                Codigo = 1,
                Descricao = "esse é um otimo fornecedor"
            });

            _repositorioMock.Setup(repositorio => repositorio.Adicionar(new Produto
            {
                Codigo = 1,
                FornecedorId = 1,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddMonths(1),
                Descricao = "Teste unitario"
            })).Returns(0);

            _servico = new ProdutoServico(_repositorioMock.Object, _repositorioFornecedorMock.Object, _mapper);
            _controller = new ProdutoController(_servico);
        }

        [Test]
        public void ProdutoController_adicionar_produto_sucesso()
        {
            IActionResult resultado = _controller.AdicionarProduto(new ProdutoAdicionarDTO
            {
                Ativo = true,
                CodigoFornecedor = 1,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddMonths(1),
                Descricao = "Teste unitario"
            });

            var resultadoSucesso = resultado as OkObjectResult;

            Assert.IsNotNull(resultadoSucesso);
            Assert.AreEqual(200, resultadoSucesso.StatusCode);
        }
    }
}
