using Microsoft.AspNetCore.Identity;

namespace LCFilaApplication.Extensions;

/// <summary>
///   Descrição dos erros do Identity em portugues
/// </summary>
public class AppErrorDescriber : IdentityErrorDescriber
{
    /// <summary>
    ///    Ocorreu algum erro desconhecido
    /// </summary>
    public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Um erro desconhecido ocorreu." }; }
    /// <summary>
    ///    Falha concorrente
    /// </summary>
    public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Falha de concorrência otimista, o objeto foi modificado." }; }
    /// <summary>
    ///    Senha incorreta
    /// </summary>
    public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Senha incorreta." }; }
    /// <summary>
    ///    Token inválido.
    /// </summary>
    public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Token inválido." }; }
    /// <summary>
    ///    Já existe um usuário com este login.
    /// </summary>
    public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Já existe um usuário com este login." }; }
    /// <summary>
    ///    Usuário inválido
    /// </summary>
    public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = $"Login '{userName}' é inválido, pode conter apenas letras ou dígitos." }; }
    /// <summary>
    ///    Email inválido
    /// </summary>
    public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = $"Email '{email}' é inválido." }; }
    /// <summary>
    ///    Valida se o usuário está duplicado ou não
    /// </summary>
    public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Login '{userName}' já está sendo utilizado." }; }
    /// <summary>
    ///    Email Duplicado
    /// </summary>
    public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Email '{email}' já está sendo utilizado." }; }
    /// <summary>
    ///    Permissão inválida
    /// </summary>
    public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"A permissão '{role}' é inválida." }; }
    /// <summary>
    ///    Permissão duplicada
    /// </summary>
    public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"A permissão '{role}' já está sendo utilizada." }; }
    /// <summary>
    ///    Usuário já possui uma senha definida.
    /// </summary>
    public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Usuário já possui uma senha definida." }; }
    /// <summary>
    ///    Lockout não está habilitado para este usuário.
    /// </summary>
    public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Lockout não está habilitado para este usuário." }; }
    /// <summary>
    ///    Usuário já possui a permissão
    /// </summary>
    public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Usuário já possui a permissão '{role}'." }; }
    /// <summary>
    ///    Usuário não tem a permissão
    /// </summary>
    public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = $"Usuário não tem a permissão '{role}'." }; }
    /// <summary>
    ///    Senhas muito curta
    /// </summary>
    public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Senhas devem conter ao menos {length} caracteres." }; }
    /// <summary>
    ///    Senhas devem conter ao menos um caracter não alfanumérico.
    /// </summary>
    public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Senhas devem conter ao menos um caracter não alfanumérico." }; }
    /// <summary>
    ///    Senhas devem conter ao menos um digito 
    /// </summary>
    public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Senhas devem conter ao menos um digito ('0'-'9')." }; }
    /// <summary>
    ///    Senhas devem conter ao menos um caracter em caixa baixa
    /// </summary>
    public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Senhas devem conter ao menos um caracter em caixa baixa ('a'-'z')." }; }
    /// <summary>
    ///    Valida se a senha contém caracteres maiúsculos
    /// </summary>
    public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Senhas devem conter ao menos um caracter em caixa alta ('A'-'Z')." }; }
}
