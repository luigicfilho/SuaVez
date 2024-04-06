using LCFilaApplication.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCFila.ViewModels
{
    public class FilaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TiposFilas Tipofila { get; set; }
        public FilaStatus Status { get; set; }
        public bool Ativo { get; set; }
        public string TempoMedio { get; set; }
        public Guid UserId { get; set; }
        public Guid EmpresaId { get; set; }

        public string NomeUser { get; set; }
    }
}
