using LCFila.Domain.Models;


namespace LCFila.Application.Interfaces;

public interface IUserAppService
{
   List<AppUser> GetListUsers(string UserName);
   (string, AppUser) GetUserAndRole(Guid id);
   bool CreateNewUser(string UserEmail, string Password, int Role);
   bool AtualizarUser(Guid id, AppUser formUser, string Role);
   bool RemoverUser(Guid id, AppUser formUser);
   bool RemoverUser(Guid id);
   bool Logout();
}
