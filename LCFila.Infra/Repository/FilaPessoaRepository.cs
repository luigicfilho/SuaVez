using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;
using LCFila.Infra.Repository;

namespace LCFila.Infra.Repository;

public class FilaPessoaRepository : Repository<FilaPessoa>, IFilaPessoaRepository
{
    public FilaPessoaRepository(FilaDbContext context) : base(context) { }
}
