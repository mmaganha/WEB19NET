using Dominio.Modelos;
using FluentValidation;


namespace ProjetoWEB19NET.Dominio.ValidacaoModels
{
    public class CategoriaValidacao : AbstractValidator<Categoria>
    {
        public CategoriaValidacao()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Campo descrição é obrigatório.")
                .Length(3, 250).WithMessage("Campo mínimo 3 e máximo 250 caracteres");
        }
    }
}
