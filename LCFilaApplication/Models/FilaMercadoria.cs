using System.Collections.Generic;

namespace LCFilaApplication.Models
{
    public class FilaMercadoria : Entity
    {
        public Fila FiladeMercadorias { get; set; }
        /* EF Relations */
        public IEnumerable<Mercadoria> Mercadorias { get; set; }
    }
}
