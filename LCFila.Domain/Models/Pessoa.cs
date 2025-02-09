using LCFila.Domain.Enums;

namespace LCFila.Domain.Models;

public class Pessoa : Entity
{
    public Guid FilaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public bool Preferencial { get; set; }
    public int Posicao { get; set; }
    public PessoaStatus Status { get; set; }
    /* EF Relations */
    //public Fila Fila { get; set; } = new();
}
