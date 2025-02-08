using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;
using LCFila.Infra.Repository;

namespace LCFila.Infra.Repository;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(FilaDbContext context) : base(context) { }
}
