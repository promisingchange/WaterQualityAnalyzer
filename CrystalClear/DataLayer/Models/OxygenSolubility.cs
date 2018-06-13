using SQLite;
using System;

namespace CrystalClear.DL.Models
{
    [Table("OxygenSolubility")]
    public class OxygenSolubility
    {
        [PrimaryKey]
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double DO { get; set; }
    }
}
