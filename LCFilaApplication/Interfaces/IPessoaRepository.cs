using LCAppFila.Domain.Interfaces;
using LCFilaApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCFilaApplication.Interfaces
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        //Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        //Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        //Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}
