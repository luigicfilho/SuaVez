using LCFilaApplication.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LCFila.ViewModels;

public class PessoaViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Celular { get; set; }
    [DisplayName("Ativo?")]
    public bool Ativo { get; set; }
    public DateTime DataEntradaNaFila { get; set; }
    public PessoaStatus Status { get; set; }
    [DisplayName("Preferencial?")]
    public bool Preferencial { get; set; }
    public int Posicao { get; set; }
    public Guid FilaId { get; set; }

    public FilaViewModel FilaPertence { get; set; }
    /* EF Relations */
    public IEnumerable<FilaViewModel> Fila { get; set; }
   
}
