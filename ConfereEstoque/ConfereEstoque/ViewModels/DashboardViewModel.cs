using ConfereEstoque.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public DashboardViewModel()
        {

        }

        public override string ViewTitle
        {
            get { return "Dashboard"; }
        }

        public Ajuste Ajuste { get; set; }
    }
}
