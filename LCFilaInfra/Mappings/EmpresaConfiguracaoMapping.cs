using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFilaApplication.Mappings;

public class EmpresaConfiguracaoMapping : IEntityTypeConfiguration<EmpresaConfiguracao>
{
    public void Configure(EntityTypeBuilder<EmpresaConfiguracao> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.NomeDaEmpresa)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.LinkLogodaEmpresa)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.CorPrincipalEmpresa)
            .HasColumnType("varchar(50)");

        builder.Property(c => c.CorSegundariaEmpresa)
            .HasColumnType("varchar(50)");

        builder.Property(c => c.CorSegundariaEmpresa)
            .HasColumnType("varchar(200)");

        builder.ToTable("EmpresaConfiguracaos");
    }
}
