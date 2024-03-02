using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Dominio.Produtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avaliacao.Repositorio
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opts) : base(opts) { }
        public DbSet<Produto> produto { get; set; }
        public DbSet<Fornecedor> fornecedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(produto =>
            {
                produto.ToTable("tbProdutos");
                produto.HasKey(produto => produto.Codigo);
                produto.Property(produto => produto.Descricao).HasColumnType("VARCHAR(100)").IsRequired(true);
                produto.Property(produto => produto.Situacao).HasColumnType("BIT").IsRequired(true).HasDefaultValue(1);
                produto.Property(produto => produto.DataFabricacao).HasColumnType("DATETIME").IsRequired(true);
                produto.Property(produto => produto.DataValidade).HasColumnType("DATETIME").IsRequired(true);
                produto.Property(produto => produto.FornecedorId).HasColumnType("int").IsRequired(true);
            });
            modelBuilder.Entity<Fornecedor>(fornecedor =>
            {
                fornecedor.ToTable("tbFornecedores");
                fornecedor.HasKey(fornecedor => fornecedor.Codigo);
                fornecedor.Property(fornecedor => fornecedor.Descricao).HasColumnType("VARCHAR(100)").IsRequired(true);
                fornecedor.Property(fornecedor => fornecedor.CNPJ).HasColumnType("VARCHAR(16)").IsRequired(true);
            });
        }
    }
}
