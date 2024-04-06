using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LCFilaApplication.Repository
{
    public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(FilaDbContext context) : base(context) { }

        //public async Task<Pessoa> ObterProdutoFornecedor(Guid id)
        //{
        //    return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
        //        .FirstOrDefaultAsync(p => p.Id == id);
        //}

        //public async Task<IEnumerable<Pessoa>> ObterProdutosFornecedores()
        //{
        //    return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
        //        .OrderBy(p => p.Nome).ToListAsync();
        //}

        //public async Task<IEnumerable<Pessoa>> ObterProdutosPorFornecedor(Guid fornecedorId)
        //{
        //    return await Buscar(p => p.FornecedorId == fornecedorId);
        //}
       
    }
}
