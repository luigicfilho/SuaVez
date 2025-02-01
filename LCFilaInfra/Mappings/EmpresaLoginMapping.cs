using LCFilaApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFilaApplication.Mappings;

public class EmpresaLoginMapping : IEntityTypeConfiguration<EmpresaLogin>
{
    public void Configure(EntityTypeBuilder<EmpresaLogin> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.NomeEmpresa)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.CNPJ)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.HasOne(f => f.EmpresaConfiguracao);

        //builder.HasMany(f => f.UsersEmpresa).WithOne(i => i.EmpresaLogin);

        builder.HasMany(f => f.EmpresaFilas);

        builder.ToTable("EmpresaLogins");
    }
}
