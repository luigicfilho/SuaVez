using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;

namespace LCFilaInfra.Repository;

public class EmpresaConfiguracaoRepository : Repository<EmpresaConfiguracao>, IEmpresaConfiguracaoRepository
{
    public EmpresaConfiguracaoRepository(FilaDbContext context) : base(context) { }
}
