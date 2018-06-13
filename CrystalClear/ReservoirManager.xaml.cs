using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using MaterialDesignThemes.Wpf;

using CrystalClear.ViewModels;
using CrystalClear.DL;
using CrystalClear.DL.Models;

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for ReservoirManager.xaml
    /// </summary>
    public partial class ReservoirManager : UserControl
    {
        public ReservoirManager()
        {
            InitializeComponent();

            AddButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }
        private void ReservoirManager_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Reservoir Manager loaded");

            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var reservoirs = db.GetAllReservoirs();
            var reservoirModels = new ObservableCollection<ReservoirViewModel>();
            foreach (var reservoir in reservoirs)
            {
                var reservoirModel = new ReservoirViewModel(reservoir);
                reservoirModels.Add(reservoirModel);
            }

            var viewModel = (ReservoirManagerViewModel)DataContext;
            viewModel.Reservoirs = reservoirModels;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = ((ListView)sender).SelectedItems;
            if (selectedItems.Count == 1)
            {
                // Only one selected
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
            else if (selectedItems.Count == 0)
            {
                // No one selected
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Multiple ones selected
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Visible;
            }
        }
        private void ReservoirAddDialog_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, true)) return;

            ResizeListViewColumn();
        }
        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = ReservoirListView.SelectedItems;
            if (selectedItems.Count > 0)
            {
                var selectedItem = selectedItems[0];
                var viewModel = (ReservoirManagerViewModel)DataContext;
                viewModel.SelectedReservoir = selectedItem as ReservoirViewModel;
                viewModel.RunExtendedDialogCommand.Execute("");

                ResizeListViewColumn();
            }            
        }
        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = ReservoirListView.SelectedItems;
            if (selectedItems.Count > 0)
            {
                var viewModel = (ReservoirManagerViewModel)DataContext;
                viewModel.SelectedReservoirs = selectedItems.Cast<ReservoirViewModel>().ToList();
                viewModel.DeleteReservoirsCommand.Execute("");

                ResizeListViewColumn();
            }
        }

        private void ReservoirListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                GridView view = this.ReservoirListView.View as GridView;
                Decorator border = VisualTreeHelper.GetChild(this.ReservoirListView, 0) as Decorator;
                if (border != null)
                {
                    ScrollViewer scroller = border.Child as ScrollViewer;
                    if (scroller != null)
                    {
                        ItemsPresenter presenter = scroller.Content as ItemsPresenter;
                        if (presenter != null)
                        {
                            view.Columns[0].Width = presenter.ActualWidth;
                            for (int i = 1; i < view.Columns.Count; i++)
                            {
                                view.Columns[0].Width -= view.Columns[i].ActualWidth;
                            }
                        }
                    }
                }
            }
        }
        private void ResizeListViewColumn()
        {
            var view = this.ReservoirListView.View as GridView;
            foreach (GridViewColumn column in view.Columns)
            {
                // Setting NaN for the column width automatically determines the required
                // width enough to hold the content completely.

                // If the width is NaN, first set it to ActualWidth temporarily.
                if (double.IsNaN(column.Width))
                    column.Width = column.ActualWidth;

                // Finally, set the column with to NaN. This raises the property change
                // event and re computes the width.
                column.Width = double.NaN;
            }
        }
    }
}
