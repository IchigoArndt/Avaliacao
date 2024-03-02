using Avaliacao.Dominio.Fornecedores;
using Avaliacao.Dominio.Produtos.Validadores;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Avaliacao.Dominio.Produtos
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public List<string> Validate()
        {
            List<string> erros = new List<string>();

            ValidadorProduto validator = new ValidadorProduto();

            ValidationResult results = validator.Validate(this);

            results.Errors.ForEach(error =>
            {
                erros.Add(error.ErrorMessage);
            });

            return erros;
        }
    }
}
