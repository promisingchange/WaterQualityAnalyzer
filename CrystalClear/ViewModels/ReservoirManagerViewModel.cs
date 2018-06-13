using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

using CrystalClear.Domain;
using CrystalClear.DL;
using CrystalClear.DL.Models;

namespace CrystalClear.ViewModels
{
    public class ReservoirManagerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ReservoirViewModel> _reservoirs;
        private bool? _isAllItemsSelected;
        private ReservoirViewModel _selectedReservoir;
        private List<ReservoirViewModel> _selectedReservoirs;

        private readonly ISnackbarMessageQueue _snackbarMessageQueue;

        public ReservoirManagerViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _snackbarMessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));

            _reservoirs = new ObservableCollection<ReservoirViewModel>();
            //_reservoirs = CreateData();
        }
        public bool? IsAllItemsSelected
        {
            get { return _isAllItemsSelected; }
            set
            {
                if (_isAllItemsSelected == value) return;

                _isAllItemsSelected = value;

                OnPropertyChanged();
            }
        }
        private static ObservableCollection<ReservoirViewModel> CreateData()
        {
            return new ObservableCollection<ReservoirViewModel>
            {
                new ReservoirViewModel
                {
                    Id = 0,
                    Name = "Lake Kariba",
                },
                new ReservoirViewModel
                {
                    Id = 1,
                    Name = "Bratsk Reservoir",
                },
                new ReservoirViewModel
                {
                    Id = 2,
                    Name = "Lake Volta",
                },
                new ReservoirViewModel
                {
                    Id = 3,
                    Name = "Manicouagan Reservoir",
                },
                new ReservoirViewModel
                {
                    Id = 4,
                    Name = "Lake Guri",
                }
            };
        }

        public ObservableCollection<ReservoirViewModel> Reservoirs
        {
            get { return _reservoirs; }
            set
            {
                _reservoirs = value;
                OnPropertyChanged();
            }
        }
        public ReservoirViewModel SelectedReservoir
        {
            get { return _selectedReservoir; }
            set
            {
                _selectedReservoir = value;
            }
        }
        public List<ReservoirViewModel> SelectedReservoirs
        {
            get { return _selectedReservoirs; }
            set
            {
                _selectedReservoirs = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RunExtendedDialogCommand => new AnotherCommandImplementation(ExecuteRunExtendedDialog);
        public ICommand DeleteReservoirsCommand => new AnotherCommandImplementation(DeleteReservoirs);

        private async void ExecuteRunExtendedDialog(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new ReservoirAddDialog
            {
                DataContext = new ReservoirAddDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

            if (this.SelectedReservoir != null)
            {
                var dialog = eventargs.Session.Content as ReservoirAddDialog;
                dialog.TextReservoirName.Text = SelectedReservoir.Name;
            }
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
            {
                this.SelectedReservoir = null;
                return;
            }

            ReservoirAddDialog dialog = eventArgs.Session.Content as ReservoirAddDialog;
            var newName = dialog.TextReservoirName.Text;
            if (!string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New Reservoir Name: " + newName);
                if (this.SelectedReservoir != null)
                {
                    this.SelectedReservoir.Name = newName;

                    var db = CrystalClearDB.GetInstance();
                    if (db == null) return;

                    int rowAffected = db.UpdateReservoir(new Reservoir { Id = SelectedReservoir.Id, Name = SelectedReservoir.Name });
                    if (rowAffected == 1)
                    {
                        var reservoirs = db.GetAllReservoirs();
                        var newReservoirModels = new ObservableCollection<ReservoirViewModel>();
                        foreach (var reservoir in reservoirs)
                        {
                            newReservoirModels.Add(new ReservoirViewModel(reservoir));
                        }

                        Reservoirs = newReservoirModels;
                        this.SelectedReservoir = null;
                    }
                }
                else
                {
                    var db = CrystalClearDB.GetInstance();
                    if (db == null) return;

                    int newId = db.AddReservoir(newName);
                    if (newId != -1)
                    {
                        var newReservoir = db.GetLastReservoir();
                        Reservoirs.Add(new ReservoirViewModel(newReservoir));
                    }
                }
            }
        }
        private void DeleteReservoirs(object o)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            if (this.SelectedReservoirs != null && this.SelectedReservoirs.Count > 0)
            {
                foreach (var model in this.SelectedReservoirs)
                {
                    db.DeleteReservoir(new Reservoir { Id = model.Id, Name = model.Name });
                    var index = Reservoirs.IndexOf(model);
                    Reservoirs.RemoveAt(index);
                }

                this.SelectedReservoirs = null;
            }
        }
    }
}
