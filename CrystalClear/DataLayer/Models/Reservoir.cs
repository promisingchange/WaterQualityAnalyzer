using SQLite;
using System;

namespace CrystalClear.DL.Models
{
    [Table("Reservoir")]
    public class Reservoir
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
