using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    public class ItemAjuste
    {
        public int Codigo { get; set; }
        public int ItemNumero { get; set; }
        public int ProdutoCodigo { get; set; }
        public int Quantidade { get; set; }
        public string Usuario { get; set; }
        public string Endereco { get; set; }
    }
}
