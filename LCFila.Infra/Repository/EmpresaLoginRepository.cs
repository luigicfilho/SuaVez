using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;
using LCFila.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace LCFila.Infra.Repository;

public class EmpresaLoginRepository : Repository<EmpresaLogin>, IEmpresaLoginRepository
{
    public EmpresaLoginRepository(FilaDbContext context) : base(context) 
    {
    }

    public override async Task<EmpresaLogin?> ObterPorId(Guid id)
    {
        var empresas = await Db.EmpresasLogin.Include(f => f.UsersEmpresa).Include(f => f.EmpresaConfiguracao).SingleOrDefaultAsync(p => p.Id == id);
        return empresas;
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

    public async Task<EmpresaLogin?> ObterPorAdminId(Guid id)
    {
        var empresas = await Db.EmpresasLogin.Include(f => f.UsersEmpresa).Include(f => f.EmpresaConfiguracao).SingleOrDefaultAsync(p => p.IdAdminEmpresa == id);
        return empresas;
    }
}
