using ConfereEstoque.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Support
{
    public class AjusteEventArgs : EventArgs
    {
        public AjusteEventArgs(Ajuste ajuste)
        {
            Ajuste = ajuste;
        }

        public Ajuste Ajuste { get; set; }
    }
}
