using System.ComponentModel.DataAnnotations;

namespace LCFila.ViewModels;

public class EmpresaConfiguracaoViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Nome da Empresa")]
    public string NomeDaEmpresa { get; set; }

    [Display(Name = "Logo da Empresa")]
    public string LinkLogodaEmpresa { get; set; }

    [Display(Name = "Cor principal da Empresa")]
    public string CorPrincipalEmpresa { get; set; }

    [Display(Name = "Cor secundária da Empresa")]
    public string CorSegundariaEmpresa { get; set; }

    [Display(Name = "Rodapé da Empresa (HTML)")]
    [MaxLength(100, ErrorMessage = "Máximo 100 Caracteres")]
    public string FooterEmpresa { get; set; }

    public IFormFile file { get; set; }

}
