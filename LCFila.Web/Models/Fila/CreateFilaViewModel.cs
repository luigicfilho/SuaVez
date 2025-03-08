namespace LCFila.Web.Models.Fila;

public class CreateFilaViewModel
{
    public string Nome { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid EmpresaId { get; set; }
}
