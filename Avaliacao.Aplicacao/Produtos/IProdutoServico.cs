using Avaliacao.Aplicacao.Produtos.DTO;
using System.Collections.Generic;

namespace Avaliacao.Aplicacao.Produtos
{
    public interface IProdutoServico
    {
        public string Adicionar(ProdutoAdicionarDTO produtoDTO);
        public string Alterar(int id, ProdutoAdicionarDTO produtoDTO);
        public ProdutoDTO BuscarPorId(int id);
        public ProdutoLerDTO BuscarTodos(int skip, int take, int CodigoFornecedor, bool Ativo, bool Inativo);
        public string Deletar(int id);
    }
}
