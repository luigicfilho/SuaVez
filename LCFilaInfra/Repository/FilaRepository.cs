using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;

namespace LCFilaApplication.Repository;

public class FilaRepository : Repository<Fila>, IFilaRepository
{
    public FilaRepository(FilaDbContext context) : base(context) { }
}
