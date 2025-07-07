using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PersonelApplication.Commands;
using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace PersonelApplication.ViewModels
{
    public class DismissPersonViewModel : ViewModelBase, IDataErrorInfo
    {
        private Repository _repository = new Repository();

        private bool _isVerifedEndDate;

        public DismissPersonViewModel(PersonWrapper person = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new AsyncRelayCommand(Confirm);

            SelectedPerson = person;
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(EndDate):
                        if (EndDate.Year < 2010)
                        {
                            Error = "Błędna Data.";
                            _isVerifedEndDate = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isVerifedEndDate = true;
                        }
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }

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

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
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

        private async Task Confirm(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;

            CloseWindow(obj as Window);

            var dialog = await metroWindow.ShowMessageAsync("Zwalnianie Osoby", $"Czy na pewno chcesz zwolnić osobę {SelectedPerson.FirstName} {SelectedPerson.LastName}.", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DissmisPerson(SelectedPerson.Id, EndDate, SelectedPerson.ContractId);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isVerifedEndDate;
            }
        }
    }
}