using LCFilaApplication.Enums;

namespace LCFilaApplication.Models;

public class Pessoa : Entity
{
    public Guid FilaId { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Celular { get; set; }
    public bool Ativo { get; set; }
    public bool Preferencial { get; set; }
    public int Posicao { get; set; }
    public PessoaStatus Status { get; set; }
    /* EF Relations */
    public Fila Fila { get; set; }
}
