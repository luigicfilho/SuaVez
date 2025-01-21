namespace LCFilaApplication.Models;

public class FilaPessoa : Entity
{
    public Fila FiladePessoas { get; set; }
    /* EF Relations */
    public IEnumerable<Pessoa> Pessoas { get; set; }
}
