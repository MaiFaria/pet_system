using FluentValidation.Results;
using PS.Core.Data;

namespace PS.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddErrors(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistsData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddErrors("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}
