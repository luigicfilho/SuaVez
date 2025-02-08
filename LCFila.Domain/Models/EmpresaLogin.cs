namespace LCFila.Domain.Models;

public class EmpresaLogin : Entity
{
    public string NomeEmpresa { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Guid IdAdminEmpresa { get; set; } = new();
    public List<AppUser> UsersEmpresa { get; set; } = [];
    public EmpresaConfiguracao EmpresaConfiguracao { get; set; } = new();
    public List<Fila> EmpresaFilas { get; set; } = [];
    public bool Ativo { get; set; }
}
