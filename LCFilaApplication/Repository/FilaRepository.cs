using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCFilaApplication.Repository
{
    public class FilaRepository : Repository<Fila>, IFilaRepository
    {
        public FilaRepository(FilaDbContext context) : base(context) { }
    }

}
