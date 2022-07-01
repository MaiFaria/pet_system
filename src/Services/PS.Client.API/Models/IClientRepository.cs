using PS.Core.Data;

namespace PS.Client.API.Models
{
    public interface IClienteRepository : IRepository<Client>
    {
        void AddCLient(Client cliente);

        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetByCpf(string cpf);

        void AddAddress(Address address);
        Task<Address> GetAddressById(Guid id);
    }
}
