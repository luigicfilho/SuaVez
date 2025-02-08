using LCFila.Domain.Models;

namespace LCFila.Application.Interfaces;

public interface IPessoaAppService
{
    bool Atender(Guid id, Guid filaid);
    bool Pular(Guid id, Guid filaid);

    bool Chamar(Guid id, Guid filaid);

    Pessoa GetDetails(Guid id, Guid filaid);

    bool Remover(Guid id, Guid filaid);
}
