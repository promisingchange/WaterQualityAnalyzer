using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using CrystalClear.DL;
using CrystalClear.BL;

namespace CrystalClear
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the database
            CrystalClearDB.InitializeDatabase();

            //Task.Run(() => this.TestEngine());
        }

        private async Task TestEngine()
        {
            // January
            Console.WriteLine("----------------------January----------------------");
            double Temperature = 10.0, PH = 7.1, DO = 13.86, BOD5 = 3.42, COD = 2.68, NH4N = 0.27, NO2N = 0.03, NO3N = 6.7, SS = 24.67, CL = 15.33, CB = 235;
            var Is = AnalysisEngine.CalculateSimpleIndices(Temperature, PH, DO, BOD5, COD, NH4N, NO2N, NO3N, SS, CL, CB);
            Console.WriteLine(Is.ToJSONString());
            var Ws = AnalysisEngine.GetWeightCoefficients(Temperature, Is);
            Console.WriteLine(Ws.ToJSONString());
            var OverallIndex = AnalysisEngine.CalculateOverallIndex(Is, Ws);
            Console.WriteLine("Overall Index: " + OverallIndex.ToString("F5"));
            var Level = AnalysisEngine.GetLevel(Temperature, OverallIndex);
            Console.WriteLine("Level: " + Level.ToString("D"));

            // May
            Console.WriteLine("----------------------May----------------------");
            Temperature = 20.0; PH = 7.27; DO = 9.95; BOD5 = 2.41; COD = 2.18; NH4N = 0.35; NO2N = 0.02; NO3N = 07.9; SS = 21.33; CL = 24.67; CB = 796.67;
            Is = AnalysisEngine.CalculateSimpleIndices(Temperature, PH, DO, BOD5, COD, NH4N, NO2N, NO3N, SS, CL, CB);
            Console.WriteLine(Is.ToJSONString());
            Ws = AnalysisEngine.GetWeightCoefficients(Temperature, Is);
            Console.WriteLine(Ws.ToJSONString());
            OverallIndex = AnalysisEngine.CalculateOverallIndex(Is, Ws);
            Console.WriteLine("Overall Index: " + OverallIndex.ToString("F5"));
            Level = AnalysisEngine.GetLevel(Temperature, OverallIndex);
            Console.WriteLine("Level: " + Level.ToString("D"));

            // July
            Console.WriteLine("----------------------July----------------------");
            Temperature = 30.0; PH = 7.23; DO = 8.69; BOD5 = 2.37; COD = 2.4; NH4N = 0.28; NO2N = 0.04; NO3N = 9; SS = 27.33; CL = 23.33; CB = 773.33;
            Is = AnalysisEngine.CalculateSimpleIndices(Temperature, PH, DO, BOD5, COD, NH4N, NO2N, NO3N, SS, CL, CB);
            Console.WriteLine(Is.ToJSONString());
            Ws = AnalysisEngine.GetWeightCoefficients(Temperature, Is);
            Console.WriteLine(Ws.ToJSONString());
            OverallIndex = AnalysisEngine.CalculateOverallIndex(Is, Ws);
            Console.WriteLine("Overall Index: " + OverallIndex.ToString("F5"));
            Level = AnalysisEngine.GetLevel(Temperature, OverallIndex);
            Console.WriteLine("Level: " + Level.ToString("D"));



        }
    }
}
