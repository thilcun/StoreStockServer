using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    [ComplexType]
    public class Marca
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
