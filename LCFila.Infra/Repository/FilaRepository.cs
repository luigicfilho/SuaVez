using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;

namespace LCFila.Infra.Repository;

public class FilaRepository : Repository<Fila>, IFilaRepository
{
    public FilaRepository(FilaDbContext context) : base(context) { }
}
