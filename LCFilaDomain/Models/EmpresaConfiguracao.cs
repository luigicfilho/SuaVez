namespace LCFilaApplication.Models;

public class EmpresaConfiguracao : Entity
{
    public string NomeDaEmpresa { get; set; }
    public string LinkLogodaEmpresa { get; set; }
    public string CorPrincipalEmpresa { get; set; }
    public string CorSegundariaEmpresa { get; set; }
    public string FooterEmpresa { get; set; }
}
