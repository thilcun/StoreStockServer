using ConfereEstoque.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    public class ItemAjuste : ObjectBase
    {
        public int Codigo { get; set; }
        [ForeignKey("AjusteId")]
        public Ajuste Ajuste { get; set; }
        public int AjusteId { get; set; }
        public int ItemNumero { get; set; }
        public Produto Produto { get; set; }
        private int _Quantidade;
        public int Quantidade
        {
            get { return _Quantidade; }
            set { _Quantidade = value; OnPropertyChanged(() => Quantidade); }
        }
        public string Usuario { get; set; }
        public string Endereco { get; set; }
        public DateTime Alterado { get; set; }
    }
}
