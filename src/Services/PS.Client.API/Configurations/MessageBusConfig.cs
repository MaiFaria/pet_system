using PS.Client.API.Services;
using PS.Core.Helpers;
using PS.MessageBus;

namespace PS.Client.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistrationClientIntegrationHandler>();
        }
    }
}
