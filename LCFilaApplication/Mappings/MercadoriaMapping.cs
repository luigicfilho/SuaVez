using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFilaApplication.Mappings
{
    public class MercadoriaMapping : IEntityTypeConfiguration<Mercadoria>
    {
        public void Configure(EntityTypeBuilder<Mercadoria> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Identificacao)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Descricao)
                //.IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Dimensoes)
                //.IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Peso)
                //.IsRequired()
                .HasColumnType("varchar(10)");

            builder.HasOne(f => f.Fila);

            builder.ToTable("Mercadorias");
        }
    }
}
