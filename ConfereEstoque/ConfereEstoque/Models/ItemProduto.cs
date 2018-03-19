using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Models
{
    public class ItemProduto
    {
        public Produto Produto { get; set; }
        public int AjusteId { get; set; }
        public int Quantidade { get; set; }
    }
}
