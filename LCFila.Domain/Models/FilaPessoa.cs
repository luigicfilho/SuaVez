namespace LCFila.Domain.Models;

public class FilaPessoa : Entity
{
    public Fila FiladePessoas { get; set; } = new();
    /* EF Relations */
    public IEnumerable<Pessoa> Pessoas { get; set; } = [];
}
