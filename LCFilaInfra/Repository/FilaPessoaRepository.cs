using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;
using LCFilaInfra.Repository;

namespace LCFilaApplication.Repository;

public class FilaPessoaRepository : Repository<FilaPessoa>, IFilaPessoaRepository
{
    public FilaPessoaRepository(FilaDbContext context) : base(context) { }
}
