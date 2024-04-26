using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFilaApplication.Mappings
{
    public class VeiculoMapping : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Modelo)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Fabricante)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Placa)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.HasOne(f => f.Fila);

            builder.ToTable("Veiculos");
        }
    }
}
