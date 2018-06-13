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

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for Helper.xaml
    /// </summary>
    public partial class Helper : UserControl
    {
        public Helper()
        {
            InitializeComponent();
        }

        private void Helper_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Helper loaded");

            var db = CrystalClearDB.GetInstance();
            if (db == null) return;

            var viewModel = (HelperViewModel)DataContext;

            var standards = db.GetEnvironmentalStandards();
            var environmentalStandards = new ObservableCollection<EnvironmentalStandardViewModel>();
            foreach (var standard in standards)
            {
                environmentalStandards.Add(new EnvironmentalStandardViewModel
                {
                    Level = standard.Level.ToString("D"),
                    PH = standard.PH1.ToString("F") + "~" + standard.PH2.ToString("F"),
                    DO = standard.DO.ToString("F"),
                    BOD5 = standard.BOD5.ToString("F"),
                    COD = standard.COD.ToString("F"),
                    NH4N = standard.NH4N.ToString("F"),
                    NO2N = standard.NO2N.ToString("F"),
                    NO3N = standard.NO3N.ToString("F"),
                    SS = standard.SS.ToString("F"),
                    CL = standard.CL.ToString("F"),
                    CB = standard.CB.ToString("F")
                });
            }

            viewModel.EnvironmentalStandards = environmentalStandards;

            var solubilities = db.GetOxygenSolubilities();
            var dos = db.GetOxygenDOs();

            var oxygenSolubilities = new ObservableCollection<OxygenSolubilityViewModel>();
            for (int i=0;i<10;i++)
            {
                var solubility1 = solubilities[i];
                var solubility2 = solubilities[i + 11];
                var solubility3 = solubilities[i + 21];
                oxygenSolubilities.Add(new OxygenSolubilityViewModel {
                    Temperature1 = solubility1.Temperature.ToString("F0"),
                    DO1 = solubility1.DO.ToString("F2"),
                    Temperature2 = solubility2.Temperature.ToString("F0"),
                    DO2 = solubility2.DO.ToString("F2"),
                    Temperature3 = solubility3.Temperature.ToString("F0"),
                    DO3 = solubility3.DO.ToString("F2")
                });
            }
            var solubility = solubilities[10];
            oxygenSolubilities.Add(new OxygenSolubilityViewModel
            {
                Temperature1 = solubility.Temperature.ToString("F0"),
                DO1 = solubility.DO.ToString("F2"),
                Temperature2 = "",
                DO2 = "",
                Temperature3 = "",
                DO3 = ""
            });
            oxygenSolubilities.Add(new OxygenSolubilityViewModel
            {
                Temperature1 = "0~10°C",
                DO1 = "14.000",
                Temperature2 = "11~20°C",
                DO2 = "10.000",
                Temperature3 = "21~30°C",
                DO3 = "8.292"
            });

            viewModel.OxygenSolubilities = oxygenSolubilities;

            var individualIndicesFor10Celsius = db.GetIndividualIndicesFor10Celsius();
            var iIfor10 = new ObservableCollection<BaseIndex>(individualIndicesFor10Celsius);
            viewModel.IndividualIndicesFor10Celsius = iIfor10;

            var individualIndicesFor20Celsius = db.GetIndividualIndicesFor20Celsius();
            var iIfor20 = new ObservableCollection<BaseIndex>(individualIndicesFor20Celsius);
            viewModel.IndividualIndicesFor20Celsius = iIfor20;

            var individualIndicesFor30Celsius = db.GetIndividualIndicesFor30Celsius();
            var iIfor30 = new ObservableCollection<BaseIndex>(individualIndicesFor30Celsius);
            viewModel.IndividualIndicesFor30Celsius = iIfor30;

            var weightCoefficientsFor10Celsius = db.GetWeightCoefficientsFor10Celsius();
            var wCfor10 = new ObservableCollection<BaseIndex>(weightCoefficientsFor10Celsius);
            viewModel.WeightCoefficientsFor10Celsius = wCfor10;

            var weightCoefficientsFor20Celsius = db.GetWeightCoefficientsFor20Celsius();
            var wCfor20 = new ObservableCollection<BaseIndex>(weightCoefficientsFor20Celsius);
            viewModel.WeightCoefficientsFor20Celsius = wCfor20;

            var weightCoefficientsFor30Celsius = db.GetWeightCoefficientsFor30Celsius();
            var wCfor30 = new ObservableCollection<BaseIndex>(weightCoefficientsFor30Celsius);
            viewModel.WeightCoefficientsFor30Celsius = wCfor30;

            var weightIndicesFor10Celsius = db.GetWeightIndicesFor10Celsius();
            var wIfor10 = new ObservableCollection<BaseIndex>(weightIndicesFor10Celsius);
            viewModel.WeightIndicesFor10Celsius = wIfor10;

            var weightIndicesFor20Celsius = db.GetWeightIndicesFor20Celsius();
            var wIfor20 = new ObservableCollection<BaseIndex>(weightIndicesFor20Celsius);
            viewModel.WeightIndicesFor20Celsius = wIfor20;

            var weightIndicesFor30Celsius = db.GetWeightIndicesFor30Celsius();
            var wIfor30 = new ObservableCollection<BaseIndex>(weightIndicesFor30Celsius);
            viewModel.WeightIndicesFor30Celsius = wIfor30;
        }
    }
}
