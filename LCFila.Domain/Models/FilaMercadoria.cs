namespace LCFila.Domain.Models;

public class FilaMercadoria : Entity
{
    public Fila FiladeMercadorias { get; set; } = new();
    /* EF Relations */
    public IEnumerable<Mercadoria> Mercadorias { get; set; } = [];
}
