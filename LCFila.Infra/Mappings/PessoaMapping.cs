using LCFila.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFila.Infra.Mappings;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Documento)
            .HasColumnType("varchar(14)");

        builder.Property(c => c.Celular)
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        //builder.HasOne(f => f.Fila).WithOne();
           //.WithMany() // or .WithOne() if it's a one-to-one
           //.HasForeignKey(p => p.FilaId);// Specify the foreign key property
           //.OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Pessoas");
    }
}
