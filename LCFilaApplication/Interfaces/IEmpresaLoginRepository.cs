using LCAppFila.Domain.Interfaces;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LCFilaApplication.Interfaces
{
    public interface IEmpresaLoginRepository : IRepository<EmpresaLogin>
    {
        void CadastrarUsuario(Guid empresaId, AppUser user);
    }
}
