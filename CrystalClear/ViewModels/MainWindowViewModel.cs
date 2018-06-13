using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;

using CrystalClear.Domain;

namespace CrystalClear.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue, ListBox MenuBox)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            MenuItems = new[]
            {
                new Domain.MenuItem("Home", new Home { DataContext = new HomeViewModel(MenuBox) }),
                new Domain.MenuItem("Analyzer", new Analyzer { DataContext = new AnalyzerViewModel(snackbarMessageQueue) }),
                new Domain.MenuItem("Record Manager", new RecordManager { DataContext = new RecordManagerViewModel(snackbarMessageQueue) }),
                new Domain.MenuItem("Reservoir Manager", new ReservoirManager { DataContext = new ReservoirManagerViewModel(snackbarMessageQueue) }),
                new Domain.MenuItem("Helper", new Helper { DataContext = new HelperViewModel() }),
            };
        }

        public Domain.MenuItem[] MenuItems { get; }
    }
}
