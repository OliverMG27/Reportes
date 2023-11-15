using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reportes.Controllers;

namespace Reportes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configura los servicios de la aplicación, como la inyección de dependencias.
            services.AddTransient<IRMortalidades, RMortalidades>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configura el middleware y la canalización de solicitud.
        }
    }
}

