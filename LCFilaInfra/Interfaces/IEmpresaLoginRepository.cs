using LCFilaApplication.Models;

namespace LCFilaInfra.Interfaces;

public interface IEmpresaLoginRepository : IRepository<EmpresaLogin>
{
    void CadastrarUsuario(Guid empresaId, AppUser user);
}
