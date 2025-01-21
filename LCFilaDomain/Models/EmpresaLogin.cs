namespace LCFilaApplication.Models;

public class EmpresaLogin : Entity
{
    public string NomeEmpresa { get; set; }
    public string CNPJ { get; set; }
    public Guid IdAdminEmpresa { get; set; }
    public List<AppUser> UsersEmpresa { get; set; }
    public EmpresaConfiguracao EmpresaConfiguracao { get; set; }
    public List<Fila> EmpresaFilas { get; set; }
    public bool Ativo { get; set; }
}
