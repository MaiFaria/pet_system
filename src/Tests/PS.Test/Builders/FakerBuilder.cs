using Bogus;

namespace PS.Tests.Builders
{
    public class FakerBuilder
    {
        private static string? _language;

        public static FakerBuilder New()
        {
            _language = "pt_BR";

            return new FakerBuilder();
        }

        public FakerBuilder WithLanguage(string language)
        {
            _language = language;

            return this;
        }

        public Faker Build()
        {
            return new Faker(_language);
        }
    }
}
