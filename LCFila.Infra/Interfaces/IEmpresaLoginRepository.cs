using LCFila.Domain.Models;

namespace LCFila.Infra.Interfaces;

public interface IEmpresaLoginRepository : IRepository<EmpresaLogin>
{
    void CadastrarUsuario(Guid empresaId, AppUser user);
}
