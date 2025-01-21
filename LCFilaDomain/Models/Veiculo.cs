namespace LCFilaApplication.Models;

public class Veiculo : Entity
{
    public Guid FilaId { get; set; }
    public string Placa { get; set; }
    public string Fabricante { get; set; }
    public string Modelo { get; set; }
    public string TipoVeiculo { get; set; }
    public bool Ativo { get; set; }

    /* EF Relations */
    public Fila Fila { get; set; }
}
