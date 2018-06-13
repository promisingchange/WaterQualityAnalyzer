﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

using CrystalClear.DL;
using CrystalClear.DL.Models;
using CrystalClear.Domain;

namespace CrystalClear.ViewModels
{
    public class AnalyzerViewModel : INotifyPropertyChanged
    {
        private DateTime _date;
        private DateTime _time;
        private string _validatingTime;
        private DateTime? _futureValidatingDate;
        private ObservableCollection<OxygenDOViewModel> _temperatures;
        private OxygenDOViewModel _temperature;
        private ObservableCollection<Reservoir> _reservoirs;
        private Reservoir _reservoir;

        private double _overallIndex;
        private int _level;
        private string _explanation;

        private int _Id;
        private string _ph;
        private string _do;
        private string _bod5;
        private string _cod;
        private string _nh4n;
        private string _no2n;
        private string _no3n;
        private string _ss;
        private string _cl;
        private string _cb;

        private readonly ISnackbarMessageQueue _snackbarMessageQueue;

        public event Action<String> ReservoirAdded;

        public AnalyzerViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _snackbarMessageQueue = snackbarMessageQueue ?? throw new ArgumentNullException(nameof(snackbarMessageQueue));

            Date = DateTime.Now;
            Time = DateTime.Now;
            OverallIndex = "0";
            Level = "0";
            Explanation = " ";

            TemperatureRange = new ObservableCollection<OxygenDOViewModel>
            {
                new OxygenDOViewModel { Id = 0, TemperatureRange = "0~10°C", Temperature = 10 },
                new OxygenDOViewModel { Id = 1, TemperatureRange = "11~20°C", Temperature = 20 },
                new OxygenDOViewModel { Id = 2, TemperatureRange = "21~30°C", Temperature = 30 }
            };

            /*
            Reservoirs = new ObservableCollection<Reservoir>
            {
                new Reservoir { Id = 0, Name = "Lake Kariba" },
                new Reservoir { Id = 1, Name = "Bratsk Reservoir" },
                new Reservoir { Id = 2, Name = "Lake Volta" },
                new Reservoir { Id = 3, Name = "Manicouagan Reservoir" },
                new Reservoir { Id = 4, Name = "Lake Guri" }
            };
            */

            PH = "";
            DO = "";
            BOD5 = "";
            COD = "";
            NH4N = "";
            NO2N = "";
            NO3N = "";
            SS = "";
            CL = "";
            CB = "";

            ShowMessageCommand = new AnotherCommandImplementation(ShowMessage);
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        public string ValidatingTime
        {
            get { return _validatingTime; }
            set
            {
                _validatingTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime? FutureValidatingDate
        {
            get { return _futureValidatingDate; }
            set
            {
                _futureValidatingDate = value;
                OnPropertyChanged();
            }
        }

        public string OverallIndex
        {
            get { return _overallIndex.ToString("F5"); }
            set
            {
                _overallIndex = double.Parse(value);
                OnPropertyChanged();
            }
        }

        public string Level
        {
            get { return _level.ToString("D"); }
            set
            {
                _level = int.Parse(value);
                OnPropertyChanged();
            }
        }
        public string Explanation
        {
            get { return _explanation; }
            set
            {
                _explanation = value;
                OnPropertyChanged();
            }
        }

        public OxygenDOViewModel Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<OxygenDOViewModel> TemperatureRange
        {
            get { return _temperatures; }
            set
            {
                _temperatures = value;
                OnPropertyChanged();
            }
        }

        public Reservoir Reservoir
        {
            get { return _reservoir; }
            set
            {
                _reservoir = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Reservoir> Reservoirs
        {
            get { return _reservoirs; }
            set
            {
                _reservoirs = value;
                OnPropertyChanged();
            }
        }
        public int ID
        {
            get { return _Id; }
            set
            {
                _Id = value;
            }
        }
        public string PH
        {
            get { return _ph; }
            set
            {
                _ph = value;
                OnPropertyChanged();
            }
        }
        public string DO
        {
            get { return _do; }
            set
            {
                _do = value;
                OnPropertyChanged();
            }
        }
        public string BOD5
        {
            get { return _bod5; }
            set
            {
                _bod5 = value;
                OnPropertyChanged();
            }
        }
        public string COD
        {
            get { return _cod; }
            set
            {
                _cod = value;
                OnPropertyChanged();
            }
        }
        public string NH4N
        {
            get { return _nh4n; }
            set
            {
                _nh4n = value;
                OnPropertyChanged();
            }
        }
        public string NO2N
        {
            get { return _no2n; }
            set
            {
                _no2n = value;
                OnPropertyChanged();
            }
        }
        public string NO3N
        {
            get { return _no3n; }
            set
            {
                _no3n = value;
                OnPropertyChanged();
            }
        }
        public string SS
        {
            get { return _ss; }
            set
            {
                _ss = value;
                OnPropertyChanged();
            }
        }
        public string CL
        {
            get { return _cl; }
            set
            {
                _cl = value;
                OnPropertyChanged();
            }
        }
        public string CB
        {
            get { return _cb; }
            set
            {
                _cb = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ShowMessageCommand { get; }

        private void ShowMessage(object obj)
        {
            var message = obj as string;
            _snackbarMessageQueue.Enqueue(message);
        }
        public ICommand RunExtendedDialogCommand => new AnotherCommandImplementation(ExecuteRunExtendedDialog);
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
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
            {
                return;
            }

            ReservoirAddDialog dialog = eventArgs.Session.Content as ReservoirAddDialog;
            var newName = dialog.TextReservoirName.Text;
            if (!string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("New Reservoir Name: " + newName);

                var db = CrystalClearDB.GetInstance();
                if (db == null) return;

                int newId = db.AddReservoir(newName);
                if (newId != -1)
                {
                    var reservoirs = db.GetAllReservoirs();
                    var newReservoirModels = new ObservableCollection<Reservoir>();
                    foreach (var reservoir in reservoirs)
                    {
                        newReservoirModels.Add(reservoir);
                    }

                    Reservoirs = newReservoirModels;

                    ReservoirAdded("");
                }
            }
        }
    }
}
