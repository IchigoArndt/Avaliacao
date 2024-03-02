using Avaliacao.Aplicacao.Fornecedores;
using Avaliacao.Aplicacao.Fornecedores.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Avaliacao.Apresentacao.Controllers.Fornecedores
{
    [ApiController]
    [Route("api/Avaliacao/v1/[controller]")]
    public class FornecedorController : ControllerBase
    {
        IFornecedorServico _servico;

        public FornecedorController(IFornecedorServico servico)
        {
            _servico = servico;
        }

        [HttpPost("AdicionarFornecedor")]
        public IActionResult AdicionarFornecedor(FornecedorAdicionarDTO fornecedorDTO)
        {
            var sucesso = _servico.Adicionar(fornecedorDTO);

            if (string.IsNullOrEmpty(sucesso))
                return Ok("Fornecedor Adicionado com sucesso !");
            else
                return BadRequest(sucesso);
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var fornecedor = _servico.BuscarPorId(id);

            if (fornecedor != null)
                return Ok(fornecedor);
            else
                return BadRequest("Fornecedor não encontrado");
        }

        [HttpGet("BuscarTodos")]
        public IActionResult BuscarTodos([FromQuery] int skip = 0,
                                           [FromQuery] int take = 10,
                                           [FromQuery] string CNPJ = "")
        {
            var retorno = _servico.BuscarTodos(skip, take, CNPJ);

            if (!string.IsNullOrEmpty(retorno.Mensagem))
                return BadRequest(retorno);
            else
                return Ok(retorno.Fornecedores);
        }

        [HttpPut("EditarFornecedor/{id}")]
        public IActionResult EditarFornecedor(int id, FornecedorAdicionarDTO fornecedorDTO)
        {
            var sucesso = _servico.Alterar(id, fornecedorDTO);

            if (string.IsNullOrEmpty(sucesso))
                return Ok("Fornecedor alterado com sucesso");
            else
                return BadRequest(sucesso);
        }

        [HttpDelete("RemoverFornecedor/{id}")]
        public IActionResult RemoverFornecedor(int id)
        {
            var Sucesso = _servico.Deletar(id);

            if (string.IsNullOrEmpty(Sucesso))
                return Ok("Fornecedor removido com sucesso");
            else
                return BadRequest(Sucesso);
        }
    }
}
