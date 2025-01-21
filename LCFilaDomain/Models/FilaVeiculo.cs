using System.Collections.Generic;

namespace LCFilaApplication.Models
{
    public class FilaVeiculo : Entity
    {
        public Fila FiladeVeiculos { get; set; }
        /* EF Relations */
        public IEnumerable<Veiculo> Veiculos { get; set; }
    }
}
