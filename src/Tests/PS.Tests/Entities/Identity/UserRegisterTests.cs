using PS.Tests.Builders.Identity;
using Xunit;
using Xunit.Abstractions;
using static PS.Identity.Models.UserViewModels;
using Assert = PS.Tests.Helpers.Assert;

namespace PS.Tests.Entities.Identity
{
    public class UserRegisterTests
    {
        private readonly UserRegisterBuilder _builder;
        private readonly ITestOutputHelper _output;

        public UserRegisterTests(ITestOutputHelper output)
        {
            _builder = new UserRegisterBuilder();
            _output = output;
        }

        [Fact(DisplayName = "#01 - Deve criar um model")]
        public void MustCreateModel()
        {
            var builder = _builder.New();
            var model = _builder.Build();

            Assert.Equal<UserRegister>(model, builder, _output);
        }
    }
}
