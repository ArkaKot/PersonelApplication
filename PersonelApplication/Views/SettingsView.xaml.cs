﻿using MahApps.Metro.Controls;
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
    /// Logika interakcji dla klasy SettingsView.xaml
    /// </summary>
    public partial class SettingsView : MetroWindow
    {
        public SettingsView(bool CanCloseWindow)
        {
            InitializeComponent();
            DataContext = new SettingsViewModel(CanCloseWindow);
        }
    }
}
