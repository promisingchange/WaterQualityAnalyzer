using SQLite;

namespace CrystalClear.DL.Models
{
    [Table("BaseIndex")]
    public class BaseIndex
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Category { get; set; }
        public int Level { get; set; }
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

    }
}
