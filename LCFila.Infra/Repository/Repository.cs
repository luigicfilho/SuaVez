using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LCFila.Infra.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly FilaDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(FilaDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity?> ObterPorId(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> ObterTodos()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task Adicionar(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Atualizar(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Remover(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}
