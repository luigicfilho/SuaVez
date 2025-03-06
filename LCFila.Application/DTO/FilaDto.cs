namespace LCFila.Application.Dto;

public class FilaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public string Tipofila { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string TempoMedio { get; set; } = string.Empty;

    public string NomeUser { get; set; } = string.Empty;
}
