using Bogus;
using PS.Tests.Utils;

namespace PS.Tests.Helpers
{
    public static class FakerHelper
    {
        public static decimal GenerateId(this Faker faker)
        {
            return faker.Random.Number(min: 1, max: (10 ^ 18 - 1));
        }

        public static List<decimal?> GenerateListIds(this Faker faker)
        {
            return faker.Make<decimal?>(count: faker.Random.Number(min: 1, max: 10), action: () => GenerateId(faker)).ToList();
        }

        public static string GenerateName(this Faker faker)
        {
            return faker.Person.FullName;
        }

        public static string GenerateCPF(this Faker faker)
        {
            return CPF.Gerar();
        }

        public static string GenerateEmail(this Faker faker)
        {
            return faker.Internet.Email();
        }

        public static string GenerateCNPJ(this Faker faker)
        {
            return CNPJ.Gerar();
        }

        public static string GenerateUserName(this Faker faker)
        {
            return faker.Person.UserName;
        }

        public static string GeneratePassword(this Faker faker)
        {
            return faker.Internet.Password();
        }
    }
}
