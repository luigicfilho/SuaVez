using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;

namespace LCFila.Infra.Repository;

public class EmpresaConfiguracaoRepository : Repository<EmpresaConfiguracao>, IEmpresaConfiguracaoRepository
{
    public EmpresaConfiguracaoRepository(FilaDbContext context) : base(context) { }
}
