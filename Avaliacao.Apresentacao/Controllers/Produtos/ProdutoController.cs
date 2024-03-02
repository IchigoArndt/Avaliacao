using AutoMapper;
using Avaliacao.Aplicacao.Produtos;
using Avaliacao.Aplicacao.Produtos.DTO;
using Avaliacao.Aplicacao.Produtos.Mapping;
using Avaliacao.Dominio.Produtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Apresentacao.Controllers.Produtos
{
    [ApiController]
    [Route("api/Avaliacao/v1/[controller]")]
    public class ProdutoController : ControllerBase
    {
        IProdutoServico _servico;

        public ProdutoController(IProdutoServico servico)
        {
            _servico = servico;
        }

        [HttpPost("AdicionarProduto")]
        public IActionResult AdicionarProduto(ProdutoAdicionarDTO produtoDTO)
        {
            var sucesso = _servico.Adicionar(produtoDTO);

            if (string.IsNullOrEmpty(sucesso))
                return Ok("Produto Adicionado com sucesso !");
            else
                return BadRequest(sucesso);
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var produto = _servico.BuscarPorId(id);

            if (produto != null)
                return Ok(produto);
            else
                return BadRequest("produto não encontrado");
        }

        [HttpGet("BuscarTodos")]
        public IActionResult BuscarTodos([FromQuery]int skip = 0, 
                                                   [FromQuery]int take = 10, 
                                                   [FromQuery]int CodigoFornecedor = 0, 
                                                   [FromQuery]bool Ativo = false,
                                                   [FromQuery] bool Inativo = false)
        {
            var retorno = _servico.BuscarTodos(skip, take, CodigoFornecedor, Ativo, Inativo);

            if (!string.IsNullOrEmpty(retorno.Mensagem))
                return BadRequest(retorno);
            else
                return Ok(retorno.Produtos);
        }

        [HttpPut("EditarProduto/{id}")]
        public IActionResult EditarProduto(int id, ProdutoAdicionarDTO produtoDTO)
        {
            var sucesso = _servico.Alterar(id, produtoDTO);

            if (string.IsNullOrEmpty(sucesso))
                return Ok("Produto alterado com sucesso");
            else
                return BadRequest(sucesso);
        }

        [HttpDelete("RemoverProduto/{id}")]
        public IActionResult RemoverProduto(int id)
        {
            var Sucesso = _servico.Deletar(id);

            if (string.IsNullOrEmpty(Sucesso))
                return Ok("Produto removido com sucesso");
            else
                return BadRequest(Sucesso);
        }
    }
}
