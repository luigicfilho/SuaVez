namespace LCFila.Domain.Models;

public class FilaVeiculo : Entity
{
    public Fila FiladeVeiculos { get; set; } = new();
    /* EF Relations */
    public IEnumerable<Veiculo> Veiculos { get; set; } = [];
}
