using Bycoders.DesafioDev.App.Models;
using Bycoders.DesafioDev.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bycoders.DesafioDev.App.Extensions
{
    public static class DepndencyInjectionExtenstions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var appsettings = configuration.Get<Appsettings>();
            services.AddSingleton(appsettings);

            services.AddHttpClient<IImportaHttpRepository, ImportaHttpRepository>();
        }
    }
}
