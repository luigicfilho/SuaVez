using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFilaApplication.Mappings;

public class FilaMercadoriaMapping : IEntityTypeConfiguration<FilaMercadoria>
{
    public void Configure(EntityTypeBuilder<FilaMercadoria> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(f => f.FiladeMercadorias);

        builder.HasMany(f => f.Mercadorias);

        builder.ToTable("FilaMercadorias");
    }
}
