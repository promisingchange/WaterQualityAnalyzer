using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalClear.BL.Entities
{
    public class SimpleIndices
    {
        public double Iph { get; set; }
        public double Ido { get; set; }
        public double Ibod5 { get; set; }
        public double Icod { get; set; }
        public double Inh4n { get; set; }
        public double Ino2n { get; set; }
        public double Ino3n { get; set; }
        public double Iss { get; set; }
        public double Icl { get; set; }
        public double Icb { get; set; }

        public String ToJSONString()
        {
            var phStr = "PH: " + Iph.ToString("F5");
            var doStr = "DO: " + Ido.ToString("F5");
            var bod5Str = "BOD5: " + Ibod5.ToString("F5");
            var codStr = "COD: " + Icod.ToString("F5");
            var nh4nStr = "NH4-N: " + Inh4n.ToString("F5");
            var no2nStr = "NO2-N: " + Ino2n.ToString("F5");
            var no3nStr = "NO3-N: " + Ino3n.ToString("F5");
            var ssStr = "SS: " + Iss.ToString("F5");
            var clStr = "CL: " + Icl.ToString("F5");
            var cbStr = "CB: " + Icb.ToString("F5");

            return phStr + ", " + doStr + ", " + bod5Str + ", " + codStr + ", " + nh4nStr + ", " + no2nStr + ", " + no3nStr + ", " + ssStr + ", " + clStr + ", " + cbStr;
        }
    }
}
