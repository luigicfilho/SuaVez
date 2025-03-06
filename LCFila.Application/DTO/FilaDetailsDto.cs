
namespace LCFila.Application.DTO;

public class FilaDetailsDto
{
    public Guid FilaId { get; set; }
    public string FilaStatus { get; set; } = string.Empty;
    public List<PessoasDto> ListaPessoas { get; set; } = [];
}
