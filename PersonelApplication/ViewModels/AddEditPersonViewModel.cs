using PersonelApplication.Commands;
using PersonelApplication.Models;
using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PersonelApplication.ViewModels
{
    public class AddEditPersonViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();


        public AddEditPersonViewModel(PersonWrapper person = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (person == null)
            {
                Person = new PersonWrapper();
            }
            else
            {
                Person = person;
                IsUpdate = true;
            }

            InitGroups();
            InitPersons();
        }

       

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private PersonWrapper _person;

        public PersonWrapper Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Group> _group;

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();

            groups.Insert(0, new Group { Id = 0, Name = "-- Brak --" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Person.GroupId;

        }

        private void Confirm(object obj)
        {
            if (!Person.IsValid)
               return;

            if (!IsUpdate)
                AddPerson();
            else
                UpdatePerson();


            CloseWindow(obj as Window);

        }

        private void UpdatePerson()
        {
            _repository.UpdatePerson(Person);
        }

        private void AddPerson()
        {
            _repository.AddPerson(Person);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitPersons()
        {
            var persons = _repository.GetPersonsToContract();
        }
    }
}
