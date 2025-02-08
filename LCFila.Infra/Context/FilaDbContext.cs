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
    public DbSet<FilaPessoa> FilaPessoas { get; set; }
    public DbSet<EmpresaLogin> EmpresasLogin { get; set; }
    public DbSet<EmpresaConfiguracao> EmpresaConfigs { get; set; }
    //public DbSet<IdentityRole> IdentityRoles { get; set; }


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
        //foreach (var property in modelBuilder.Model.GetEntityTypes()
        //    .SelectMany(e => e.GetProperties()
        //        .Where(p => p.ClrType == typeof(string))))
        //    property.SetColumnType("varchar(100)");

        //modelBuilder.ApplyConfigurationsFromAssembly();
        
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

        base.OnModelCreating(modelBuilder);
        //modelBuilder.HasDefaultSchema("Identity");
        //modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers", "security");


        modelBuilder.Entity<AppUser>()
            .Property(u => u.Id)
            .HasMaxLength(128); // Let SQLite auto-generate it
        ////modelBuilder.Entity<IdentityUser>(entity =>
        ////{
        ////    entity.ToTable(name: "User", "Identity");
        ////});
        //modelBuilder.Entity<IdentityRole>(entity =>
        //{
        //    entity.ToTable(name: "Role", "Identity");
        //});
        //modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        //{
        //    entity.ToTable("UserRoles", "Identity");
        //});
        //modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        //{
        //    entity.ToTable("UserClaims", "Identity");
        //});
        //modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        //{
        //    entity.ToTable("UserLogins", "Identity");
        //});
        //modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        //{
        //    entity.ToTable("RoleClaims", "Identity");
        //});
        //modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        //{
        //    entity.ToTable("UserTokens", "Identity");
        //});
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        //{
        //    if (entry.State == EntityState.Added)
        //    {
        //        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
        //    }

        //    if (entry.State == EntityState.Modified)
        //    {
        //        entry.Property("DataCadastro").IsModified = false;
        //    }
        //}

        return base.SaveChangesAsync(cancellationToken);
    }
}
