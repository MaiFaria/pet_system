using Microsoft.EntityFrameworkCore;
using PS.Cart.Data;
using PS.MessageBus;
using PS.MessageBus.Integration;

namespace PS.Cart.Services
{
    public class CartIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CartIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PedidoRealizadoIntegrationEvent>("PedidoRealizado", async request =>
                await ApagarCarrinho(request));
        }

        private async Task ApagarCarrinho(PedidoRealizadoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CartContext>();

            var carrinho = await context.CustomerCart
                .FirstOrDefaultAsync(c => c.ClienteId == message.ClienteId);

            if (carrinho != null)
            {
                context.CustomerCart.Remove(carrinho);
                await context.SaveChangesAsync();
            }
        }
    }
}