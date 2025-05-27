using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas");

            builder.HasKey(x => x.NumeroConta);

            builder.Property(x => x.NumeroConta)
                   .UseMySqlIdentityColumn()
                   .HasColumnName("Numero");

            builder.Property(x => x.Saldo)
                   .HasColumnName("Saldo");
        }

    }
}