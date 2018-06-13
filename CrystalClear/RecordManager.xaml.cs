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
using CrystalClear.BL;
using CrystalClear.Utils;

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for RecordManager.xaml
    /// </summary>
    public partial class RecordManager : UserControl
    {
        private bool analyzing = false;
        private bool analyzed = false;
        private double OverallIndex = 0.0;
        private int Level = 0;

        public RecordManager()
        {
            InitializeComponent();
        }

        private void RecordManager_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("RecordManager loaded");

            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var viewModel = (RecordManagerViewModel)DataContext;

            var allRecords = db.GetAllRecords();
            var recordModels = new ObservableCollection<RecordViewModel>();
            foreach(var record in allRecords)
            {
                recordModels.Add(new RecordViewModel(record));
            }

            viewModel.Records = recordModels;

            var reservoirs = db.GetAllReservoirs();

            var reservoirModels = new ObservableCollection<Reservoir>(reservoirs);

            viewModel.Reservoirs = reservoirModels;
            viewModel.Reservoir = null;

            var reservoirAll = new Reservoir { Id = -1, Name = "All", Deleted = false };
            reservoirs.Insert(0, reservoirAll);

            viewModel.ReservoirsForView = new ObservableCollection<Reservoir>(reservoirs);
            viewModel.ReservoirForView = reservoirAll;

            viewModel.ViewMethod = viewModel.ViewMethods[0];

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

            AnalyzerPanel.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;

        }

        private void ReservoirComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = ReservoirComboBox.SelectedItem as Reservoir;
            if (SelectedItem == null) return;

            if (SelectedItem.Id == -1)
            {
                // All
                var db = CrystalClearDB.GetInstance();
                if (db == null) return;

                var viewModel = (RecordManagerViewModel)DataContext;

                var allRecords = db.GetAllRecords();
                var recordModels = new ObservableCollection<RecordViewModel>();
                foreach (var record in allRecords)
                {
                    recordModels.Add(new RecordViewModel(record));
                }

                viewModel.Records = recordModels;
                viewModel.BackupRecords = recordModels.ToList<RecordViewModel>();
            }
            else
            {
                // by Reservoir
                var db = CrystalClearDB.GetInstance();
                if (db == null) return;

                var viewModel = (RecordManagerViewModel)DataContext;

                var allRecords = db.GetRecordsForReservoir(SelectedItem.Id);
                var recordModels = new ObservableCollection<RecordViewModel>();
                foreach (var record in allRecords)
                {
                    recordModels.Add(new RecordViewModel(record));
                }

                viewModel.Records = recordModels;
                viewModel.BackupRecords = recordModels.ToList<RecordViewModel>();
            }
        }
        private void ViewMethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedItem = ViewMethodComboBox.SelectedItem as ViewMethodViewModel;
            if (SelectedItem == null) return;

            if (SelectedItem.Id == 0)
            {
                // Daily view, switched from Monthly view
                // Use backup records
                var viewModel = (RecordManagerViewModel)DataContext;

                var records = viewModel.BackupRecords;
                viewModel.Records = new ObservableCollection<RecordViewModel>(records);

                DeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                // Monthly view
                // Hide the analysis panel
                AnalyzerPanel.Visibility = Visibility.Collapsed;

                // Process current data to switch view
                var viewModel = (RecordManagerViewModel)DataContext;
                var records = viewModel.BackupRecords;

                // Group records, based on months
                var groupedData = new Dictionary<string, System.Collections.Generic.List<RecordViewModel>>();
                foreach(var record in records)
                {
                    var dateTime = DateTime.Parse(record.DateTime);
                    var monthTime = dateTime.ToString("yyyy-MM");
                    var newKey = record.ReservoirId.ToString() + "/" + monthTime;
                    if (groupedData.ContainsKey(newKey))
                    {
                        // Existing
                        // Add the record
                        var data = groupedData[newKey];
                        data.Add(record);
                        groupedData[newKey] = data;
                    }
                    else
                    {
                        // New
                        var data = new List<RecordViewModel>();
                        data.Add(record);
                        groupedData[newKey] = data;
                    }
                }

                // Process grouped data
                var processedData = new ObservableCollection<RecordViewModel>();
                var keys = groupedData.Keys;
                foreach(var key in keys)
                {
                    var monthData = groupedData[key];
                    if (monthData.Count == 1)
                    {
                        // Only single record for a month
                        // Add it directly. Just change DateTime.
                        // No need to do the analysis again
                        var keyParts = key.Split('/');
                        var monthRecord = new RecordViewModel
                        {
                            DateTime = keyParts[1],
                            Id = -1,
                            ReservoirId = monthData[0].ReservoirId,
                            ReservoirName = monthData[0].ReservoirName,
                            Temperature = monthData[0].Temperature,
                            TemperatureRange = monthData[0].TemperatureRange,
                            PH = monthData[0].PH,
                            DO = monthData[0].DO,
                            BOD5 = monthData[0].BOD5,
                            COD = monthData[0].COD,
                            NH4N = monthData[0].NH4N,
                            NO2N = monthData[0].NO2N,
                            NO3N = monthData[0].NO3N,
                            SS = monthData[0].SS,
                            CL = monthData[0].CL,
                            CB = monthData[0].CB,
                            Overall = monthData[0].Overall,
                            Level = monthData[0].Level,
                        };
                        processedData.Add(monthRecord);
                    }
                    else
                    {
                        var keyParts = key.Split('/');
                        string DateTime = keyParts[1];
                        int ReservoirId = monthData[0].ReservoirId;
                        string ReservoirName = monthData[0].ReservoirName;
                        double Temperature = monthData[0].Temperature;
                        double PH = double.Parse(monthData[0].PH);
                        double DO = double.Parse(monthData[0].DO);
                        double BOD5 = double.Parse(monthData[0].BOD5);
                        double COD = double.Parse(monthData[0].COD);
                        double NH4N = double.Parse(monthData[0].NH4N);
                        double NO2N = double.Parse(monthData[0].NO2N);
                        double NO3N = double.Parse(monthData[0].NO3N);
                        double SS = double.Parse(monthData[0].SS);
                        double CL = double.Parse(monthData[0].CL);
                        double CB = double.Parse(monthData[0].CB);
                        
                        for (int i=1;i<monthData.Count;i++)
                        {
                            if (monthData[i].Temperature == Temperature)
                            {
                                // Records in the same month should have the same temperature range.
                                PH += double.Parse(monthData[i].PH);
                                DO += double.Parse(monthData[i].DO);
                                BOD5 += double.Parse(monthData[i].BOD5);
                                COD += double.Parse(monthData[i].COD);
                                NH4N += double.Parse(monthData[i].NH4N);
                                NO2N += double.Parse(monthData[i].NO2N);
                                NO3N += double.Parse(monthData[i].NO3N);
                                SS += double.Parse(monthData[i].SS);
                                CL += double.Parse(monthData[i].CL);
                                CB += double.Parse(monthData[i].CB);
                            }
                            else
                            {
                                // Temperature range is different.
                                // Can't analyze the data.
                                viewModel.ShowMessageCommand.Execute("There must be some error in the data. Please inspect it and try again.");
                                return;
                            }
                        }

                        PH = PH / monthData.Count;
                        DO = DO / monthData.Count;
                        BOD5 = BOD5 / monthData.Count;
                        COD = COD / monthData.Count;
                        NH4N = NH4N / monthData.Count;
                        NO2N = NO2N / monthData.Count;
                        NO3N = NO3N / monthData.Count;
                        SS = SS / monthData.Count;
                        CL = CL / monthData.Count;
                        CB = CB / monthData.Count;

                        var Is = AnalysisEngine.CalculateSimpleIndices(Temperature, PH, DO, BOD5, COD, NH4N, NO2N, NO3N, SS, CL, CB);
                        var Ws = AnalysisEngine.GetWeightCoefficients(Temperature, Is);
                        double OverallIndex = AnalysisEngine.CalculateOverallIndex(Is, Ws);
                        int Level = AnalysisEngine.GetLevel(Temperature, OverallIndex);

                        var monthRecord = new RecordExtended
                        {
                            Id = -1,
                            Temperature = Temperature,
                            PH = PH,
                            DO = DO,
                            BOD5 = BOD5,
                            COD = COD,
                            NH4N = NH4N,
                            NO2N = NO2N,
                            NO3N = NO3N,
                            SS = SS,
                            CL = CL,
                            CB = CB,
                            Overall = OverallIndex,
                            Level = Level,
                            ReservoirId = ReservoirId,
                            ReservoirName = ReservoirName,
                            DateTime = DateTime
                        };
                        processedData.Add(new RecordViewModel(monthRecord));
                    }
                }

                viewModel.Records = processedData;

                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }
        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (RecordManagerViewModel)DataContext;

            var SelectedItem = RecordsListBox.SelectedItem as RecordViewModel;
            if (SelectedItem != null)
            {
                var db = CrystalClearDB.GetInstance();
                if (db == null)
                {
                    viewModel.ShowMessageCommand.Execute("Something went wrong. Please try it again.");
                    return;
                }

                var record = SelectedItem.ToRecord();
                int rowAffected = db.DeleteRecord(record);
                if (rowAffected == 1)
                {
                    viewModel.Records.Remove(SelectedItem);
                }
                else
                {
                    viewModel.ShowMessageCommand.Execute("Something went wrong. Please try it again.");
                }
            }
            else
            {
                viewModel.ShowMessageCommand.Execute("Please select one in order to delete.");
            }
        }
        public void CalendarDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Calendar.SelectedDate = ((RecordManagerViewModel)DataContext).Date;
        }

        public void CalendarDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (!Equals(eventArgs.Parameter, "1")) return;

            if (!Calendar.SelectedDate.HasValue)
            {
                eventArgs.Cancel();
                return;
            }

            ((RecordManagerViewModel)DataContext).Date = Calendar.SelectedDate.Value;
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
        private async void Analyze_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as RecordManagerViewModel;

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
        }
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var viewModel = DataContext as RecordManagerViewModel;

            if (viewModel.ID == -1)
            {
                // Shouldn't reach here.
                viewModel.ShowMessageCommand.Execute("Something odd happened. Please try it again.");
                return;
            }

            var updatedRecord = new Record();
            updatedRecord.Id = viewModel.ID;
            updatedRecord.Temperature = viewModel.Temperature.Temperature;
            updatedRecord.PH = double.Parse(viewModel.PH);
            updatedRecord.DO = double.Parse(viewModel.DO);
            updatedRecord.BOD5 = double.Parse(viewModel.BOD5);
            updatedRecord.COD = double.Parse(viewModel.COD);
            updatedRecord.NH4N = double.Parse(viewModel.NH4N);
            updatedRecord.NO2N = double.Parse(viewModel.NO2N);
            updatedRecord.NO3N = double.Parse(viewModel.NO3N);
            updatedRecord.SS = double.Parse(viewModel.SS);
            updatedRecord.CL = double.Parse(viewModel.CL);
            updatedRecord.CB = double.Parse(viewModel.CB);
            updatedRecord.Overall = double.Parse(viewModel.OverallIndex);
            updatedRecord.Level = int.Parse(viewModel.Level);
            updatedRecord.ReservoirId = viewModel.Reservoir.Id;
            updatedRecord.DateTime = viewModel.Date.ToString("yyyy-MM-dd");

            int rowAffected = db.UpdateRecord(updatedRecord);
            if (rowAffected != 1)
            {
                viewModel.ShowMessageCommand.Execute("Something went wrong. Please try it again.");
            }
            else
            {
                viewModel.ShowMessageCommand.Execute("It's been updated successfully.");

                // Refresh Data
                viewModel.ID = -1;
                AnalyzerPanel.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;

                ReservoirComboBox_SelectionChanged(null, null);
            }
        }

        private void RecordsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as RecordManagerViewModel;
            if (viewModel.ViewMethod.Id == 1)
            {
                // It's in a monthly view.
                // Can't show detail panel.
                viewModel.ID = -1;
                AnalyzerPanel.Visibility = Visibility.Collapsed;
                return;
            }

            var SelectedItem = RecordsListBox.SelectedItem as RecordViewModel;
            if (SelectedItem != null)
            {
                AnalyzerPanel.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Collapsed;

                viewModel.Record = SelectedItem;

                viewModel.ID = SelectedItem.Id;
                viewModel.PH = SelectedItem.PH;
                viewModel.DO = SelectedItem.DO;
                viewModel.BOD5 = SelectedItem.BOD5;
                viewModel.COD = SelectedItem.COD;
                viewModel.NH4N = SelectedItem.NH4N;
                viewModel.NO2N = SelectedItem.NO2N;
                viewModel.NO3N = SelectedItem.NO3N;
                viewModel.SS = SelectedItem.SS;
                viewModel.CL = SelectedItem.CL;
                viewModel.CB = SelectedItem.CB;
                viewModel.OverallIndex = SelectedItem.Overall;
                viewModel.Level = SelectedItem.Level;
                viewModel.Date = DateTime.Parse(SelectedItem.DateTime);

                Reservoir selectedReservoir = null;
                for (int i=0; i<viewModel.Reservoirs.Count; i++)
                {
                    if (viewModel.Reservoirs[i].Id == SelectedItem.ReservoirId)
                    {
                        selectedReservoir = viewModel.Reservoirs[i];
                        break;
                    }
                }
                viewModel.Reservoir = selectedReservoir;

                OxygenDOViewModel selectedTemperatureRange = null;
                for (int i=0; i<viewModel.TemperatureRange.Count; i++)
                {
                    if (viewModel.TemperatureRange[i].Temperature == SelectedItem.Temperature)
                    {
                        selectedTemperatureRange = viewModel.TemperatureRange[i];
                        break;
                    }
                }
                viewModel.Temperature = selectedTemperatureRange;
                viewModel.Explanation = Utility.GetExplanationsForLevels()[int.Parse(SelectedItem.Level)];
            }
            else
            {
                viewModel.ID = -1;
                AnalyzerPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
