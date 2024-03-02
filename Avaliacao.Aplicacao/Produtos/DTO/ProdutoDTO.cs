using System;

namespace Avaliacao.Aplicacao.Produtos.DTO
{
    public class ProdutoDTO
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public string DataValidade { get; set; }
        public string DataFabricacao { get; set; }
        public int CodigoFornecedor { get; set; }
    }
}
