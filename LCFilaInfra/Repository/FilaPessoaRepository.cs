using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;

namespace LCFilaApplication.Repository
{
    public class FilaPessoaRepository : Repository<FilaPessoa>, IFilaPessoaRepository
    {
        public FilaPessoaRepository(FilaDbContext context) : base(context) { }
    }
}
