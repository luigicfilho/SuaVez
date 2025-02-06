using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;
using LCFilaInfra.Repository;

namespace LCFilaApplication.Repository;

public class FilaRepository : Repository<Fila>, IFilaRepository
{
    public FilaRepository(FilaDbContext context) : base(context) { }
}
