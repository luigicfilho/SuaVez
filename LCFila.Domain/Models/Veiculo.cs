namespace LCFila.Domain.Models;

public class Veiculo : Entity
{
    public Guid FilaId { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string TipoVeiculo { get; set; } = string.Empty;
    public bool Ativo { get; set; }

    /* EF Relations */
    public Fila Fila { get; set; } = new();
}
