namespace LCFila.Web.Models.Pessoa;

public class AdicionarpessoasViewModel
{
    public Guid filaId { get; set; }
    public PessoaViewModel pessoa { get; set; } = new();
}
