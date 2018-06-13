using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrystalClear.DL.Models;

namespace CrystalClear.ViewModels
{
    public class ReservoirViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ReservoirViewModel()
        {

        }
        public ReservoirViewModel(Reservoir reservoir)
        {
            Id = reservoir.Id;
            Name = reservoir.Name;
        }
    }
}
