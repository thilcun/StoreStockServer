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
        public MainViewModel()//ConferenciaViewModel conferenciaViewModel, CriarAjusteViewModel criarAjusteViewModel, PesquisarAjusteViewModel pesquisarAjusteViewModel)
        {
            //ConferenciaViewModel = conferenciaViewModel;
            //CriarAjusteViewModel = criarAjusteViewModel;
            //PesquisarAjusteViewModel = pesquisarAjusteViewModel;

            CriarAjusteCommand = new DelegateCommand<object>(OnCriarAjusteCommand, CanCriarAjusteCommand);
            PesquisarAjusteCommand = new DelegateCommand<object>(OnPesquisarAjusteCommand, CanPesquisarAjusteCommand);

            ViewModelAtivo = ConferenciaViewModel;
        }
        
        private void PesquisarAjusteViewModel_AjusteSelected(object sender, Support.AjusteEventArgs e)
        {
            ConferenciaViewModel.Ajuste = e.Ajuste;
            ViewModelAtivo = ConferenciaViewModel;
        }

        private bool CanPesquisarAjusteCommand(object obj)
        {
            if (ViewModelAtivo != null)
                return (ViewModelAtivo.GetType() != typeof(PesquisarAjusteViewModel));
            else
                return true;
        }

        private bool CanCriarAjusteCommand(object obj)
        {
            if (ViewModelAtivo != null)
                return (ViewModelAtivo.GetType() != typeof(CriarAjusteViewModel));
            else
                return true;
        }

        private void OnPesquisarAjusteCommand(object obj)
        {
            PesquisarAjusteViewModel.AjusteSelected += PesquisarAjusteViewModel_AjusteSelected;
            ViewModelAtivo = PesquisarAjusteViewModel;
        }
        
        private void OnCriarAjusteCommand(object obj)
        {
            CriarAjusteViewModel.AjusteCriado += CriarAjusteViewModel_AjusteCriado;
            CriarAjusteViewModel.AjusteCancelado += CriarAjusteViewModel_AjusteCancelado;
            ViewModelAtivo = CriarAjusteViewModel;
        }

        private void CriarAjusteViewModel_AjusteCancelado(object sender, EventArgs e)
        {
            ViewModelAtivo = ConferenciaViewModel;
        }

        private void CriarAjusteViewModel_AjusteCriado(object sender, Support.AjusteEventArgs e)
        {
            ConferenciaViewModel.Ajuste = e.Ajuste;
            ViewModelAtivo = ConferenciaViewModel;
        }

        private ViewModelBase _viewModelAtivo;
        public ViewModelBase ViewModelAtivo
        {
            get
            {
                return _viewModelAtivo;
            }
            set
            {
                _viewModelAtivo = value;
                OnPropertyChanged(() => ViewModelAtivo);
            }
        }

        
        [Import]
        public ConferenciaViewModel ConferenciaViewModel { get; private set; }
        [Import]
        public CriarAjusteViewModel CriarAjusteViewModel { get; private set; }
        [Import]
        public PesquisarAjusteViewModel PesquisarAjusteViewModel { get; private set; }

        public DelegateCommand<object> CriarAjusteCommand { get; private set; }

        public DelegateCommand<object> PesquisarAjusteCommand { get; private set; }
    }

}
