using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

using MaterialDesignThemes.Wpf;

using CrystalClear.DL.Models;
using CrystalClear.Domain;

namespace CrystalClear.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly ListBox MainMenuBox;
        public HomeViewModel(ListBox MenuBox)
        {
            this.MainMenuBox = MenuBox;

            ShowAnalyzerCommand = new AnotherCommandImplementation(ShowView);
            ShowRecordsCommand = new AnotherCommandImplementation(ShowView);
            ShowReservoirsCommand = new AnotherCommandImplementation(ShowView);
            ShowHelpCommand = new AnotherCommandImplementation(ShowView);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand ShowAnalyzerCommand { get; }
        public ICommand ShowRecordsCommand { get; }
        public ICommand ShowHelpCommand { get; }
        public ICommand ShowReservoirsCommand { get; }

        private void ShowView(object obj)
        {
            var viewIndex = obj as string;
            int index = int.Parse(viewIndex);
            MainMenuBox.SelectedIndex = index;
        }
    }
}
