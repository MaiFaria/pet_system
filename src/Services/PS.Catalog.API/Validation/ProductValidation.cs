using FluentValidation;
using PS.Catalog.API.Models;
using static PS.Core.Helpers.ResourceString;

namespace PS.Catalog.API.Validation
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(row => row.Name)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_NOME_OBRIGATORIO);

            RuleFor(row => row.Description)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_DESCRICAO_OBRIGATORIO);

            RuleFor(row => row.Price)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_PRECO_OBRIGATORIO);

            RuleFor(row => row.DateRegister)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_DATAREGISTRO_OBRIGATORIO);

            RuleFor(row => row.Image)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_IMAGEM_OBRIGATORIO);

            RuleFor(row => row.QuantityStock)
               .NotEmpty()
               .WithMessage(Mensagens.MSG_QUANTIDADEESTOQUE_OBRIGATORIO);
        }
    }
}
