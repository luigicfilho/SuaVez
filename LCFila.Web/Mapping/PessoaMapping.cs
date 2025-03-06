using LCFila.Application.Dto;
using LCFila.Web.Models;

namespace LCFila.Web.Mapping;

public static class PessoaMapping
{
    public static PessoasDto ConvertToPessoaDto(this PessoaViewModel? pessoaViewModel)
    {
        PessoasDto pessoa = new();
        FilaDto fila = new();


        if (pessoaViewModel is not null)
        {
            pessoa.Id = pessoaViewModel.Id;
            pessoa.Nome = pessoaViewModel.Nome;
            pessoa.Status = pessoaViewModel.Status.ToString();
            pessoa.Celular = pessoaViewModel.Celular;
            //pessoa.Documento = pessoaViewModel.Documento;
            //pessoa.Fila = fila;
            //pessoa.FilaId = pessoaViewModel.FilaId;
            pessoa.Posicao = pessoaViewModel.Posicao;
            pessoa.Preferencial = pessoaViewModel.Preferencial;
            pessoa.Ativo = pessoaViewModel.Ativo;
        }

        return pessoa;
    }

    public static FilaPessoaViewModel ConvertToFilaViewModelListVM(this FilaDetailsDto pessoalistdto)
    {
        FilaPessoaViewModel listPessoa = new()
        {
            Id = pessoalistdto.FilaId,
            FilaStatus = pessoalistdto.FilaStatus,
            Pessoas = pessoalistdto.ListaPessoas.ConvertToPessoaViewModelListVM()
        };

        return listPessoa;
    }

    public static PessoaViewModel ConvertToPessoaViewModel(this PessoasDto? pessoa)
    {
        PessoaViewModel pessoaViewModel = new();
        if (pessoa is not null)
        {
            pessoaViewModel.Id = pessoa!.Id;
            pessoaViewModel.Nome = pessoa.Nome;
            //pessoaViewModel.Status = Enum.Parse<PessoaStatusViewModel>(Enum.GetName(pessoa.Status)!);
            pessoaViewModel.Status = pessoa.Status;
            pessoaViewModel.Celular = pessoa.Celular;
            //pessoaViewModel.Documento = pessoa.Documento;
            //pessoaViewModel.FilaPertence = pessoa.Fila.ConvertToFilaViewModel();
            //pessoaViewModel.FilaId = pessoa.FilaId;
            pessoaViewModel.Posicao = pessoa.Posicao;
            pessoaViewModel.Preferencial = pessoa.Preferencial;
            pessoaViewModel.Ativo = pessoa.Ativo;
        }

        return pessoaViewModel!;
    }

    internal static IEnumerable<PessoaViewModel> ConvertToPessoaViewModelListVM(this List<PessoasDto> pessoalist)
    {
        List<PessoaViewModel> listPessoa = [];

        foreach (var pessoa in pessoalist)
        {
            listPessoa.Add(new PessoaViewModel
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Status = pessoa.Status,
                Celular = pessoa.Celular,
                //Documento = pessoa.Documento,
                //Fila = [pessoa.Fila.ConvertToFilaViewModel()],
                //FilaId = pessoa.FilaId,
                Posicao = pessoa.Posicao,
                Preferencial = pessoa.Preferencial,
                Ativo = pessoa.Ativo
            });
        }

        return listPessoa;
    }
}
