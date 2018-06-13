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
using System.Windows.Navigation;
using System.Windows.Shapes;

using CrystalClear.ViewModels;

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        private void Analyzer_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomeViewModel)DataContext;
            viewModel.ShowAnalyzerCommand.Execute("1");
        }
        private void Records_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomeViewModel)DataContext;
            viewModel.ShowRecordsCommand.Execute("2");
        }
        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomeViewModel)DataContext;
            viewModel.ShowHelpCommand.Execute("4");
        }
        private void Reservoirs_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomeViewModel)DataContext;
            viewModel.ShowReservoirsCommand.Execute("3");
        }
    }
}
