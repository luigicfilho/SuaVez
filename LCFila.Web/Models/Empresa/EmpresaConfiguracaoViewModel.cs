using System.ComponentModel.DataAnnotations;

namespace LCFila.Web.Models.Empresa;

public class EmpresaConfiguracaoViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Nome da Empresa")]
    public string NomeDaEmpresa { get; set; } = string.Empty;

    [Display(Name = "Logo da Empresa")]
    public string LinkLogodaEmpresa { get; set; } = string.Empty;

    [Display(Name = "Cor principal da Empresa")]
    public string CorPrincipalEmpresa { get; set; } = string.Empty;

    [Display(Name = "Cor secundária da Empresa")]
    public string CorSegundariaEmpresa { get; set; } = string.Empty;

    [Display(Name = "Rodapé da Empresa (HTML)")]
    [MaxLength(100, ErrorMessage = "Máximo 100 Caracteres")]
    public string FooterEmpresa { get; set; } = string.Empty;

    public IFormFile? file { get; set; }

}
