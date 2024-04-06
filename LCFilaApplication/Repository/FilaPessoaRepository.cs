using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCFilaApplication.Repository
{
    public class FilaPessoaRepository : Repository<FilaPessoa>, IFilaPessoaRepository
    {
        public FilaPessoaRepository(FilaDbContext context) : base(context) { }
    }
}
