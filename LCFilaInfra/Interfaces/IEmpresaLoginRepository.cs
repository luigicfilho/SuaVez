using LCAppFila.Domain.Interfaces;
using LCFilaApplication.Models;
using System;

namespace LCFilaApplication.Interfaces
{
    public interface IEmpresaLoginRepository : IRepository<EmpresaLogin>
    {
        void CadastrarUsuario(Guid empresaId, AppUser user);
    }
}
