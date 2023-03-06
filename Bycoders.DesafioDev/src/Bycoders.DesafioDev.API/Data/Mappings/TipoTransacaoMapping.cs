using Bycoders.DesafioDev.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bycoders.DesafioDev.API.Data.Mappings
{
    public class TipoTransacaoMapping : IEntityTypeConfiguration<TipoTransacao>
    {
        public void Configure(EntityTypeBuilder<TipoTransacao> builder)
        {
            builder.ToTable("TipoTransacao");

            builder.HasKey(tipo => tipo.Id);

            builder
                .Property(tipo => tipo.Descricao)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(tipo => tipo.Natureza)
                .HasColumnType("varchar")
                .HasMaxLength(7)
                .IsRequired();

            builder
                .Property(tipo => tipo.Sinal)
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}
