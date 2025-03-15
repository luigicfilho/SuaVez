using LCFila.Application.Dto;
using LCFila.Domain.Enums;
using LCFila.Domain.Models;

namespace LCFila.Application.Mappers;

public static class FilaMappings
{
    public static List<Fila> ConvertToFilas(this List<FilaDto> FilaDtos)
    {
        List<Fila> filalist = [];
        foreach (var filas in FilaDtos)
        {
            filalist.Add(new()
            {
                Id = filas.Id,
                Nome = filas.Nome,
                DataInicio = filas.DataInicio,
                DataFim = filas.DataFim,
                Tipofila = Enum.Parse<TiposFilas>(filas.Tipofila),
                Status = Enum.Parse<FilaStatus>(filas.Status),
                Ativo = filas.Ativo,
                TempoMedio = filas.TempoMedio
            });
        }
        return filalist;
    }

    public static List<FilaDto> ConvertToFilasDto(this List<Fila> FilaDtos)
    {
        List<FilaDto> filalist = [];
        foreach (var filas in FilaDtos)
        {
            filalist.Add(new()
            {
                Id = filas.Id,
                Nome = filas.Nome,
                DataInicio = filas.DataInicio,
                DataFim = filas.DataFim,
                Tipofila = filas.Tipofila.ToString(),
                Status = filas.Status.ToString(),
                Ativo = filas.Ativo,
                TempoMedio = filas.TempoMedio
            });
        }
        return filalist;
    }
}
