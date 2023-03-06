using Bycoders.DesafioDev.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bycoders.DesafioDev.API.Data.Mappings
{
    public class TransacaoFinanceiraMapping : IEntityTypeConfiguration<TransacaoFinanceira>
    {
        public void Configure(EntityTypeBuilder<TransacaoFinanceira> builder)
        {
            builder.ToTable("TransacaoFinanceira");

            builder.HasKey(tran => tran.Id);

            builder
                .Property(tran => tran.Data)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(tran => tran.Valor)
                .HasColumnType("decimal(14,2)")
                .IsRequired();

            builder
                .Property(tran => tran.CPF)
                .HasColumnType("varchar")
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property(tran => tran.Cartao)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(tran => tran.Hora)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

            builder
                .Property(tran => tran.NomeLoja)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(tran => tran.Dono)
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasOne(tipo => tipo.TipoTransacao)
                .WithMany(tran => tran.TransacaosFinanceiras)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
