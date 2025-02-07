using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using LCFilaInfra.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LCFilaApplication.AppServices;

internal class FilaAppService : IFilaAppService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IFilaRepository _filaRepository;
    private readonly IFilaPessoaRepository _filapessoaRepository;
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly UserManager<AppUser> _userManager;
    public FilaAppService(UserManager<AppUser> userManager,
                          IPessoaRepository pessoaRepository,
                          IFilaPessoaRepository filapessoaRepository,
                          IFilaRepository filaRepository,
                          IEmpresaLoginRepository empresaRepository)
    {
        _pessoaRepository = pessoaRepository;
        _filaRepository = filaRepository;
        _filapessoaRepository = filapessoaRepository;
        _userManager = userManager;
        _empresaRepository = empresaRepository;
    }

    public bool AdicionarPessoa(Pessoa Pessoa, Guid FilaId)
    {
        try
        {
            var pessoas = _pessoaRepository.ObterTodos().Result;
            var pessoasdafila = pessoas.Where(p => p.FilaId == FilaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).ToList();

            if (Pessoa.Preferencial)
            {
                Pessoa.Posicao = 1;
                foreach (var item in pessoas.Where(p => p.FilaId == FilaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).OrderBy(p => p.Preferencial))
                {
                    if (item.Ativo)
                    {
                        if (item.Preferencial)
                        {
                            Pessoa.Posicao = item.Posicao + 1;
                        }
                        else
                        {
                            item.Posicao = item.Posicao + 1;
                        }
                        _pessoaRepository.Atualizar(item);
                    }
                }

            }
            else
            {
                Pessoa.Posicao = pessoasdafila.Count + 1;
            }
            Pessoa.FilaId = FilaId;

            _pessoaRepository.Adicionar(Pessoa);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CriarFila(Fila fila)
    {
        var result = _filaRepository.Adicionar(fila);
        return result.IsCompletedSuccessfully;
    }

    public bool FinalizarFila(Guid Id)
    {
        try
        {
            var filatoopen = _filaRepository.ObterPorId(Id).Result;

            filatoopen!.Status = FilaStatus.Finalizada;
            _filaRepository.Atualizar(filatoopen);
            _filaRepository.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        
    }

    public (Guid userid, Guid empresaid) GetUserIdEmpId(string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result.FirstOrDefault();
        return (Guid.Parse(user!.Id), empresalogin!.Id);
    }

    public List<Fila> GetFilaList(string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        //var allusers = GetAllUsers();
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id));
        var Empresaid = empresalogin.Id;
        var pegarfila = _filaRepository.ObterTodos().Result;
        List<Fila> filasdousuario = new List<Fila>();
        //if (User.IsInRole("EmpAdmin"))
        //{
        //    filasdousuario = pegarfila.Where(p => p.EmpresaId == Empresaid).ToList();
        //} else
        //{
        return filasdousuario = pegarfila.Where(p => p.UserId == Guid.Parse(user!.Id)).ToList();
        //}

        //var pessoas = _pessoaRepository.ObterTodos().Result;
    }

    public (Fila, List<Pessoa>) GetPessoas(Guid Id, string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result;
        var Empresaid = empresalogin.SingleOrDefault()!.Id;
        var filatoopen = _filaRepository.ObterPorId(Id).Result;
        return (filatoopen!, _pessoaRepository.Buscar(p => p.FilaId == Id).Result.ToList());
    }

    public Guid IniciarFila(string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result.FirstOrDefault();
        var Empresaid = empresalogin!.Id;
        Fila novafila = new Fila();
        novafila.DataInicio = DateTime.Now;
        novafila.TempoMedio = "30";
        novafila.EmpresaId = Empresaid;
        novafila.UserId = Guid.Parse(user!.Id);
        var result = CriarFila(novafila);
        return novafila.Id;
    }

    public bool ReabrirFila(Guid Id)
    {
        try
        {
            var filatoopen = _filaRepository.ObterPorId(Id).Result;

            filatoopen!.Status = FilaStatus.Aberta;
            _filaRepository.Atualizar(filatoopen);
            _filaRepository.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public bool RemoverFila(Guid Id)
    {
        try
        {
            var filatoopen = _filaRepository.ObterPorId(Id).Result;

            filatoopen!.Ativo = false;
            _filaRepository.Atualizar(filatoopen);
            _filaRepository.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<AppUser> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }
}
