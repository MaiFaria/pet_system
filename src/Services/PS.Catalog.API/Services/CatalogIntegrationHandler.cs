using PS.Catalog.API.Models;
using PS.Core.DomainObjects;
using PS.MessageBus;
using PS.MessageBus.Integration;

namespace PS.Catalog.API.Services
{
    public class CatalogIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
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
            _bus.SubscribeAsync<PedidoAutorizadoIntegrationEvent>("PedidoAutorizado", async request =>
                await BaixarEstoque(request));
        }

        private async Task BaixarEstoque(PedidoAutorizadoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var produtosComEstoque = new List<Product>();
                var produtoRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                var idsProdutos = string.Join(",", message.Itens.Select(c => c.Key));
                var produtos = await produtoRepository.GetProductById(idsProdutos);

                if (produtos.Count != message.Itens.Count)
                {
                    CancelarPedidoSemEstoque(message);
                    return;
                }

                foreach (var produto in produtos)
                {
                    var quantidadeProduto = message.Itens.FirstOrDefault(p => p.Key == produto.Id).Value;

                    if (produto.IsAvaiable(quantidadeProduto))
                    {
                        produto.WithdrawStock(quantidadeProduto);
                        produtosComEstoque.Add(produto);
                    }
                }

                if (produtosComEstoque.Count != message.Itens.Count)
                {
                    CancelarPedidoSemEstoque(message);
                    return;
                }

                foreach (var produto in produtosComEstoque)
                {
                    produtoRepository.Refresh(produto);
                }

                if (!await produtoRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problemas ao atualizar estoque do pedido {message.PedidoId}");
                }

                var pedidoBaixado = new PedidoBaixadoEstoqueIntegrationEvent(message.ClienteId, message.PedidoId);
                await _bus.PublishAsync(pedidoBaixado);
            }
        }

        public async void CancelarPedidoSemEstoque(PedidoAutorizadoIntegrationEvent message)
        {
            var pedidoCancelado = new PedidoCanceladoIntegrationEvent(message.ClienteId, message.PedidoId);
            await _bus.PublishAsync(pedidoCancelado);
        }
    }
}