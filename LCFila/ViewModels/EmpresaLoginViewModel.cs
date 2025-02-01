using LCFilaApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LCFila.ViewModels;

public class EmpresaLoginViewModel
{

    public Guid Id { get; set; }
    [Display(Name = "Nome da Empresa")]
    public string NomeEmpresa { get; set; }
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório!")]
    [EmailAddress(ErrorMessage = "Este campo precisa ser um e-mail")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório!")]
    [StringLength(100, ErrorMessage = "A {0} precisa ter no mínimo {2} máximo {1} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    public Guid IdAdminEmpresa { get; set; }
    public List<AppUser>? UsersEmpresa { get; set; }

    [Display(Name = "Lista de Usuários")]
    public SelectList? ListaUsers { get; set; }
    public EmpresaConfiguracaoViewModel? EmpresaConfiguracao { get; set; }
    public List<FilaViewModel>? EmpresaFilas { get; set; } = [];

    [Display(Name = "Administrador")]
    public AppUser? AdminEmpresa { get; set; }

    [Display(Name = "Status ")]
    public bool Ativo { get; set; }
}
