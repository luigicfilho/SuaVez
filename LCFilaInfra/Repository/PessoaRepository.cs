using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;
using LCFilaInfra.Repository;

namespace LCFilaApplication.Repository;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(FilaDbContext context) : base(context) { }
}
