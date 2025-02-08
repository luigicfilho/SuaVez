using LCFila.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFila.Infra.Mappings;

public class MercadoriaMapping : IEntityTypeConfiguration<Mercadoria>
{
    public void Configure(EntityTypeBuilder<Mercadoria> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Identificacao)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(c => c.Descricao)
            .HasColumnType("varchar(200)");

        builder.Property(c => c.Dimensoes)
            .HasColumnType("varchar(50)");

        builder.Property(c => c.Peso)
            .HasColumnType("varchar(10)");

        builder.HasOne(f => f.Fila);

        builder.ToTable("Mercadorias");
    }
}
