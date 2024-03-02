using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Dominio.Produtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Repositorio.Repositorios.ProdutosRepositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        protected readonly Context Db;
        protected readonly DbSet<Produto> DbSet;

        public ProdutoRepositorio(Context db)
        {
            Db = db;

            DbSet = db.Set<Produto>();
        }

        public int Adicionar(Produto item)
        {
            DbSet.Add(item);

            return SaveChanges();
        }

        public Produto BuscarPorId(int id)
        {
            return DbSet.FirstOrDefault(produto => produto.Codigo == id);
        }

        public IEnumerable<Produto> BuscarTodos()
        {
            return DbSet.ToList();
        }

        public IEnumerable<Produto> BuscarTodos(int idFornecedor)
        {
            return DbSet.ToList().Where(produto => produto.FornecedorId == idFornecedor);
        }

        public IEnumerable<Produto> BuscarTodos(bool situacao)
        {
            return DbSet.ToList().Where(produto => produto.Situacao == situacao);
        }

        public IEnumerable<Produto> BuscarTodos(int idFornecedor, bool situacao)
        {
            return DbSet.ToList().Where(produto => produto.Situacao == situacao && produto.FornecedorId == idFornecedor);
        }



        public int Deletar(int id)
        {
            var produto = BuscarPorId(id);

            if(produto == null)
                return -1;
            else if(produto.Situacao == false)
                return -2;
            else
            {
                produto.Situacao = false;
                Editar(produto);
                return SaveChanges();
            }
        }

        public int Editar(Produto item)
        {
            DbSet.Update(item);

            return SaveChanges();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
    }
}
