namespace LCFilaApplication.Models;

public class EmpresaConfiguracao : Entity
{
    public string NomeDaEmpresa { get; set; } = string.Empty;
    public string LinkLogodaEmpresa { get; set; } = string.Empty;
    public string CorPrincipalEmpresa { get; set; } = string.Empty;
    public string CorSegundariaEmpresa { get; set; } = string.Empty;
    public string FooterEmpresa { get; set; } = string.Empty;
}
