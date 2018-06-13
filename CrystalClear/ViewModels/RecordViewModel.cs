using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrystalClear.DL.Models;

namespace CrystalClear.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public string TemperatureRange { get; set; }
        public string PH { get; set; }
        public string DO { get; set; }
        public string BOD5 { get; set; }
        public string COD { get; set; }
        public string NH4N { get; set; }
        public string NO2N { get; set; }
        public string NO3N { get; set; }
        public string SS { get; set; }
        public string CL { get; set; }
        public string CB { get; set; }
        public string Overall { get; set; }
        public string Level { get; set; }
        public string DateTime { get; set; }
        public int ReservoirId { get; set; }
        public string ReservoirName { get; set; }
        public RecordViewModel()
        {

        }
        public RecordViewModel(RecordExtended record)
        {
            this.Id = record.Id;
            if (record.Temperature == 10)
            {
                this.TemperatureRange = "0~10°C";
            } else if (record.Temperature == 20)
            {
                this.TemperatureRange = "11~20°C";
            } else if (record.Temperature == 30)
            {
                this.TemperatureRange = "21~30°C";
            }
            this.Temperature = record.Temperature;
            this.PH = record.PH.ToString("F");
            this.DO = record.DO.ToString("F");
            this.BOD5 = record.BOD5.ToString("F");
            this.COD = record.COD.ToString("F");
            this.NH4N = record.NH4N.ToString("F");
            this.NO2N = record.NO2N.ToString("F");
            this.NO3N = record.NO3N.ToString("F");
            this.SS = record.SS.ToString("F");
            this.CL = record.CL.ToString("F");
            this.CB = record.CB.ToString("F");
            this.Overall = record.Overall.ToString("F5");
            this.Level = record.Level.ToString("D");
            this.DateTime = record.DateTime;
            this.ReservoirId = record.ReservoirId;
            this.ReservoirName = record.ReservoirName;
        }
        public Record ToRecord()
        {
            return new Record
            {
                Id = this.Id,
                Temperature = this.Temperature,
                PH = double.Parse(this.PH),
                DO = double.Parse(this.DO),
                BOD5 = double.Parse(this.BOD5),
                COD = double.Parse(this.COD),
                NH4N = double.Parse(this.NH4N),
                NO2N = double.Parse(this.NO2N),
                NO3N = double.Parse(this.NO3N),
                SS = double.Parse(this.SS),
                CL = double.Parse(this.CL),
                CB = double.Parse(this.CB),
                Overall = double.Parse(this.Overall),
                Level = int.Parse(this.Level),
                DateTime = this.DateTime,
                ReservoirId = this.ReservoirId
            };
        }
    }
}
