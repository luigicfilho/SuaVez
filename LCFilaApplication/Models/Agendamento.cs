using System;
using System.Collections.Generic;
using System.Text;

namespace LCFilaApplication.Models
{
    public class Agendamento: Entity
    {
        public DateTime DatadoAgendamento { get; set; }
        public Pessoa Pessoanodia { get; set; }
    }
}
