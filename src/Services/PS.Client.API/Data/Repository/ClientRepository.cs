using Microsoft.EntityFrameworkCore;
using PS.Client.API.Models;
using PS.Core.Data;

namespace PS.Client.API.Data.Repository
{
    public class ClientRepository : IClienteRepository
    {
        private readonly ClientsContext _context;

        public ClientRepository(ClientsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Models.Client>> GetAll()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public Task<Models.Client> GetByCpf(string cpf)
        {
            return _context.Clients.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Add(Models.Client client)
        {
            _context.Clients.Add(client);
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(e => e.ClientId == id);
        }

        public void AddAddress(Address endereco)
        {
            _context.Addresses.Add(endereco);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
