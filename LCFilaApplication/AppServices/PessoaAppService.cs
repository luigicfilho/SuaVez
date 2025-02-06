using LCFilaApplication.Enums;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Models;
using LCFilaInfra.Interfaces;
using System.Linq.Expressions;

namespace LCFilaApplication.AppServices
{
    internal class PessoaAppService : IPessoaAppService
    {
        public IPessoaRepository _pessoaRepository { get; }

        public PessoaAppService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }
        public bool Atender(Guid id, Guid filaid)
        {
            var pessoa = _pessoaRepository.ObterPorId(id).Result;
            pessoa.Ativo = false;
            pessoa.Status = PessoaStatus.Atendido;
            var result = _pessoaRepository.Atualizar(pessoa);
            if (result.IsCompletedSuccessfully)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Chamar(Guid id, Guid filaid)
        {
            try
            {
                var pessoa = GetDetails(id, filaid);
                var pessoas = Buscar(p => p.FilaId == filaid && p.Ativo == true && p.Status == PessoaStatus.Esperando);

                foreach (var item in pessoas.OrderBy(p => p.Preferencial))
                {
                    if (item.Id == id)
                    {
                        item.Posicao = 0;
                        item.Status = PessoaStatus.Chamado;
                    }
                    else
                    {
                        item.Posicao = item.Posicao - 1;
                    }
                    Atualizar(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Pessoa GetDetails(Guid Id, Guid filaid)
        {
            return ObterPorId(Id);
        }

        public bool Pular(Guid id, Guid filaid)
        {
            try
            {
                var pessoa = ObterPorId(id);
                var pessoas = Buscar(p => p.FilaId == filaid && p.Ativo == true && (p.Status == PessoaStatus.Esperando || p.Status == PessoaStatus.Chamado));
                var posicaopessoadel = pessoa.Posicao;
                foreach (var item in pessoas.OrderBy(p => p.Preferencial))
                {
                    if (item.Id == id)
                    {
                        if (item.Status == PessoaStatus.Chamado)
                        {
                            item.Posicao = 1;
                            item.Status = PessoaStatus.Esperando;
                        }
                        else
                        {
                            item.Posicao = item.Posicao + 1;
                        }
                    }
                    else
                    {
                        if (item.Posicao > posicaopessoadel)
                        {
                            if (item.Posicao == 1)
                            {
                                item.Posicao = 0;
                                item.Status = PessoaStatus.Chamado;
                            }
                            else
                            {
                                item.Posicao = item.Posicao - 1;
                            }
                        }
                    }
                    Atualizar(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool Remover(Guid id, Guid filaid)
        {
            try
            {
                var pessoa = ObterPorId(id);
                var pessoas = Buscar(p => p.FilaId == filaid && p.Ativo == true && (p.Status == PessoaStatus.Esperando || p.Status == PessoaStatus.Chamado));
                var posicaopessoadel = pessoa.Posicao;
                foreach (var item in pessoas.OrderBy(p => p.Preferencial))
                {
                    if (item.Id == id)
                    {
                        item.Ativo = false;
                        item.Status = PessoaStatus.Removido;
                    }
                    else
                    {
                        if (item.Posicao > posicaopessoadel)
                        {
                            item.Posicao = item.Posicao - 1;
                        }
                    }
                    Atualizar(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal Pessoa ObterPorId(Guid Id) 
        {
            return _pessoaRepository.ObterPorId(Id).Result;
        }

        internal IEnumerable<Pessoa> Buscar(Expression<Func<Pessoa, bool>> predicate)
        {
            return _pessoaRepository.Buscar(predicate).Result;
        }

        internal bool Atualizar(Pessoa pessoa)
        {
            try
            {
                _pessoaRepository.Atualizar(pessoa);
                return true;
            }
            catch (Exception)
            {
                return false;
            }  
        }
    }
}
