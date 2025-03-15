using LCFila.Application.Dto;
using LCFila.Domain.Models;

namespace LCFila.Application.Mappers;

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

    public static List<AppUser> ConvertToAppUsers(this List<AppUserDto> usersDto)
    {
        List<AppUser> appUsers = [];

        foreach(var user in usersDto)
        {
            appUsers.Add(new()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            });
        }
        return appUsers;
    }

    public static List<AppUserDto> ConvertToAppUsersDto(this List<AppUser> usersDto)
    {
        List<AppUserDto> appUsers = [];

        foreach (var user in usersDto)
        {
            appUsers.Add(new()
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber!
            });
        }
        return appUsers;
    }
}
