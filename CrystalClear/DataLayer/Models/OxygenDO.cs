using SQLite;
using System;

namespace CrystalClear.DL.Models
{
    [Table("OxygenDO")]
    public class OxygenDO
    {
        [PrimaryKey]
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double DO { get; set; }
    }
}
