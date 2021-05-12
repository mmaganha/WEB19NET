using Dominio.Modelos;
using FluentValidation;

namespace ProjetoWEB19NET.Dominio.ValidacaoModels
{
    public class ReceitaValidacao : AbstractValidator<Receita>
    {
        public ReceitaValidacao()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Campo titulo é obrigatório.")
                .Length(3, 250).WithMessage("Campo mínimo 3 e máximo 250 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Campo descrição é obrigatório.")
                .Length(3, 250).WithMessage("Campo mínimo 3 e máximo 250 caracteres");

            RuleFor(x => x.Ingredientes)
                .NotEmpty().WithMessage("Campo ingredientes é obrigatório.")
                .MinimumLength(6).WithMessage("Campo mínimo 6 caracteres");

            RuleFor(x => x.ModoPreparo)
                .NotEmpty().WithMessage("Campo Modo de Preparo é obrigatório.")
                .MinimumLength(6).WithMessage("Campo mínimo 6 caracteres");

            RuleFor(x => x.IdCategoria)
                .GreaterThan(0).WithMessage("Campo categoria é obrigatório.");
        }
    }
}
