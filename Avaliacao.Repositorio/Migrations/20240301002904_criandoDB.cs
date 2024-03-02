using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Avaliacao.Repositorio.Migrations
{
    public partial class criandoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbFornecedores",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CNPJ = table.Column<string>(type: "VARCHAR(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFornecedores", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "tbProdutos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Situacao = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DataValidade = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProdutos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbFornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tbFornecedores",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_FornecedorId",
                table: "tbProdutos",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbProdutos");

            migrationBuilder.DropTable(
                name: "tbFornecedores");
        }
    }
}
