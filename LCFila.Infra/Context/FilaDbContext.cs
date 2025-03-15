using LCFila.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LCFila.Infra.Context;

public class FilaDbContext : IdentityDbContext
{
    public FilaDbContext(DbContextOptions<FilaDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Fila> Filas { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<EmpresaLogin> EmpresasLogin { get; set; }
    public DbSet<EmpresaConfiguracao> EmpresaConfigs { get; set; }


    public bool MigrateDatabase()
    {
        if (!Database.IsInMemory())
        {
            //context.Database.EnsureCreated();
            Database.Migrate();
            return true;
        }
        return false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilaDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>()
            .Property(u => u.Id)
            .HasMaxLength(128);
    }
}
