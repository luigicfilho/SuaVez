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
            //Pessoa.DataEntradaNaFila = DateTime.Now;
            pessoadb.Ativo = true;
            pessoadb.Status = PessoaStatus.Esperando;
            pessoadb.Preferencial = Pessoa.Preferencial;
            //Enum.Parse<PessoaStatus>(Enum.GetName(pessoaViewModel.Status)!);
            //Pessoa.Fila = fila!;
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
            //Pessoa.Fila = fila!;
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
        //var allusers = GetAllUsers();
        var empresalogin = _empresaRepository.Buscar(s => s.IdAdminEmpresa == Guid.Parse(user!.Id));
        var Empresaid = empresalogin.Id;
        var pegarfila = _filaRepository.ObterTodos().Result;
        var allfila = pegarfila.Where(p => p.UserId == Guid.Parse(user!.Id)).ToList();
        
        List<Fila> filasdousuario = new List<Fila>();
        //if (User.IsInRole("EmpAdmin"))
        //{
        //    filasdousuario = pegarfila.Where(p => p.EmpresaId == Empresaid).ToList();
        //} else
        //{

        foreach (var item in allfila)
        {
            FilaDto fila = new();
            fila.Id = item.Id;
            fila.Status = Enum.GetName(item.Status)!;
            fila.Nome = item.Nome;
            fila.DataInicio = item.DataInicio;
            fila.Ativo = item.Ativo;
            fila.NomeUser = allusers.SingleOrDefault(p => p.Id == item.UserId.ToString())!.UserName!;
            listaDeFila.Add(fila);
        }
        return listaDeFila;
        //}

        //var pessoas = _pessoaRepository.ObterTodos().Result;
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
            PessoasDto pessoaDto = new();
            pessoaDto.Id = pessoa.Id;
            pessoaDto.Nome = pessoa.Nome;
            pessoaDto.Posicao = pessoa.Posicao;
            pessoaDto.Preferencial = pessoa.Preferencial;
            pessoaDto.Celular = pessoa.Celular;
            pessoaDto.Status = Enum.GetName(pessoa.Status)!;
            pessoaDto.Ativo = pessoa.Ativo;
            filaDetails.ListaPessoas.Add(pessoaDto);
        }

        return filaDetails;
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
        //var result = CriarFila(novafila);
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
