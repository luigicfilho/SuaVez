using LCFila.ViewModels;
using LCFilaApplication.Mapping;
using LCFilaApplication.Models;

namespace LCFila.Mapping;

public static class PessoaMapping
{
    public static Pessoa ConvertToPessoa(this PessoaViewModel pessoaViewModel)
    {
        Pessoa pessoa = new()
        {
            Id = pessoaViewModel.Id,
            Nome = pessoaViewModel.Nome,
            Status = pessoaViewModel.Status,
            Celular = pessoaViewModel.Celular,
            Documento = pessoaViewModel.Documento,
            Fila = pessoaViewModel.Fila.FirstOrDefault().ConvertToFila(),
            FilaId = pessoaViewModel.filaid,
            Posicao = pessoaViewModel.Posicao,
            Preferencial = pessoaViewModel.Preferencial,
            Ativo = pessoaViewModel.Ativo
        };

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
                Status = pessoa.Status,
                Celular = pessoa.Celular,
                Documento = pessoa.Documento,
                Fila = [pessoa.Fila.ConvertToFilaViewModel()],
                filaid = pessoa.FilaId,
                Posicao = pessoa.Posicao,
                Preferencial = pessoa.Preferencial,
                Ativo = pessoa.Ativo
            });
        }

        return listPessoa;
    }
}
