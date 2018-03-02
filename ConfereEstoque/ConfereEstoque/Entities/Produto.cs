using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public string Ncm { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
