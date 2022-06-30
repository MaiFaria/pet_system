using FluentValidation.Results;
using MediatR;
using PS.Client.API.Applications.Events;
using PS.Client.API.Models;
using PS.Core.Messages;

namespace PS.Client.API.Applications.Commands
{
    public class ClientCommandHandler : CommandHandler,
        IRequestHandler<RegisterClientCommand, ValidationResult>,
        IRequestHandler<AddAddressCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var cliente = new Models.Client(message.Id, message.Name, message.Email, message.Cpf);

            var clienteExistente = await _clienteRepository.GetByCpf(cliente.Cpf.Number);

            if (clienteExistente != null)
            {
                AddErrors("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clienteRepository.Add(cliente);

            cliente.AddEvent(new RegisteredClientEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistsData(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AddAddressCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var endereco = new Address(message.PublicPlace, message.Number, message.Complement, message.District, message.Cep, message.City, message.State, message.ClientId);
            _clienteRepository.AddAddress(endereco);

            return await PersistsData(_clienteRepository.UnitOfWork);
        }
    }
}
