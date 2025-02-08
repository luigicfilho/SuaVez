namespace LCFila.ViewModels;

public class FilaPessoaViewModel
{
    public Guid Id { get; set; }
    public FilaViewModel FiladePessoas { get; set; } = new();
    public IEnumerable<PessoaViewModel> Pessoas { get; set; } = [];
}
