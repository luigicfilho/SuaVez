using AutoMapper;
using LCFila.ViewModels;
using LCFilaApplication.Models;

namespace LCFila.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
            CreateMap<Fila, FilaViewModel>().ReverseMap();
            CreateMap<FilaPessoa, FilaPessoaViewModel>().ReverseMap();
            CreateMap<EmpresaLogin, EmpresaLoginViewModel>().ReverseMap();
            CreateMap<EmpresaConfiguracao, EmpresaConfiguracaoViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
        }
    }
}
