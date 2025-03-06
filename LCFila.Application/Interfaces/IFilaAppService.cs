using LCFila.Application.Dto;
using LCFila.Domain.Models;

namespace LCFila.Application.Interfaces;

public interface IFilaAppService
{
    List<FilaDto> GetFilaList(string UserName);
    FilaDetailsDto GetPessoas(Guid Id, string UserName);
    bool ReabrirFila(Guid Id);
    bool FinalizarFila(Guid Id);
    CreateFilaDto GetUserIdEmpId(string UserName);
    bool CriarFila(FilaDto fila, Guid EmpresaId, Guid UserId);
    Guid IniciarFila(string UserName);
    bool AdicionarPessoa(PessoasDto Pessoa, Guid FilaId);
    bool RemoverFila(Guid Id);
    List<AppUser> GetAllUsers();
}
