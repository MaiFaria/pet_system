using PS.Catalog.API.Data;
using PS.Catalog.API.Data.Repository;
using PS.Catalog.API.Models;

namespace PS.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}