using Avaliacao.Dominio.Fornecedores;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacao.Repositorio.Repositorios.FornecedoresRepositorio
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        protected readonly Context Db;
        protected readonly DbSet<Fornecedor> DbSet;

        public FornecedorRepositorio(Context db)
        {
            Db = db;

            DbSet = db.Set<Fornecedor>();
        }

        public int Adicionar(Fornecedor item)
        {
            DbSet.Add(item);

            return SaveChanges();
        }

        public Fornecedor BuscarPorId(int id)
        {
            return DbSet.FirstOrDefault(fornecedor => fornecedor.Codigo == id);
        }

        public IEnumerable<Fornecedor> BuscarTodos()
        {
            return DbSet.ToList();
        }

        public IEnumerable<Fornecedor> BuscarTodos(string CNPJ)
        {
            return DbSet.ToList().Where(fornecedor => fornecedor.CNPJ == CNPJ);
        }

        public int Deletar(int id)
        {
            var fornecedor = BuscarPorId(id);

            if(fornecedor == null) 
                return -1;
            else
            {
                Db.Remove(fornecedor);
                return SaveChanges();
            }
        }

        public int Editar(Fornecedor item)
        {
            DbSet.Update(item);

            return SaveChanges();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public bool verificaSeExisteFornecedor(int id)
        {
            var fornecedor = BuscarPorId(id);

            if (fornecedor != null)
                return true;
            else
                return false;
        }
    }
}
