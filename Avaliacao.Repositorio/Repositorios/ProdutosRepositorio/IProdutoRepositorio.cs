using Avaliacao.Dominio.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Repositorio.Repositorios.ProdutosRepositorio
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        public IEnumerable<Produto> BuscarTodos(int idFornecedor);

        public IEnumerable<Produto> BuscarTodos(bool situacao);

        public IEnumerable<Produto> BuscarTodos(int idFornecedor, bool situacao);
    }
}
