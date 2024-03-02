using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Produtos.DTO
{
    public class ProdutoLerDTO
    {
        public string Mensagem { get; set; } = "";
        public IEnumerable<ProdutoDTO> Produtos { get; set; }
    }
}
