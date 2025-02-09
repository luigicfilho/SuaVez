using LCFila.ViewModels;
using LCFila.Web.Mapping;
using LCFila.Web.Models;

//TODO: how to do it in another way to remove theses references
//Don't need all this information in this layer, just the informations
//that need to be passed down
using LCFila.Domain.Models;
using LCFila.Domain.Enums;

namespace LCFila.Mapping;

public static class PessoaMapping
{
    public static Pessoa ConvertToPessoa(this PessoaViewModel? pessoaViewModel)
    {
        Pessoa pessoa = new();
        Fila fila = new();
        if (pessoaViewModel!.Fila is not null)
        {
            var filapessoa = pessoaViewModel.Fila.FirstOrDefault();
            if(filapessoa is not null)
            {
                fila = filapessoa.ConvertToFila();
            }
        }
               
        if (pessoaViewModel is not null) {
            pessoa.Id = pessoaViewModel.Id;
            pessoa.Nome = pessoaViewModel.Nome;
            pessoa.Status = Enum.Parse<PessoaStatus>(Enum.GetName(pessoaViewModel.Status)!);
            pessoa.Celular = pessoaViewModel.Celular;
            pessoa.Documento = pessoaViewModel.Documento;
            //pessoa.Fila = fila;
            pessoa.FilaId = pessoaViewModel.FilaId;
            pessoa.Posicao = pessoaViewModel.Posicao;
            pessoa.Preferencial = pessoaViewModel.Preferencial;
            pessoa.Ativo = pessoaViewModel.Ativo;
        }

        return pessoa;
    }

    public static IEnumerable<PessoaViewModel> ConvertToPessoaViewModelList(this IEnumerable<Pessoa> pessoalist)
    {
        List<PessoaViewModel> listPessoa = [];

        foreach (var pessoa in pessoalist)
        {
            listPessoa.Add(new PessoaViewModel
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Status = Enum.Parse<PessoaStatusViewModel>(Enum.GetName(pessoa.Status)!),
                Celular = pessoa.Celular,
                Documento = pessoa.Documento,
                //Fila = [pessoa.Fila.ConvertToFilaViewModel()],
                FilaId = pessoa.FilaId,
                Posicao = pessoa.Posicao,
                Preferencial = pessoa.Preferencial,
                Ativo = pessoa.Ativo
            });
        }

        return listPessoa;
    }

    public static PessoaViewModel ConvertToPessoaViewModel(this Pessoa? pessoa)
    {
        PessoaViewModel pessoaViewModel = new();
        if (pessoa is not null)
        {
            pessoaViewModel.Id = pessoa!.Id;
            pessoaViewModel.Nome = pessoa.Nome;
            pessoaViewModel.Status = Enum.Parse<PessoaStatusViewModel>(Enum.GetName(pessoa.Status)!);
            pessoaViewModel.Celular = pessoa.Celular;
            pessoaViewModel.Documento = pessoa.Documento;
            //pessoaViewModel.FilaPertence = pessoa.Fila.ConvertToFilaViewModel();
            pessoaViewModel.FilaId = pessoa.FilaId;
            pessoaViewModel.Posicao = pessoa.Posicao;
            pessoaViewModel.Preferencial = pessoa.Preferencial;
            pessoaViewModel.Ativo = pessoa.Ativo;
        }

        return pessoaViewModel!;
    }
}
