using Avaliacao.Aplicacao.Produtos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Fornecedores.DTO
{
    public class FornecedorLerDTO
    {
        public string Mensagem { get; set; } = "";
        public IEnumerable<FornecedorDTO> Fornecedores { get; set; }
    }
}
