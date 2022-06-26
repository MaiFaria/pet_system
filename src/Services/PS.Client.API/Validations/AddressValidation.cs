using FluentValidation;
using PS.Client.API.Models;

namespace PS.Client.API.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
        }
    }
}
