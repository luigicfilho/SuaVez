using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;

namespace LCFilaApplication.Repository;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(FilaDbContext context) : base(context) { }
}
