
namespace LCFila.Application.Dto;

public class FilaDetailsDto
{
    public Guid FilaId { get; set; }
    public string FilaStatus { get; set; } = string.Empty;
    public List<PessoasDto> ListaPessoas { get; set; } = [];
}
