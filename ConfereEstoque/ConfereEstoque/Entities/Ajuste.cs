using ConfereEstoque.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Entities
{
    public class Ajuste : ObjectBase
    {
        public Ajuste()
        {
            Itens = new ObservableCollection<ItemAjuste>();
        }
        public int Codigo { get; set; }
        private string _Observacao;
        public string Observacao
        {
            get
            {
                return _Observacao;
            }
            set
            {
                _Observacao = value;
                OnPropertyChanged(() => Observacao);
            }
        }
        private ICollection<ItemAjuste> _Itens;
        [InverseProperty("Ajuste")]
        public ICollection<ItemAjuste> Itens
        {
            get
            {
                return _Itens;
            }
            set
            {
                _Itens = value;
                OnPropertyChanged(() => Itens);
            }
        }
        [NotMapped]
        public int QuantidadeItens
        {
            get
            {
                return Itens.Count;
            }
        }
        public DateTime? DataCriado { get; set; }
    }
}
