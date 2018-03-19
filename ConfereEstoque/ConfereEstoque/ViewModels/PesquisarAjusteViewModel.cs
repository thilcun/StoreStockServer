using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using ConfereEstoque.Entities;
using ConfereEstoque.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PesquisarAjusteViewModel : ViewModelBase
    {
        private IDataRepositoryFactory _dataRepositoryFactory;
        [ImportingConstructor]
        public PesquisarAjusteViewModel(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            Ajustes = new ObservableCollection<Ajuste>();
            SearchCommand = new DelegateCommand<string>(OnSearchCommandExecute);
            SelectedCommand = new DelegateCommand<Ajuste>(OnSelectedCommandExecute);
        }

        private void OnSelectedCommandExecute(Ajuste ajuste)
        {
            if (AjusteSelected != null)
                AjusteSelected(this, new AjusteEventArgs(ajuste));
        }

        private async void OnSearchCommandExecute(string query)
        {
            IAjusteRepository ajusteRepository = _dataRepositoryFactory.GetDataRepository<IAjusteRepository>();
            //var ajustes = ajusteRepository.Get().Where(o => o.Observacao.Contains(query));
            var ajustes = await ajusteRepository.GetAsync();//.Where(o => o.Observacao.Contains(query));
            
            Ajustes = new ObservableCollection<Ajuste>(ajustes.Where(o => o.Observacao.Contains(query)).ToList());
        }

        public DelegateCommand<string> SearchCommand { get; private set; }
        public DelegateCommand<Ajuste> SelectedCommand { get; private set; }

        public event EventHandler<AjusteEventArgs> AjusteSelected;

        private ICollection<Ajuste> _Ajustes;
        public ICollection<Ajuste> Ajustes
        {
            get
            {
                return _Ajustes;
            }
            set
            {
                _Ajustes = value;
                OnPropertyChanged(() => Ajustes);
            }
        }


    }
}
