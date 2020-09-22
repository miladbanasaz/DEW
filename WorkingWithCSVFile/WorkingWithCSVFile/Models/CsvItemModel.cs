using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithCSVFile.Models
{
    public class CsvItemModel
    {
        [Display(Name = "DHNO")]
        public int? DHNo { get; set; }

        [Display(Name = "network")]
        public string Network { get; set; }
        [Display(Name = "Unit_Number")]

        public int? UnitNumber { get; set; }
        [Display(Name = "Aquifer")]
        public string Aquifer { get; set; }
        [Display(Name = "Easting")]
        public decimal? Easting { get; set; }
        [Display(Name = "Northing")]
        public decimal? Northing { get; set; }
        [Display(Name = "Zone")]
        public int? Zone { get; set; }
        
        public string UnitNo { get; set; }
        [Display(Name = "Obs_No")]
        public string ObsNo { get; set; }
        [Display(Name = "obs_date")]
        public DateTime? ObsDate { get; set; }

        [Display(Name = "swl")]
        public decimal? Swl { get; set; }

        [Display(Name = "rswl")]
        public decimal? Rswl { get; set; }

        [Display(Name = "calc")]
        public decimal? Calc => (Swl ?? 0)   + (Rswl ?? 0);
        [Display(Name = "pressure")]
        public decimal? Pressure { get; set; }
        [Display(Name = "temperature")]
        public decimal? Temperature { get; set; }
        [Display(Name = "dry_ind")]
        public string DryInd { get; set; }
        [Display(Name = "anom_ind")]
        public string AnomInd { get; set; }
        [Display(Name = "pump_ind")]
        public string PumpInd { get; set; }
        [Display(Name = "measured_during")]
        public string MeasuredDuring { get; set; }
        [Display(Name = "data_source")]
        public string DataSource { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        


    }
}
