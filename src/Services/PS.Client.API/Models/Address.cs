using PS.Client.API.Validations;
using PS.Core.DomainObjects;

namespace PS.Client.API.Models
{
    public class Address : Entity
    {
        public string? PublicPlace { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? District { get; set; }
        public string? Cep { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public Guid ClientId { get; set; }

        #region EF Relation
        public Client? Client { get; set; }
        public Address() { }
        #endregion

        public Address(string publicPlace, string number, string complement, string district, string cep, string city, string state, Guid clientId)
        {
            PublicPlace = publicPlace;
            Number = number;
            Complement = complement;
            District = district;
            Cep = cep;
            City = city;
            State = state;
            ClientId = clientId;
        }

        public async Task ValidateForPersistence()
        {
            ValidationResult = await new AddressValidation().ValidateAsync(this);
        }
    }
}
