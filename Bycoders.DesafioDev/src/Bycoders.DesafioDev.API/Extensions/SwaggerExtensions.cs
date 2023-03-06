using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bycoders.DesafioDev.API", Version = "v1" });
            });
        }
    }
}
