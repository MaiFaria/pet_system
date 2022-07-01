using PS.Core.Data;

namespace PS.Catalog.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string query = null);
        Task<Product> GetById(Guid id);
        Task<List<Product>> GetProductById(string ids);

        void AddProduct(Product produto);
        void Refresh(Product produto);
    }
}