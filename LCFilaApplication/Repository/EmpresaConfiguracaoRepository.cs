using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCFilaApplication.Repository
{
    public class EmpresaConfiguracaoRepository : Repository<EmpresaConfiguracao>, IEmpresaConfiguracaoRepository
    {
        public EmpresaConfiguracaoRepository(FilaDbContext context) : base(context) { }
    }
}
