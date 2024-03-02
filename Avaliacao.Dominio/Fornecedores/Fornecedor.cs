using Avaliacao.Dominio.Fornecedores.Validadores;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Avaliacao.Dominio.Fornecedores
{
    public class Fornecedor
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string CNPJ { get; set; }

        public List<string> Validate()
        {
            List<string> erros = new List<string>();

            ValidadorFornecedor validator = new ValidadorFornecedor();

            ValidationResult results = validator.Validate(this);

            results.Errors.ForEach(error =>
            {
                erros.Add(error.ErrorMessage);
            });

            return erros;
        }
    }
}
