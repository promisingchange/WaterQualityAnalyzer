using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Messaging;

using MaterialDesignThemes.Wpf;

using CrystalClear.DL;
using CrystalClear.DL.Models;
using CrystalClear.ViewModels;
using CrystalClear.Utils;
using CrystalClear.BL;

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for Analyzer.xaml
    /// </summary>
    public partial class Analyzer : UserControl
    {
        private bool analyzing = false;
        private bool analyzed = false;
        private double OverallIndex = 0.0;
        private int Level = 0;

        public Analyzer()
        {
            InitializeComponent();

            ReservoirComboBox.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Collapsed;
            AnalyzeButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
            NewButton.Visibility = Visibility.Collapsed;
        }

        private void ReservoirAddedHandler(string obj)
        {
            Console.WriteLine("Reservoir Added");

            var viewModel = (AnalyzerViewModel)DataContext;

            if (viewModel.Reservoirs.Count == 0)
            {
                // No reservoir
                // User should add one
                ReservoirComboBox.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Visible;
                AnalyzeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // There are some
                ReservoirComboBox.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Collapsed;
                AnalyzeButton.Visibility = Visibility.Visible;
            }
        }

        private void Analyzer_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Analyzer loaded");

            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var reservoirs = db.GetAllReservoirs();
            var reservoirModels = new ObservableCollection<Reservoir>();
            foreach (var reservoir in reservoirs)
            {
                reservoirModels.Add(reservoir);
            }

            var viewModel = (AnalyzerViewModel)DataContext;
            viewModel.Reservoirs = reservoirModels;

            if (viewModel.Reservoirs.Count == 0)
            {
                // No reservoir
                // User should add one
                ReservoirComboBox.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Visible;
                AnalyzeButton.Visibility = Visibility.Collapsed;

                viewModel.ShowMessageCommand.Execute("Please add a reservoir to start the analysis");
            }
            else
            {
                // There are some
                ReservoirComboBox.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Collapsed;
                AnalyzeButton.Visibility = Visibility.Visible;
            }

            viewModel.Date = DateTime.Today;
            viewModel.Reservoir = null;
            viewModel.Temperature = null;
            viewModel.ID = -1;
            viewModel.PH = "";
            viewModel.DO = "";
            viewModel.BOD5 = "";
            viewModel.COD = "";
            viewModel.NH4N = "";
            viewModel.NO2N = "";
            viewModel.NO3N = "";
            viewModel.SS = "";
            viewModel.CL = "";
            viewModel.CB = "";
            viewModel.OverallIndex = "0.00000";
            viewModel.Level = "0";
            viewModel.Explanation = "";

            ((AnalyzerViewModel)DataContext).ReservoirAdded += ReservoirAddedHandler;
        }

        public void CalendarDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Calendar.SelectedDate = ((AnalyzerViewModel)DataContext).Date;
        }

        public void CalendarDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, "1")) return;

            if (!Calendar.SelectedDate.HasValue)
            {
                eventArgs.Cancel();
                return;
            }

            ((AnalyzerViewModel)DataContext).Date = Calendar.SelectedDate.Value;
        }
        private async void Analyze_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AnalyzerViewModel;

            if (analyzing == true)
            {
                // It's analyzing another data.
                // Please wait.
                viewModel.ShowMessageCommand.Execute("Please wait...");
                return;
            }

            Reservoir selectedReservoir = viewModel.Reservoir;
            if (selectedReservoir == null)
            {
                viewModel.ShowMessageCommand.Execute("Please choose a reservoir");
                return;
            }

            OxygenDOViewModel selectedTemperature = viewModel.Temperature;
            if (selectedTemperature == null)
            {
                viewModel.ShowMessageCommand.Execute("Please choose a temperature range");
                return;
            }

            if (TextPH.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for PH");
                return;
            }
            double PH = double.Parse(TextPH.Text);

            if (TextDO.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for DO");
                return;
            }
            double DO = double.Parse(TextDO.Text);

            if (TextBOD5.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for BOD5");
                return;
            }
            double BOD5 = double.Parse(TextBOD5.Text);

            if (TextCOD.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for COD");
                return;
            }
            double COD = double.Parse(TextCOD.Text);

            if (TextNH4N.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for NH4-N");
                return;
            }
            double NH4N = double.Parse(TextNH4N.Text);

            if (TextNO2N.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for NO2-N");
                return;
            }
            double NO2N = double.Parse(TextNO2N.Text);

            if (TextNO3N.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for NO3-N");
                return;
            }
            double NO3N = double.Parse(TextNO3N.Text);

            if (TextSS.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for SS");
                return;
            }
            double SS = double.Parse(TextSS.Text);

            if (TextCL.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for CL");
                return;
            }
            double CL = double.Parse(TextCL.Text);

            if (TextCB.Text == "")
            {
                viewModel.ShowMessageCommand.Execute("Please input a value for CB");
                return;
            }
            double CB = double.Parse(TextCB.Text);

            // All validations passed
            analyzed = false;
            analyzing = true;
            await Task.Run(() => this.AnalyzeData(selectedTemperature.Temperature, PH, DO, BOD5, COD, NH4N, NO2N, NO3N, SS, CL, CB));
            analyzing = false;
            
            viewModel.OverallIndex = OverallIndex.ToString("F5");
            viewModel.Level = Level.ToString("D");
            viewModel.Explanation = Utility.GetExplanationsForLevels()[Level];
            analyzed = true;

            viewModel.PH = TextPH.Text;
            viewModel.DO = TextDO.Text;
            viewModel.BOD5 = TextBOD5.Text;
            viewModel.COD = TextCOD.Text;
            viewModel.NH4N = TextNH4N.Text;
            viewModel.NO2N = TextNO2N.Text;
            viewModel.NO3N = TextNO3N.Text;
            viewModel.SS = TextSS.Text;
            viewModel.CL = TextCL.Text;
            viewModel.CB = TextCB.Text;

            SaveButton.Visibility = Visibility.Visible;
            NewButton.Visibility = Visibility.Visible;
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var viewModel = DataContext as AnalyzerViewModel;

            var newRecord = new Record();
            newRecord.Temperature = viewModel.Temperature.Temperature;
            newRecord.PH = double.Parse(viewModel.PH);
            newRecord.DO = double.Parse(viewModel.DO);
            newRecord.BOD5 = double.Parse(viewModel.BOD5);
            newRecord.COD = double.Parse(viewModel.COD);
            newRecord.NH4N = double.Parse(viewModel.NH4N);
            newRecord.NO2N = double.Parse(viewModel.NO2N);
            newRecord.NO3N = double.Parse(viewModel.NO3N);
            newRecord.SS = double.Parse(viewModel.SS);
            newRecord.CL = double.Parse(viewModel.CL);
            newRecord.CB = double.Parse(viewModel.CB);
            newRecord.Overall = double.Parse(viewModel.OverallIndex);
            newRecord.Level = int.Parse(viewModel.Level);
            newRecord.ReservoirId = viewModel.Reservoir.Id;
            newRecord.DateTime = viewModel.Date.ToString("yyyy-MM-dd");

            if (viewModel.ID == -1)
            {
                // New Record
                // Save
                int newId = db.AddRecord(newRecord);
                if (newId != -1)
                {
                    var lastRecord = db.GetLastRecord();
                    viewModel.ID = lastRecord.Id;
                    SaveButton.Content = "Update";
                }
                else
                {
                    viewModel.ShowMessageCommand.Execute("Something went wrong. Please try it again.");
                }
            }
            else
            {
                // Existing Record
                // Update
                newRecord.Id = viewModel.ID;
                int rowAffected = db.UpdateRecord(newRecord);
                if (rowAffected != 1)
                {
                    viewModel.ShowMessageCommand.Execute("Something went wrong. Please try it again.");
                }
            }
        }
        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Utility.IsTextAllowed(e.Text);
            if (!e.Handled)
            {
                // Try parse the text to double
                var result = 0.0;
                var extra = e.Text;
                if (e.Text == ".") extra = ".0";
                var text = ((TextBox)sender).Text + extra;
                e.Handled = !double.TryParse(text, out result);
            }
        }
        private async Task AnalyzeData(double Temperature, double Cph, double Cdo, double Cbod5, double Ccod, double Cnh4n, double Cno2n, double Cno3n, double Css, double Ccl, double Ccb)
        {
            Console.WriteLine("----------------------------------------------------------");
            var Is = AnalysisEngine.CalculateSimpleIndices(Temperature, Cph, Cdo, Cbod5, Ccod, Cnh4n, Cno2n, Cno3n, Css, Ccl, Ccb);
            Console.WriteLine(Is.ToJSONString());
            var Ws = AnalysisEngine.GetWeightCoefficients(Temperature, Is);
            Console.WriteLine(Ws.ToJSONString());
            OverallIndex = AnalysisEngine.CalculateOverallIndex(Is, Ws);
            Console.WriteLine("Overall Index: " + OverallIndex.ToString("F5"));
            Level = AnalysisEngine.GetLevel(Temperature, OverallIndex);
            Console.WriteLine("Level: " + Level.ToString("D"));
        }
        private void AddReservoir_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AnalyzerViewModel;
            viewModel.RunExtendedDialogCommand.Execute("");
        }
        private void New_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as AnalyzerViewModel;
            viewModel.ID = -1;
            viewModel.Reservoir = null;
            viewModel.Temperature = null;
            viewModel.PH = "";
            viewModel.DO = "";
            viewModel.BOD5 = "";
            viewModel.COD = "";
            viewModel.NH4N = "";
            viewModel.NO2N = "";
            viewModel.NO3N = "";
            viewModel.SS = "";
            viewModel.CL = "";
            viewModel.CB = "";
            viewModel.OverallIndex = "0.00000";
            viewModel.Level = "0";
            viewModel.Explanation = "";

            TextPH.Text = "";
            TextDO.Text = "";
            TextBOD5.Text = "";
            TextCOD.Text = "";
            TextNH4N.Text = "";
            TextNO2N.Text = "";
            TextNO3N.Text = "";
            TextSS.Text = "";
            TextCL.Text = "";
            TextCB.Text = "";

            analyzed = false;
            SaveButton.Visibility = Visibility.Collapsed;
            SaveButton.Content = "Save";
        }
    }
}
