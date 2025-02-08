using LCFila.Domain.Models;

namespace LCFila.Application.Interfaces;

public interface IFilaAppService
{
    List<Fila> GetFilaList(string UserName);
    (Fila, List<Pessoa>) GetPessoas(Guid Id, string UserName);
    bool ReabrirFila(Guid Id);
    bool FinalizarFila(Guid Id);
    (Guid userid, Guid empresaid) GetUserIdEmpId(string UserName);
    bool CriarFila(Fila fila);
    Guid IniciarFila(string UserName);
    bool AdicionarPessoa(Pessoa Pessoa, Guid FilaId);
    bool RemoverFila(Guid Id);
    List<AppUser> GetAllUsers();
}
