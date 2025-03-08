namespace LCFila.Application.Dto;

public class EmpresaLoginDto
{
    public Guid Id { get; set; }
    public string NomeEmpresa { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid IdAdminEmpresa { get; set; }
    public List<AppUserDto>? UsersEmpresa { get; set; }

    public EmpresaConfiguracaoDto? EmpresaConfiguracao { get; set; }
    public List<FilaDto>? EmpresaFilas { get; set; } = [];

    public AppUserDto? AdminEmpresa { get; set; }

    public bool Ativo { get; set; }
}
