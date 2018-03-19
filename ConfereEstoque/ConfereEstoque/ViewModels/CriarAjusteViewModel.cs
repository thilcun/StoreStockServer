using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using ConfereEstoque.Entities;
using ConfereEstoque.Support;
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
    public class CriarAjusteViewModel : ViewModelBase
    {
        private IDataRepositoryFactory _dataRepositoryFactory;
        [ImportingConstructor]
        public CriarAjusteViewModel(IDataRepositoryFactory dataRepositoryFactory)
        {
            Ajuste = new Ajuste();
            _dataRepositoryFactory = dataRepositoryFactory;

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute, OnCancelCommandCanExecute);
        }
        
        private bool OnCancelCommandCanExecute(object obj)
        {
            return true;
        }

        private void OnCancelCommandExecute(object obj)
        {    
            Ajuste = new Ajuste();

            if (AjusteCancelado != null)
                AjusteCancelado(this, null);
        }

        private bool OnSaveCommandCanExecute(object obj)
        {
            if(Ajuste != null)
            {
                if (Ajuste.Observacao != null)
                    return true;
            }

            return false;
        }

        private void OnSaveCommandExecute(object obj)
        {
            IAjusteRepository ajusteRepository = _dataRepositoryFactory.GetDataRepository<IAjusteRepository>();
            Ajuste novoAjuste = new Ajuste { Codigo = Ajuste.Codigo, Observacao = Ajuste.Observacao, DataCriado = DateTime.Now };
            if (novoAjuste.Codigo == default(int))
                ajusteRepository.Add(novoAjuste);
            else
                ajusteRepository.Update(novoAjuste);

            if (novoAjuste.Codigo != default(int))
                Ajuste = novoAjuste;

            if (AjusteCriado != null)
                AjusteCriado(this, new AjusteEventArgs(Ajuste));
        }

        private Ajuste _ajuste;
        public Ajuste Ajuste
        {
            get
            {
                return _ajuste;
            }
            set
            {
                _ajuste = value;
                OnPropertyChanged(() => Ajuste, true);
            }
        }
        
        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }


        public event EventHandler<AjusteEventArgs> AjusteCriado;

        public event EventHandler AjusteCancelado;
    }
}
