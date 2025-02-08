namespace LCFila.Domain.Models;

public class Mercadoria : Entity
{
    public Guid FilaId { get; set; }
    public string Identificacao { get; set; } = string.Empty;
    public string Dimensoes { get; set; } = string.Empty;
    public string Peso { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set; }

    /* EF Relations */
    public Fila Fila { get; set; } = new();
}
