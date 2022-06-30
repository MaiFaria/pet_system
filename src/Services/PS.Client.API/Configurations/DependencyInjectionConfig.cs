using FluentValidation.Results;
using MediatR;
using PS.Client.API.Applications.Commands;
using PS.Client.API.Applications.Events;
using PS.Client.API.Data;
using PS.Client.API.Data.Repository;
using PS.Client.API.Models;
using PS.Core.Mediator;
using PS.WebApi.Core.User;

namespace PS.Client.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<IRequestHandler<AddAddressCommand, ValidationResult>, ClientCommandHandler>();
            services.AddScoped<INotificationHandler<RegisteredClientEvent>, ClientEventHandler>();
            services.AddScoped<IClienteRepository, ClientRepository>();
            services.AddScoped<ClientsContext>();
        }
    }
}
