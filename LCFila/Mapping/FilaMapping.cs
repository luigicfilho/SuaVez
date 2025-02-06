using LCFila.ViewModels;
//TODO: how to do it in another way to remove theses references
using LCFilaApplication.Models;

namespace LCFilaApplication.Mapping;

public static class FilaMapping
{
    public static Fila ConvertToFila(this FilaViewModel filaViewModel)
    {
        Fila fila = new()
        {
            Id = filaViewModel.Id,
            Nome = filaViewModel.Nome,
            Status = filaViewModel.Status,
            TempoMedio = filaViewModel.TempoMedio,
            Tipofila = filaViewModel.Tipofila,
            UserId = filaViewModel.UserId,
            Ativo = filaViewModel.Ativo,
            DataFim = filaViewModel.DataFim,
            DataInicio = filaViewModel.DataInicio,
            EmpresaId = filaViewModel.EmpresaId
        };

        return fila;
    }
    public static List<Fila> ConvertToListFila(this List<FilaViewModel> filaViewModellist)
    {
        List<Fila> listfila = [];

        foreach (var fila in filaViewModellist)
        {
            listfila.Add(new Fila
            {
                Id = fila.Id,
                Nome = fila.Nome,
                Status = fila.Status,
                TempoMedio = fila.TempoMedio,
                Tipofila = fila.Tipofila,
                UserId = fila.UserId,
                Ativo = fila.Ativo,
                DataFim = fila.DataFim,
                DataInicio = fila.DataInicio,
                EmpresaId = fila.EmpresaId
            });
        }

        return listfila;
    }

    public static FilaViewModel ConvertToFilaViewModel(this Fila fila)
    {
        FilaViewModel filaViewModel = new()
        {
            Id = fila.Id,
            Nome = fila.Nome,
            Status = fila.Status,
            TempoMedio = fila.TempoMedio,
            Tipofila = fila.Tipofila,
            UserId = fila.UserId,
            Ativo = fila.Ativo,
            DataFim = fila.DataFim,
            DataInicio = fila.DataInicio,
            EmpresaId = fila.EmpresaId
        };

        return filaViewModel;
    }

    public static List<FilaViewModel> ConvertToListFilaViewModel(this List<Fila> filalist)
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
                    Tipofila = fila.Tipofila,
                    UserId = fila.UserId,
                    Ativo = fila.Ativo,
                    DataFim = fila.DataFim,
                    DataInicio = fila.DataInicio,
                    EmpresaId = fila.EmpresaId
                });
            }
        }
        return listfila;
    }
}
