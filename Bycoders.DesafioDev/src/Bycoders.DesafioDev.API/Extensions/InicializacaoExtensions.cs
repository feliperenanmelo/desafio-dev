using Bycoders.DesafioDev.API.Data.Repository;
using Bycoders.DesafioDev.API.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bycoders.DesafioDev.API.Extensions
{
    public static class InicializacaoExtensions
    {
        public static void InserirDadosIniciais(this IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<TransacoesFinanceirasContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.TipoTransacaos.AddRange(
                    TipoTransacao.Create("Débito", "Entrada", '+'),
                    TipoTransacao.Create("Boleto", "Saída", '-'),
                    TipoTransacao.Create("Financimento", "Saída", '-'),
                    TipoTransacao.Create("Crédito", "Entrada", '+'),
                    TipoTransacao.Create("Recebimento Empréstimo", "Entrada", '+'),
                    TipoTransacao.Create("Vendas", "Entrada", '+'),
                    TipoTransacao.Create("Recebimento TED", "Entrada", '+'),
                    TipoTransacao.Create("Recebimento DOC", "Entrada", '+'),
                    TipoTransacao.Create("Aluguel", "Saída", '-'));

                context.SaveChanges();
            }
        }
    }
}
