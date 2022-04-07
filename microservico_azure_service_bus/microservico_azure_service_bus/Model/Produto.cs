using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microservico_azure_service_bus.Model
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
