using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCFila.ViewModels
{
    public class FilaPessoaViewModel
    {
        public FilaViewModel FiladePessoas { get; set; }
        public IEnumerable<PessoaViewModel> Pessoas { get; set; }
    }
}
