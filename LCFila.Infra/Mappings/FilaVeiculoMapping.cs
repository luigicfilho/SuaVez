using LCFila.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFila.Infra.Mappings;

public class FilaVeiculoMapping : IEntityTypeConfiguration<FilaVeiculo>
{
    public void Configure(EntityTypeBuilder<FilaVeiculo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(f => f.FiladeVeiculos);

        builder.HasMany(f => f.Veiculos);

        builder.ToTable("FilaVeiculos");
    }
}
