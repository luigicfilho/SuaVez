namespace LCFila.ViewModels;

public class FilaPessoaViewModel
{
    public FilaViewModel FiladePessoas { get; set; }
    public IEnumerable<PessoaViewModel> Pessoas { get; set; }
}
