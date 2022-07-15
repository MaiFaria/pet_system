using PS.Cart.Services;
using PS.Core.Helpers;
using PS.MessageBus;

namespace PS.Cart.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CartIntegrationHandler>();
        }
    }
}