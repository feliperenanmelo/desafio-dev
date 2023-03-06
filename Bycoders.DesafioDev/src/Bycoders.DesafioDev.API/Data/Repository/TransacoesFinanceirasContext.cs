using Bycoders.DesafioDev.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bycoders.DesafioDev.API.Data.Repository
{
    public class TransacoesFinanceirasContext : DbContext
    {
        public DbSet<TransacaoFinanceira> TransacoesFinanceiras { get; set; }
        public DbSet<TipoTransacao> TipoTransacaos { get; set; }

        public TransacoesFinanceirasContext(DbContextOptions<TransacoesFinanceirasContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransacoesFinanceirasContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
