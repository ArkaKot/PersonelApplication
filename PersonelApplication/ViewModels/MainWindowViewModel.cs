using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PersonelApplication.Commands;
using PersonelApplication.Models;
using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using PersonelApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonelApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private Repository _repository = new Repository();

        private object _actualView;

        public object ActualView
        {
            get { return _actualView; }
            set
            {
                _actualView = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {      
            AddPersonCommand = new RelayCommand(AddEditPersons);
            EditPersonCommand = new RelayCommand(AddEditPersons, CanEditDismissalPerson);
            RefreshPersonCommand = new RelayCommand(RefreshPersons);
            DismissalPersonCommand = new RelayCommand(DismissPersons, CanEditDismissalPerson);
            ContractPersonCommand = new RelayCommand(ContractPerson,canContractPerson);
            SettingsCommand = new RelayCommand(MainSettings);
            AddGroupCommand = new RelayCommand(AddGroups);
            DeleteGroupCommand = new AsyncRelayCommand(DeleteGroup);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            LoadedWindow(null);
        }

        public ICommand AddPersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }
        public ICommand RefreshPersonCommand { get; set; }
        public ICommand DismissalPersonCommand { get; set; }
        public ICommand ContractPersonCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand AddGroupCommand { get; set; }
        public ICommand DeleteGroupCommand { get; set; }

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


        private PersonWrapper _selectedPerson;

        public PersonWrapper SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PersonWrapper> _persons;

        public ObservableCollection<PersonWrapper> Persons
        {
            get { return _persons; }
            set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        private void RefreshPersons(object obj)
        {
            RefreshPerson();
        }

        private bool CanEditDismissalPerson(object obj)
        {
            if (SelectedPerson != null && SelectedPerson.Contract == true)
                return true;
            else
                return false;
        }

        private bool canContractPerson(object obj)
        {
            if (Persons.Any() != true)
                return false;
            else
                return true;

        }

        private void ContractPerson(object obj)
        {

            var contractViewM = new ContractViewModel(ReturnMainView);
            var newContractview = new ContractView { DataContext = contractViewM };

            ActualView = newContractview;
        }

        private void ReturnMainView()
        {
            ActualView = null;
        }

        private void  DismissPersons(object obj)
        {

            var dismissPerson = new DismissPerson(obj as PersonWrapper);
            dismissPerson.Closed += DismissPerson_Closed;
            dismissPerson.ShowDialog();

        }

        private void DismissPerson_Closed(object sender, EventArgs e)
        {
            RefreshPerson();
        }

        private void AddEditPersons(object obj)
        {
            var addEditPersonWindow = new AddEditPersonView(obj as PersonWrapper);
            addEditPersonWindow.Closed += AddEditPersonWindow_Closed;
            addEditPersonWindow.ShowDialog();
        }

        private void AddEditPersonWindow_Closed(object sender, EventArgs e)
        {
            RefreshPerson();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();

            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;

        }

        private void RefreshPerson()
        {
            Persons = new ObservableCollection<PersonWrapper>(_repository.GetPersons(SelectedGroupId));
        }

        private void MainSettings(object obj)
        {
            var settingsWindow = new SettingsView(true);
            settingsWindow.ShowDialog();

        }

        private bool IsValidConnectionToDatabase()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private async void LoadedWindow(object obj)
        {
            if (!IsValidConnectionToDatabase())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var Dialog = await metroWindow.ShowMessageAsync("Błąd połączenia", $"Nie można połączyć się z bazą danych. Czy chcesz zmienić ustawienia?", MessageDialogStyle.AffirmativeAndNegative);

                if (Dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var settingsWindow = new SettingsView(false);
                    settingsWindow.ShowDialog();
                }
            }
            else
            {
                RefreshPerson();

                InitGroups();
            }
          
        }

        private void AddGroups(object obj)
        {
            var addGroup = new AddGroupView();
            addGroup.Closed += AddGroup_Closed;
            addGroup.ShowDialog();
        }

        private void AddGroup_Closed(object sender, EventArgs e)
        {
            RefreshGroup();
        }

        private async Task DeleteGroup(object obj)
        {
            var findGroupName = Groups.FirstOrDefault(x => x.Id == SelectedGroupId);

            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie Grupy", $"Czy na pewno chcesz usunąć Grupę {findGroupName.Name} ? ", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteGroup(SelectedGroupId);

            RefreshGroup();
        }


        private void RefreshGroup()
        {
            Groups = new ObservableCollection<Group>(_repository.GetGroups());
            SelectedGroupId = Groups.FirstOrDefault()?.Id ?? 0;

        }
    }
}
