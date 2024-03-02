using FluentValidation;
using System;

namespace Avaliacao.Dominio.Produtos.Validadores
{
    public class ValidadorProduto : AbstractValidator<Produto>
    {
        public ValidadorProduto()
        {
            RuleFor(produto => produto.Descricao).NotEmpty().WithMessage("A descrição do produto não pode ser vazia")
                                                 .MaximumLength(100).WithMessage("A descrição do produto não pode ultrapassar 100 caracteres")
                                                 .MinimumLength(10).WithMessage("A descrição do produto deve conter no minimo 10 caracteres");

            RuleFor(produto => produto.Situacao).NotEmpty().WithMessage("Por favor, informe a situação do produto ( Ativo / Desativado )");

            RuleFor(produto => produto.DataFabricacao).NotEmpty().WithMessage("A data de fabricação do produto não pode ficar vazia")
                                                      .LessThan(produtoValidade => produtoValidade.DataValidade).WithMessage("a data de fabricação não pode ser maior ou igual a data de validade");

            RuleFor(produto => produto.DataValidade).NotEmpty().WithMessage("A data de validade não pode ficar vazia");
        }
    }
}
