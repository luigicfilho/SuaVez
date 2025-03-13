using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Pessoa;

public class PessoaViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; } = string.Empty;
    //public string Documento { get; set; } = string.Empty;
    public string Celular { get; set; } = string.Empty;
    [DisplayName("Ativo?")]
    public bool Ativo { get; set; }
    //public DateTime DataEntradaNaFila { get; set; }
    public string Status { get; set; } = string.Empty;
    [DisplayName("Preferencial?")]
    public bool Preferencial { get; set; }
    public int Posicao { get; set; }
    public Guid FilaId { get; set; }
   
}
