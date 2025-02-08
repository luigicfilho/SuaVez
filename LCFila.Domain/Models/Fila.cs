using LCFila.Domain.Enums;

namespace LCFila.Domain.Models;

public class Fila : Entity
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string TempoMedio { get; set; } = string.Empty;
    public TiposFilas Tipofila { get; set; }
    public Guid EmpresaId { get; set; }
    public FilaStatus Status { get; set; }
    public bool Ativo { get; set; }
    public Guid UserId { get; set; }
}
