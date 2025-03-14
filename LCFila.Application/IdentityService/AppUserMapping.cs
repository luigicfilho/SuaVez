using LCFila.Application.Dto;
using LCFila.Domain.Models;

namespace LCFila.Application.IdentityService;

public static class AppUserMapping
{
    public static AppUser ConvertToAppUser(this AppUserDto user)
    {
        AppUser appUser = new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber
        };
        return appUser;
    }

    public static AppUserDto ConvertToAppUserDto(this AppUser user)
    {
        AppUserDto appUser = new()
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!
        };
        return appUser;
    }
}
