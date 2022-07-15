using Bogus;
using PS.Cart.Models;
using PS.Client.API.Models;
using PS.Core.DomainObjects;
using PS.Tests.Utils;

namespace PS.Tests.Helpers
{
    public static class FakerHelper
    {
        public static Guid GenerateGuid(this Faker faker)
        {
            return Guid.NewGuid();
        }

        public static decimal GenerateId(this Faker faker)
        {
            return faker.Random.Number(min: 1, max: (10 ^ 18 - 1));
        }

        public static List<decimal?> GenerateListIds(this Faker faker)
        {
            return faker.Make<decimal?>(count: faker.Random.Number(min: 1, max: 10), action: () => GenerateId(faker)).ToList();
        }

        public static bool GenerateTipoBool(this Faker faker)
        {
            return faker.Random.Bool();
        }

        public static Email GenerateEmail(this Faker faker)
        {
            var email = new Email();
            email.Address = faker.GenerateEmailString();

            return email;
        }

        public static string GenerateEmailString(this Faker faker)
        {
            return faker.Internet.Email();
        }

        #region Client
        public static string GenerateName(this Faker faker)
        {
            return faker.Person.FullName;
        }

        public static string GenerateUserName(this Faker faker)
        {
            return faker.Person.UserName;
        }

        public static Cpf GenerateCPF(this Faker faker)
        {
            var cpf = new Cpf();
            cpf.Number = faker.GenerateCPFString();

            return cpf;
        }

        public static string GenerateCPFString(this Faker faker)
        {
            return faker.Random.Hash(11, false);
        }

        public static string GenerateCNPJ(this Faker faker)
        {
            return CNPJ.Gerar();
        }

        public static string GeneratePassword(this Faker faker)
        {
            return faker.Internet.Password();
        }

        public static PS.Client.API.Models.Client GenerateClient(this Faker faker)
        {
            var client = new PS.Client.API.Models.Client();
            client.Id = faker.GenerateGuid();
            client.Name = faker.Person.FullName;
            client.Email = faker.GenerateEmail();
            client.Cpf = faker.GenerateCPF();
            client.Exclused = faker.Random.Bool();

            return client;
        }
        #endregion

        #region Address 
        public static string GenerateAddressPublicPlace(this Faker faker)
        {
            return faker.Address.StreetName();
        }

        public static string GenerateAdressNumber(this Faker faker)
        {
            return faker.Address.BuildingNumber();
        }

        public static string GenerateAddressComplement(this Faker faker)
        {
            return faker.Person.Address.Suite;
        }

        public static string GenerateAddressDistrict(this Faker faker)
        {
            return faker.Address.State();
        }

        public static string GenerateAddressCity(this Faker faker)
        {
            return faker.Address.City();
        }

        public static string GenerateAddressState(this Faker faker)
        {
            return faker.Address.State();
        }

        public static string GenerateAddressCep(this Faker faker)
        {
            return faker.Person.Address.ZipCode;
        }

        public static Address GenerateAddress(this Faker faker)
        {
            var address = new Address();
            address.PublicPlace = faker.Person.Address.Street;
            address.Number = faker.Random.AlphaNumeric(5);
            address.Complement = faker.Person.Address.Suite;
            address.District = faker.Address.State();
            address.City = faker.Address.City();
            address.State = faker.Address.State();
            address.Cep = faker.Person.Address.ZipCode;
            address.ClientId = faker.Random.Guid();

            return address;
        }

        public static string GenerateAddressString(this Faker faker)
        {
            return faker.Address.FullAddress();
        }
        #endregion
    }
}
