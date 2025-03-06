namespace LCFila.Web.Models;

public class FilaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string TempoMedio { get; set; } = string.Empty;
    public string NomeUser { get; set; } = string.Empty;

    //While
    public Guid UserId { get; set; }
    public Guid EmpresaId { get; set; }
}

