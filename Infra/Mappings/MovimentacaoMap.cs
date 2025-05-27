using Domain.Entidades;
using Domain.Enums;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class MovimentacaoMap : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.ToTable("Movimentacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseMySqlIdentityColumn()
                   .HasColumnName("Id");

            builder.Property(x => x.NumeroConta)
                   .HasColumnName("Numero_Conta");


            builder.Property(x => x.SaldoAntes)
                   .HasColumnName("Saldo_Antes");

            builder.Property(x => x.SaldoDepois)
                   .HasColumnName("Saldo_Depois");

            builder.Property(x => x.TipoTransacao)
                   .HasColumnName("Tipo_Transacao")
                   .HasConversion(
                        x => x.GetDescription(),
                        x => x.GetEnumFromDescription<TipoTransacao>()
                    )
                    .HasColumnType("varchar(2)") // Define tipo no banco
                    .IsRequired()
                    .HasComment("Tipos Transação (P, D, C)");

            builder.Property(x => x.Data)
                   .HasColumnName("Data");


            //Relacionamentos
            builder.HasOne(movi => movi.Conta)
                   .WithMany(conta => conta.Movimentacoes)
                   .HasForeignKey(movi => movi.NumeroConta);
        }

    }
}