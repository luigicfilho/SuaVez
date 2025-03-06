namespace LCFila.Application.Dto;

public class PessoasDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool Preferencial { get; set; }
    public int Posicao { get; set; }
}