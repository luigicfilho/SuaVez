using LCFila.Domain.Enums;
using LCFila.Application.Interfaces;
using LCFila.Domain.Models;
using LCFila.Infra.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LCFila.Application.Dto;

namespace LCFila.Application.AppServices;

internal class FilaAppService : IFilaAppService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IFilaRepository _filaRepository;
    private readonly IEmpresaLoginRepository _empresaRepository;
    private readonly UserManager<AppUser> _userManager;
    public FilaAppService(UserManager<AppUser> userManager,
                          IPessoaRepository pessoaRepository,
                          IFilaRepository filaRepository,
                          IEmpresaLoginRepository empresaRepository)
    {
        _pessoaRepository = pessoaRepository;
        _filaRepository = filaRepository;
        _userManager = userManager;
        _empresaRepository = empresaRepository;
    }

    public bool AdicionarPessoa(PessoasDto Pessoa, Guid FilaId)
    {
        try
        {
            var pessoas = _pessoaRepository.ObterTodos().Result;
            var fila = _filaRepository.ObterPorId(FilaId).Result;
            var pessoasdafila = pessoas.Where(p => p.FilaId == FilaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).ToList();

            Pessoa pessoadb = new();
            pessoadb.Ativo = true;
            pessoadb.Status = PessoaStatus.Esperando;
            pessoadb.Preferencial = Pessoa.Preferencial;

            if (Pessoa.Preferencial)
            {
                pessoadb.Posicao = 1;
                foreach (var item in pessoas.Where(p => p.FilaId == FilaId && p.Ativo == true && p.Status == PessoaStatus.Esperando).OrderBy(p => p.Preferencial))
                {
                    if (item.Ativo)
                    {
                        if (item.Preferencial)
                        {
                            pessoadb.Posicao = item.Posicao + 1;
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
                pessoadb.Posicao = pessoasdafila.Count + 1;
            }
            pessoadb.FilaId = FilaId;
            _pessoaRepository.Adicionar(pessoadb);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool CriarFila(FilaDto fila, Guid EmpresaId, Guid UserId)
    {
        Fila novaFila = new();
        novaFila.DataInicio = DateTime.Now;
        novaFila.Ativo = true;
        novaFila.Status = FilaStatus.Aberta;
        novaFila.Nome = fila.Nome;
        novaFila.EmpresaId = EmpresaId;
        novaFila.UserId = UserId;
        novaFila.TempoMedio = "30";
        var result = _filaRepository.Adicionar(novaFila);
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

    public CreateFilaDto GetUserIdEmpId(string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result.FirstOrDefault();

        CreateFilaDto createFilaDto = new()
        {
            UserId = Guid.Parse(user!.Id),
            EmpresaId = empresalogin!.Id
        };

        return createFilaDto;
    }

    public List<FilaDto> GetFilaList(string UserName)
    {
        List<FilaDto> listaDeFila = new();

        var allusers = GetAllUsers();
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id));
        var Empresaid = empresalogin.Id;
        var pegarfila = _filaRepository.ObterTodos().Result;
        var allfila = pegarfila.Where(p => p.UserId == Guid.Parse(user!.Id)).ToList();
        
        List<Fila> filasdousuario = [];

        foreach (var item in allfila)
        {
            FilaDto fila = new()
            {
                Id = item.Id,
                Status = Enum.GetName(item.Status)!,
                Nome = item.Nome,
                DataInicio = item.DataInicio,
                Ativo = item.Ativo,
                NomeUser = allusers.SingleOrDefault(p => p.Id == item.UserId.ToString())!.UserName!
            };
            listaDeFila.Add(fila);
        }
        return listaDeFila;
    }

    public FilaDetailsDto GetPessoas(Guid Id, string UserName)
    {
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result;
        var Empresaid = empresalogin.SingleOrDefault()!.Id;
        var filatoopen = _filaRepository.ObterPorId(Id).Result;
        var pessoas = _pessoaRepository.Buscar(p => p.FilaId == Id).Result.ToList();
        FilaDetailsDto filaDetails = new();
        filaDetails.FilaId = filatoopen!.Id;
        filaDetails.FilaStatus = Enum.GetName(filatoopen.Status)!;
        
        foreach(var pessoa in pessoas)
        {
            PessoasDto pessoaDto = new()
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Posicao = pessoa.Posicao,
                Preferencial = pessoa.Preferencial,
                Celular = pessoa.Celular,
                Status = Enum.GetName(pessoa.Status)!,
                Ativo = pessoa.Ativo
            };
            filaDetails.ListaPessoas.Add(pessoaDto);
        }

        return filaDetails;
    }

    public Guid IniciarFila(string UserName)
    {
        //TODO: Verificar
        var user = _userManager.Users.SingleOrDefaultAsync(p => p.UserName == UserName).Result;
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id)).Result.FirstOrDefault();
        var Empresaid = empresalogin!.Id;
        Fila novafila = new Fila
        {
            DataInicio = DateTime.Now,
            TempoMedio = "30",
            EmpresaId = Empresaid,
            UserId = Guid.Parse(user!.Id)
        };
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
