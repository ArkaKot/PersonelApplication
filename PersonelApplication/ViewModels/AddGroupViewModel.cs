using PersonelApplication.Commands;
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
    public class AddGroupViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddGroupViewModel(GroupWrapper group = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
           
            Groups = new GroupWrapper();
        }

      

        public ICommand CloseCommand { get; set; }

        public ICommand ConfirmCommand { get; set; }

        

        private GroupWrapper _group;

        public GroupWrapper Groups
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void Confirm(object obj)
        {
            AddGroup();
            CloseWindow(obj as Window);
        }

        private void AddGroup()
        {
            _repository.AddGroup(Groups);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }


        private void CloseWindow(Window window)
        {
            window.Close();
        }

    }
}
