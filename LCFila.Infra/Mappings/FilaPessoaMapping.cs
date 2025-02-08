using LCFila.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFila.Infra.Mappings;

public class FilaPessoaMapping : IEntityTypeConfiguration<FilaPessoa>
{
    public void Configure(EntityTypeBuilder<FilaPessoa> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(f => f.FiladePessoas);

        builder.HasMany(f => f.Pessoas);

        builder.ToTable("FilaPessoas");
    }
}
