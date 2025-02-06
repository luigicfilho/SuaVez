using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;
using LCFilaInfra.Repository;
using Microsoft.EntityFrameworkCore;

namespace LCFilaApplication.Repository;

public class EmpresaLoginRepository : Repository<EmpresaLogin>, IEmpresaLoginRepository
{
    public EmpresaLoginRepository(FilaDbContext context) : base(context) 
    {
    }

    public override async Task<EmpresaLogin?> ObterPorId(Guid id)
    {
        return await Db.EmpresasLogin.Include(f => f.UsersEmpresa).Include(f => f.EmpresaConfiguracao).SingleOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<List<EmpresaLogin>> ObterTodos()
    {
        return await Db.EmpresasLogin.Include(f => f.UsersEmpresa)
            .Include(f => f.EmpresaConfiguracao).ToListAsync();
    }

    public void CadastrarUsuario(Guid empresaId, AppUser user)
    {
        var empresa = Db.EmpresasLogin.Include(f => f.UsersEmpresa).FirstOrDefault(p => p.Id == empresaId);
        List<Fila> EmpresaFilas = new List<Fila>();
        empresa!.EmpresaFilas = EmpresaFilas;
        empresa.UsersEmpresa.Add(user);

        Task result = Atualizar(empresa);
        if (!result.IsCompletedSuccessfully)
        {
            throw new Exception("Something go wrong!");
        }
        Task<int> saved = SaveChanges();
        if (!saved.IsCompletedSuccessfully)
        {
            throw new Exception("Something go wrong!");
        }
    }
}
