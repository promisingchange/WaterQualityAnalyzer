using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalClear.BL.Entities
{
    public class WeightCoefficients
    {
        public double PH { get; set; }
        public double DO { get; set; }
        public double BOD5 { get; set; }
        public double COD { get; set; }
        public double NH4N { get; set; }
        public double NO2N { get; set; }
        public double NO3N { get; set; }
        public double SS { get; set; }
        public double CL { get; set; }
        public double CB { get; set; }
        public String ToJSONString()
        {
            var phStr = "PH: " + PH.ToString("F5");
            var doStr = "DO: " + DO.ToString("F5");
            var bod5Str = "BOD5: " + BOD5.ToString("F5");
            var codStr = "COD: " + COD.ToString("F5");
            var nh4nStr = "NH4-N: " + NH4N.ToString("F5");
            var no2nStr = "NO2-N: " + NO2N.ToString("F5");
            var no3nStr = "NO3-N: " + NO3N.ToString("F5");
            var ssStr = "SS: " + SS.ToString("F5");
            var clStr = "CL: " + CL.ToString("F5");
            var cbStr = "CB: " + CB.ToString("F5");

            return phStr + ", " + doStr + ", " + bod5Str + ", " + codStr + ", " + nh4nStr + ", " + no2nStr + ", " + no3nStr + ", " + ssStr + ", " + clStr + ", " + cbStr;
        }
    }
}
