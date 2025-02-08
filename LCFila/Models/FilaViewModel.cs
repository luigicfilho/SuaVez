//TODO: remove this reference in someway
using LCFila.Web.Models;
namespace LCFila.ViewModels;

public class FilaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public TiposFilaViewModel Tipofila { get; set; }
    public FilaStatusViewModel Status { get; set; }
    public bool Ativo { get; set; }
    public string TempoMedio { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid EmpresaId { get; set; }

    public string NomeUser { get; set; } = string.Empty;
}

