namespace LCFila.Web.Models;

public class AdicionarpessoasViewModel
{
    public Guid filaId { get; set; }
    public PessoaViewModel pessoa { get; set; } = new();
}
