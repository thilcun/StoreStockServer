using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    [ComplexType]
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public Marca Marca { get; set; }
        public string CodigoBarra { get; set; }
        public string Ncm { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
