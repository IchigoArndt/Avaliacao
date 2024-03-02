using System;

namespace Avaliacao.Aplicacao.Produtos.DTO
{
    public class ProdutoAdicionarDTO
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int CodigoFornecedor { get; set; }
    }
}
