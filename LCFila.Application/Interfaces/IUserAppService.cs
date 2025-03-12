using LCFila.Application.Dto;
using LCFila.Domain.Models;


namespace LCFila.Application.Interfaces;

public interface IUserAppService
{
   List<AppUserDto> GetListUsers(string UserName);
   (string, AppUserDto) GetUserAndRole(Guid id);
   bool CreateNewUser(string UserEmail, string Password, int Role, string userLoggedIn);
   bool AtualizarUser(Guid id, AppUserDto formUser, string Role);
   bool RemoverUser(Guid id, AppUserDto formUser);
   bool RemoverUser(Guid id);
   bool Logout();
}
