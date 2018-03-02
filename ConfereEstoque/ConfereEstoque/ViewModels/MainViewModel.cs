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
    public class MainViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public MainViewModel(DashboardViewModel dashboardViewModel)
        {

            ViewModelAtivo = dashboardViewModel;
        }

        public ViewModelBase ViewModelAtivo { get; private set; }

        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }
        [Import]
        public CriarAjusteViewModel CriarAjusteViewModel { get; private set; }
        [Import]
        public PesquisarAjusteViewModel PesquisarAjusteViewModel { get; private set; }
    }

}
