using Microsoft.AspNetCore.Http;

namespace LCFila.Application.Dto;

public class EmpresaConfiguracaoDto
{
    public Guid Id { get; set; }

    public string NomeDaEmpresa { get; set; } = string.Empty;

    public string LinkLogodaEmpresa { get; set; } = string.Empty;

    public string CorPrincipalEmpresa { get; set; } = string.Empty;

    public string CorSegundariaEmpresa { get; set; } = string.Empty;

    public string FooterEmpresa { get; set; } = string.Empty;

    public IFormFile? file { get; set; }
}
