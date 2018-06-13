using SQLite;
using System;

namespace CrystalClear.DL.Models
{
    [Table("Record")]
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Temperature { get; set; }
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
        public double Overall { get; set; }
        public int Level { get; set; }
        public string DateTime { get; set; }
        public int ReservoirId { get; set; }
    }
}
