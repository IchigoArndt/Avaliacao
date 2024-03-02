using Avaliacao.Dominio.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio
{
    public interface IFornecedorRepositorio : IRepositorio<Fornecedor>
    {
        public bool verificaSeExisteFornecedor(int id);
        public IEnumerable<Fornecedor> BuscarTodos(string CNPJ);
    }
}
