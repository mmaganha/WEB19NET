using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace ProjetoWEB19NET.Dominio.ValidacaoModels
{
    public class Entidade
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Entity"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        public bool Valid { get; set; }

        /// <summary>
        /// Gets or sets the validation result.
        /// </summary>
        /// <value>
        /// The validation result.
        /// </value>
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// Gets or sets the lista.
        /// </summary>
        /// <value>
        /// The lista.
        /// </value>
        public List<Notificacao> Lista { get; set; } = new List<Notificacao>();

        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="validator">The validator.</param>
        /// <returns>
        /// True: Válido.
        /// False: Inválido.
        /// </returns>
        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
            where TModel : class
        {
            this.ValidationResult = validator.Validate(model);

            if (!this.ValidationResult.IsValid)
            {
                foreach (var failure in this.ValidationResult.Errors)
                {
                    Notificacao notificacao = new Notificacao
                    {
                        Propriedade = failure.PropertyName,
                        Erro = failure.ErrorMessage,
                    };

                    this.Lista.Add(notificacao);
                }
            }

            return this.Valid = this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Adicionars the erro validation.
        /// </summary>
        /// <returns>Lista com erros encontrados.</returns>
        public List<Notificacao> AdicionarErroValidation()
        {
            return this.Lista;
        }

        /// <summary>
        /// Adicionars the erro validation.
        /// </summary>
        /// <param name="propriedade">The propriedade.</param>
        /// <param name="erro">The erro.</param>
        /// <returns>Lista com erros encontrados.</returns>
        public List<Notificacao> AdicionarErroValidation(string propriedade, string erro)
        {
            Notificacao notificacao = new Notificacao
            {
                Propriedade = propriedade,
                Erro = erro,
            };
            this.Lista.Add(notificacao);
            return this.Lista;
        }
    }
}
