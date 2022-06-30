using Bogus;
using PS.Client.API.Models;
using PS.Core.DomainObjects;
using PS.Tests.Helpers;

namespace PS.Tests.Builders.Client
{
    public class AddressBuilder
    {
        private readonly Faker? _faker;

        public string? PublicPlace { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? District { get; set; }
        public string? Cep { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public Guid ClientId { get; set; }
        public PS.Client.API.Models.Client Client { get; set;  }
        public Guid Id { get; set; }

        public AddressBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public AddressBuilder New()
        {
            this.PublicPlace = _faker.GenerateAddressPublicPlace();
            this.Number = _faker.GenerateAdressNumber();
            this.Complement = _faker.GenerateAddressComplement();
            this.District = _faker.GenerateAddressDistrict();
            this.Cep = _faker.GenerateAddressCep();
            this.City = _faker.GenerateAddressCity();    
            this.State = _faker.GenerateAddressState();
            this.ClientId = _faker.GenerateGuid();
            this.Id = _faker.GenerateGuid();

            return this;
        }

        public Address Build()
        {
            var model = new Address();

            if(this.PublicPlace != null)
                model.PublicPlace = this.PublicPlace;

            if (this.Number != null)
                model.Number = this.Number;

            if (this.Complement != null)
                model.Complement = this.Complement;

            if (this.District != null)
                model.District = this.District;

            if (this.Cep != null)
                model.Cep = this.Cep;

            if (this.City != null)
                model.City = this.City;

            if (this.State != null)
                model.State = this.State;

            if (this.ClientId != null)
                model.ClientId = this.ClientId;

            if (this.Id != null)
                model.Id = this.Id;

            return model;
        }
    }
}
