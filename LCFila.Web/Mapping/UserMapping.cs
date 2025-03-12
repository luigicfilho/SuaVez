using LCFila.Application.Dto;
using LCFila.Web.Models.User;

namespace LCFila.Web.Mapping;

public static class UserMapping
{
    public static AppUserViewModel ConvertToViewModel(this AppUserDto user)
    {
        AppUserViewModel UserViewModel = new()
        {
            UserName = user.Email!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!
        };
        return UserViewModel;
    }
    public static AppUserDto ConvertToAppUser(this AppUserViewModel appUserViewModel)
    {
        return new AppUserDto()
        {
            UserName = appUserViewModel.Email,
            Email = appUserViewModel.Email,
            PhoneNumber = appUserViewModel.PhoneNumber
        };
    }

    public static List<AppUserViewModel> ConvertToViewModel(this List<AppUserDto> appUserDtos)
    {
        List<AppUserViewModel> appUserViewModels = [];
        foreach (var appuser in appUserDtos)
        {
            appUserViewModels.Add(new AppUserViewModel
            {
                Id = Guid.Parse(appuser.Id),
                Email = appuser.Email,
                UserName = appuser.UserName,
                PhoneNumber = appuser.PhoneNumber
            });
        }
        return appUserViewModels;
    }

    public static List<AppUserDto> ConvertToDto(this List<AppUserViewModel> appUserViewModels)
    {
        List<AppUserDto> appUserDtoList = [];
        foreach (var appuser in appUserViewModels)
        {
            appUserDtoList.Add(new AppUserDto
            {
                Id = appuser.Id.ToString(),
                Email = appuser.Email,
                UserName = appuser.UserName,
                PhoneNumber = appuser.PhoneNumber
            });
        }
        return appUserDtoList;
    }
}

