﻿using LCFila.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCFila.Infra.Mappings;

public class FilaMapping : IEntityTypeConfiguration<Fila>
{
    public void Configure(EntityTypeBuilder<Fila> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.TempoMedio)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.ToTable("Filas");
    }
}
