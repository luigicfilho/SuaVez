using LCFila.Web.Models;

namespace LCFila.ViewModels;

public class FilaPessoaViewModel
{
    public Guid Id { get; set; }
    public string FilaStatus { get; set; } = string.Empty;
    public FilaViewModel FiladePessoas { get; set; } = new();
    public IEnumerable<PessoaViewModel> Pessoas { get; set; } = [];
}
