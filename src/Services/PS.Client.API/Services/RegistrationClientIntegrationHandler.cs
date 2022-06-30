using FluentValidation.Results;
using PS.Client.API.Applications.Commands;
using PS.Core.Mediator;
using PS.Core.Messages.Integration;
using PS.MessageBus;

namespace PS.Client.API.Services
{
    public class RegistrationClientIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistrationClientIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponse()
        {
            _bus.RespondAsync<RegisteredUserIntegrationEvent, ResponseMessage>(async request =>
                await RegisterClient(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponse();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponse();
        }

        private async Task<ResponseMessage> RegisterClient(RegisteredUserIntegrationEvent message)
        {
            var clienteCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
