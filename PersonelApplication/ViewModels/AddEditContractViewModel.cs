using PersonelApplication.Commands;
using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PersonelApplication.ViewModels
{
    public class AddEditContractViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddEditContractViewModel(ContractWrapper contract = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (contract == null)
            {
                Contract = new ContractWrapper();
                Contract.StartDate = DateTime.Now;
            }
            else
            {
                Contract = contract;
                IsUpdateContract = true;
            }

            InitType();
            InitPerson();
            
        }

        public ICommand CloseCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        private ContractWrapper _contract;

        public ContractWrapper Contract
        {
            get
            {
                return _contract;
            }
            set
            {
                _contract = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdateContract;

        public bool IsUpdateContract
        {
            get { return _isUpdateContract; }
            set
            {
                _isUpdateContract = value;
                OnPropertyChanged();
            }
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


        private ObservableCollection<Person> _person;

        public ObservableCollection<Person> Persons
        {
            get { return _person; }
            set
            {
                _person = value;
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

        private ObservableCollection<TypeContract> _typeContractPerson;

        public ObservableCollection<TypeContract> TypeContractPerson
        {
            get { return _typeContractPerson; }
            set
            {
                _typeContractPerson = value;
                OnPropertyChanged();
            }
        }

        private void Confirm(object obj)
        {
            if (!Contract.IsValid)
                return;

            if (!IsUpdateContract)
            {
                Contract.Person = null;
                AddContract();
            }

            else
                UpdateContract();


            CloseWindow(obj as Window);
        }

        private void UpdateContract()
        {
            _repository.UpdateContract(Contract);
        }

        private void AddContract()
        {
            _repository.AddContract(Contract);

        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }


        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitType()
        {
            var typesContract = _repository.GetTypeContracts();

            typesContract.Insert(0, new TypeContract { Id = 0, Name = "-- Brak --" });

            TypeContractPerson = new ObservableCollection<TypeContract>(typesContract);

            SelectedTypeContractId = Contract.TypeContract.Id;
        }

        private void InitPerson()
        {
            var personToContract = _repository.GetPersonsToContract();

            if (!IsUpdateContract)
            {
                 personToContract = _repository.GetPersonsAddToContract();
            }

                personToContract.Insert(0, new Person { Id = 0, FirstName = "-- Brak --" });

                Persons = new ObservableCollection<Person>(personToContract);

                SelectedPersonId = Contract.PersonId;
        }
    }
}