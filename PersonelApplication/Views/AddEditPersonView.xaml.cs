using MahApps.Metro.Controls;
using PersonelApplication.Models;
using PersonelApplication.Models.Wrappers;
using PersonelApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PersonelApplication.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddEditPerson.xaml
    /// </summary>
    public partial class AddEditPersonView : MetroWindow
    {
       // private PersonWrapper personWrapper;

        public AddEditPersonView(PersonWrapper person = null)
        {
            InitializeComponent();
            DataContext = new AddEditPersonViewModel(person);
        }

       
    }
}
