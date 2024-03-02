using Avaliacao.Aplicacao.Fornecedores.DTO;
using Avaliacao.Aplicacao.Produtos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Aplicacao.Fornecedores
{
    public interface IFornecedorServico
    {
        public string Adicionar(FornecedorAdicionarDTO fornecedorDTO);
        public string Alterar(int id, FornecedorAdicionarDTO fornecedorDTO);
        public FornecedorDTO BuscarPorId(int id);
        public FornecedorLerDTO BuscarTodos(int skip, int take, string CNPJ);
        public string Deletar(int id);
    }
}
