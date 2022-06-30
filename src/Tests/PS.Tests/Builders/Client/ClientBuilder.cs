using Bogus;
using PS.Client.API.Models;
using PS.Core.DomainObjects;
using PS.Tests.Helpers;

namespace PS.Tests.Builders.Client
{
    public class ClientBuilder
    {
        private readonly Faker? _faker;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Email? Email { get; set; }
        public Cpf? Cpf { get; set; }
        public bool Exclused { get; set; }
        public Address? Address { get; set; }

        public ClientBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public ClientBuilder New()
        {
            this.Id = _faker.GenerateGuid();
            this.Name = _faker.GenerateName();
            this.Email = _faker.GenerateEmail();
            this.Cpf = _faker.GenerateCPF();
            this.Exclused = _faker.GenerateTipoBool();

            return this;
        }

        public PS.Client.API.Models.Client Build()
        {
            var model = new PS.Client.API.Models.Client();

            if (this.Id != null)
                model.Id = this.Id;

            if (this.Name != null)
                model.Name = this.Name;

            if (this.Email != null)
                model.Email = this.Email;

            if (this.Cpf != null)
                model.Cpf = this.Cpf;

            if (this.Exclused)
                model.Exclused = this.Exclused;

            return model;
        }
    }
}
