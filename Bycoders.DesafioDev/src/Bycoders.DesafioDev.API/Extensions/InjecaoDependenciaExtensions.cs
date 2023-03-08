using Bycoders.DesafioDev.API.Configurations;
using Bycoders.DesafioDev.API.Data.Repository;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Bycoders.DesafioDev.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class InjecaoDependenciaExtensions
    {
        public static void RegistrarDependencias(this IServiceCollection services, IConfiguration configuration)
        {   
            var cnabConfiguracoes = configuration.Get<CnabConfiguracoes>();
            services.AddSingleton<CnabConfiguracoes>(cnabConfiguracoes);

            string _conexao = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            services.AddDbContext<TransacoesFinanceirasContext>(options
                => options
                        .UseSqlServer(_conexao)
                        .LogTo(Console.WriteLine)
                        .EnableSensitiveDataLogging());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransacaoFinanceiraRepository, TransacaoFinanceiraRepository>();
            services.AddScoped<ITipoTransacaoRepository, TipoTransacaoRepository>();
                        
            services.AddScoped<ITransacaoFinanceiraService, TransacaoFinanceiraService>();
        }
    }
}
