using LCFila.Application.Dto;
using LCFila.Domain.Models;
using LCFila.Domain.Enums;
using LCFila.Web.Models;

namespace LCFila.Web.Mapping;

public static class FilaMapping
{
    public static FilaDto ConvertToFila(this FilaViewModel filaViewModel)
    {
        FilaDto fila = new()
        {
            Id = filaViewModel.Id,
            Nome = filaViewModel.Nome,
            Status = filaViewModel.Status,
            TempoMedio = filaViewModel.TempoMedio,
            Ativo = filaViewModel.Ativo,
            DataInicio = filaViewModel.DataInicio
        };

        return fila;
    }

    public static FilaDto ConvertToFilaVM(this CreateFilaViewModel filaViewModel)
    {
        FilaDto fila = new()
        {
            Nome = filaViewModel.Nome
        };

        return fila;
    }

    public static List<FilaDto> ConvertToListFila(this List<FilaViewModel> filaViewModellist)
    {
        List<FilaDto> listfila = [];

        foreach (var fila in filaViewModellist)
        {
            listfila.Add(new FilaDto
            {
                Id = fila.Id,
                Nome = fila.Nome,
                Status = fila.Status,
                TempoMedio = fila.TempoMedio,
                Ativo = fila.Ativo,
                DataInicio = fila.DataInicio
            });
        }

        return listfila;
    }

    public static FilaViewModel ConvertToFilaViewModel(this FilaDto fila)
    {
        FilaViewModel filaViewModel = new()
        {
            Id = fila.Id,
            Nome = fila.Nome,
            Status = fila.Status,
            TempoMedio = fila.TempoMedio,
            Ativo = fila.Ativo,
            DataInicio = fila.DataInicio
        };

        return filaViewModel;
    }

    public static List<FilaViewModel> ConvertToListFilaViewModel(List<FilaDto> filalist)
    {
        List<FilaViewModel> listfila = [];

        if(filalist is not null)
        {
            foreach (var fila in filalist)
            {
                listfila.Add(new FilaViewModel
                {
                    Id = fila.Id,
                    Nome = fila.Nome,
                    Status = fila.Status,
                    TempoMedio = fila.TempoMedio,
                    Ativo = fila.Ativo,
                    NomeUser = fila.NomeUser,
                    DataInicio = fila.DataInicio
                });
            }
        }
        return listfila;
    }

    public static CreateFilaViewModel ConvertToViewModel(this CreateFilaDto createFilaDto)
    {
        CreateFilaViewModel createFilaViewModel = new()
        {
            EmpresaId = createFilaDto.EmpresaId,
            UserId = createFilaDto.UserId
        };

        return createFilaViewModel;
    }

    //TODO: Remove
    public static FilaViewModel ConvertToFilaViewModelVM(this Fila fila)
    {
        FilaViewModel filaViewModel = new()
        {
            Id = fila.Id,
            Nome = fila.Nome,
            Status = Enum.GetName(fila.Status)!,
            TempoMedio = fila.TempoMedio,
            Ativo = fila.Ativo,
            DataInicio = fila.DataInicio
        };

        return filaViewModel;
    }

    public static List<Fila> ConvertToListFilaVM(this List<FilaViewModel> filaViewModellist)
    {
        List<Fila> listfila = [];

        foreach (var fila in filaViewModellist)
        {
            listfila.Add(new Fila
            {
                Id = fila.Id,
                Nome = fila.Nome,
                Status = Enum.Parse<FilaStatus>(fila.Status!),
                TempoMedio = fila.TempoMedio,
                Ativo = fila.Ativo,
                DataInicio = fila.DataInicio
            });
        }

        return listfila;
    }
}
