using SQLite;
using System;

namespace CrystalClear.DL.Models
{
    [Table("EnvironmentalStandard")]
    public class EnvironmentalStandard
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Level { get; set; }
        public double PH1 { get; set; }
        public double PH2 { get; set; }
        public double DO { get; set; }
        public double BOD5 { get; set; }
        public double COD { get; set; }
        public double NH4N { get; set; }
        public double NO2N { get; set; }
        public double NO3N { get; set; }
        public double SS { get; set; }
        public double CL { get; set; }
        public double CB { get; set; }
    }
}
