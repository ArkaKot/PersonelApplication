using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using PersonelApplication.Commands;
using PersonelApplication.Models;
using PersonelApplication.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using PersonelApplication.Models.Wrappers;
using PersonelApplication.Models.Domains;

namespace PersonelApplication.ViewModels
{
    public class ContractViewModel : ViewModelBase
    {

        private Repository _repository = new Repository();

        private readonly Action _returnMainWindowView;


       public ContractViewModel()
        {
        } 

        public ContractViewModel(Action returnMainWindowView)
        {

            AddContractCommand = new RelayCommand(AddEditContract);
            EditContractCommand = new RelayCommand(AddEditContract, CanEditContract);
            DeleteContractCommand = new AsyncRelayCommand(DeleteContract, CanEditContract);
            RefreshContractCommand = new RelayCommand(RefreshListContracts);

            _returnMainWindowView = returnMainWindowView;
            ChangeViewPersonCommand = new RelayCommand(ReturnMainView);

            RefreshListContract();


            InitType();

        }

        private int _selectedPersonId;

        public int SelectedPersonId
        {
            get { return _selectedPersonId; }
            set
            {
                _selectedPersonId = value;
                OnPropertyChanged();

            }
        }

        private int _selectedTypeContractId;

        public int SelectedTypeContractId
        {
            get { return _selectedTypeContractId; }
            set
            {
                _selectedTypeContractId = value;
                OnPropertyChanged();

            }
        }

        private ContractWrapper _selectedContracts;

        public ContractWrapper SelectedContract
        {
            get { return _selectedContracts; }
            set { 
                _selectedContracts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TypeContract> _typeContract;

        public ObservableCollection<TypeContract> TypeContract
        {
            get { return _typeContract; }
            set
            {
                _typeContract = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ContractWrapper> _contract;

        public ObservableCollection<ContractWrapper> Contract
        {
            get { return _contract; }
            set
            {
                _contract = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddContractCommand { get; set; }
        public ICommand EditContractCommand { get; set; }
        public ICommand DeleteContractCommand { get; set; } 
        public ICommand RefreshContractCommand { get; set; }
        public ICommand ChangeViewPersonCommand { get; set; }

        private void AddEditContract(object obj)
        {

            var addEditContractWindow = new AddEditContractView(obj as ContractWrapper);
            addEditContractWindow.Closed += AddEditContractWindow_Closed;
            addEditContractWindow.ShowDialog();
        }

        private void AddEditContractWindow_Closed(object sender, EventArgs e)
        {
            RefreshListContract();
        }

        private void RefreshListContract()
        {

            Contract = new ObservableCollection<ContractWrapper>(_repository.GetContracts(SelectedTypeContractId));
           
        }

        private void ReturnMainView(object obj)
        {
            _returnMainWindowView?.Invoke();

        }

        private bool CanEditContract(object obj)
        {
            return SelectedContract != null;
        }

        private void RefreshListContracts(object obj)
        {
            RefreshListContract();
        }

        private async Task DeleteContract(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie Kontraktu", $"Czy na pewno chcesz usunąć kontrakt o numerze {SelectedContract.NumberContract} ?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteContract(SelectedContract.Id);

            RefreshListContract();
        }

        private void InitType()
        {

            var typeContract = _repository.GetTypeContracts();

            typeContract.Insert(0, new TypeContract { Id = 0, Name = "-- Brak --" });

            TypeContract = new ObservableCollection<TypeContract>(typeContract);

            SelectedTypeContractId = 0;

        }

    }
}
