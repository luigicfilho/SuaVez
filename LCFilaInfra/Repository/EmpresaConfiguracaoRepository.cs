using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;

namespace LCFilaApplication.Repository
{
    public class EmpresaConfiguracaoRepository : Repository<EmpresaConfiguracao>, IEmpresaConfiguracaoRepository
    {
        public EmpresaConfiguracaoRepository(FilaDbContext context) : base(context) { }
    }
}
