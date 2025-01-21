namespace LCFilaApplication.Models;

public class Mercadoria : Entity
{
    public Guid FilaId { get; set; }
    public string Identificacao { get; set; }
    public string Dimensoes { get; set; }
    public string Peso { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }

    /* EF Relations */
    public Fila Fila { get; set; }
}
