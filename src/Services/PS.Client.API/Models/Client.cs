using PS.Client.API.Validations;
using PS.Core.DomainObjects;

namespace PS.Client.API.Models
{
    public class Client : Entity, IAggregateRoot
    {
        public string? Name { get; set; }
        public Email? Email { get; set; }
        public Cpf? Cpf { get; set; }
        public bool? Exclused { get; set; }
        public Address? Address { get; set; }

        #region EF Relation
        public Client() { }
        #endregion

        public Client(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Exclused = false;
        }

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

        public void AssignEndereco(Address endereco)
        {
            Address = endereco;
        }

        public async Task ValidateForPersistence()
        {
            ValidationResult = await new UserClientValidation().ValidateAsync(this);
        }
    }
}
