using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using ConfereEstoque.Utils;
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
    public class ConferenciaViewModel : ViewModelBase
    {
        private NetServices _netServices;
        public ConferenciaViewModel()
        {
            _netServices = new NetServices();
            IpLista = new ObservableCollection<string>(_netServices.RelacaoIPs());
            if(IpLista.Count > 0)
                IpServidor = _ipLista[0];
            this.PropertyChanged += ConferenciaViewModel_PropertyChanged;

            GerarArquivoCommand = new DelegateCommand<object>(GerarArquivoCommandExecute, GerarArquivoCommandCanExecute);
            SelecionarPastaCommand = new DelegateCommand<object>(SelecionarPastaCommandExecute);
            
            Messenger.Default.Register<ItemAjuste>("ItemRecebido", itemRecebido);
        }

        private void GerarArquivoCommandExecute(object obj)
        {
            //Create File
            _FileServices.GenerateFile(SelectedFolder, Ajuste);
        }

        private bool GerarArquivoCommandCanExecute(object obj)
        {
            bool result = (SelectedFolder != "" && SelectedFolder != null);
            return result;
        }

        private void SelecionarPastaCommandExecute(object obj)
        {
            SelectedFolder = _SelectFolderService.GetFolderSelected(); 
        }

        private void ConferenciaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IpServidor" || e.PropertyName == "Ajuste")
            {
                if(IpServidor != null && Ajuste != null)
                {
                    CanStart = true;
                }
                else
                {
                    if (ServerRunning)
                        ServerRunning = false;

                    CanStart = false;
                }
            }
        }
        [Import]
        FileServices _FileServices;
        [Import]
        SelectFolderService _SelectFolderService;
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        private void itemRecebido(ItemAjuste item)
        {
            if (Ajuste != null)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    bool found = false;
                    ItemAjuste itemAjuste = Ajuste.Itens.SingleOrDefault(i => i.Produto.CodigoProduto == item.Produto.CodigoProduto);
                    if (itemAjuste != null)
                    {
                        itemAjuste.Quantidade = item.Quantidade;
                    }
                    else
                    {
                        Ajuste.Itens.Add(item);
                    }
                });
                //var repo = _DataRepositoryFactory.GetDataRepository<IAjusteRepository>();
                //var ajuste = repo.Get(Ajuste.Codigo);
                //Ajuste.Itens = ajuste.Itens;
            }
        }
        public Ajuste Ajuste
        {
            get
            {
                return AjusteContext.Ajuste;
            }
            set
            {
                AjusteContext.Ajuste = value;
                OnPropertyChanged(() => Ajuste);
            }
        }

        private string _SelectedFolder;
        public string SelectedFolder
        {
            get
            {
                return _SelectedFolder;
            }
            set
            {
                _SelectedFolder = value;
                OnPropertyChanged(() => SelectedFolder);
            }
        }

        public DelegateCommand<object> SelecionarPastaCommand { get; private set; }

        public DelegateCommand<object> GerarArquivoCommand { get; private set; }

        private ObservableCollection<string> _ipLista;
        public ObservableCollection<string> IpLista
        {
            get { return _ipLista; }
            set { _ipLista = value; OnPropertyChanged(() => IpLista); }
        }
        private string _ipServidor;
        public string IpServidor
        {
            get { return _ipServidor; }
            set { _ipServidor = value; OnPropertyChanged(() => IpServidor); }
        }
        private bool _serverRunning;
        public bool ServerRunning
        {
            get { return _serverRunning; }
            set
            {
                try
                {
                    if (value == true)
                    {
                        //StartServer
                        Servidor.Start(IpServidor);
                    }
                    else
                    {
                        //StopServer
                        Servidor.Finish();
                    }
                    _serverRunning = value;
                    OnPropertyChanged(() => ServerRunning);
                }
                catch(Exception ex)
                {

                }
            }
        }
        private bool _CanStart;
        public bool CanStart
        {
            get
            {
                return _CanStart;
            }
            set
            {
                _CanStart = value;
                OnPropertyChanged(() => CanStart);
            }
        }
    }
}
