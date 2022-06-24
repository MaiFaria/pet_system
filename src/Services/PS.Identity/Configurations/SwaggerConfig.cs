using Microsoft.OpenApi.Models;

namespace PS.Identity.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(x => x.First());

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Pet System Identity API =)",
                    Description = "API de Identidade da aplicação Pet System.",
                    Contact = new OpenApiContact() { Name = "Maiara Faria", Email = "mf.mai@hotmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });

            return app;
        }
    }
}
