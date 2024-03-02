using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.Dominio.Fornecedores.Validadores
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(fornecedor => fornecedor.Descricao).NotEmpty().WithMessage("A descrição do fornecedor não pode ser vazia")
                                                 .MaximumLength(100).WithMessage("A descrição do fornecedor não pode ultrapassar 100 caracteres")
                                                 .MinimumLength(10).WithMessage("A descrição do fornecedor deve conter no minimo 10 caracteres");

            RuleFor(fornecedor => fornecedor.CNPJ).NotEmpty().WithMessage("O CNPJ do fornecedor não pode ser vazio")
                                     .MaximumLength(14).WithMessage("O CNPJ do fornecedor não pode ultrapassar os 16 caracteres");
        }
    }
}
