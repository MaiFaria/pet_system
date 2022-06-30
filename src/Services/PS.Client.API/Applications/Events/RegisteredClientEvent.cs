using PS.Core.Messages;

namespace PS.Client.API.Applications.Events
{
    public class RegisteredClientEvent : Event
    {
        public Guid Id { get; private set; }
        public string name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisteredClientEvent(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            this.name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}
