using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiyatMandli
{
    public class Transaction_model : GenericModel
    {
        public int FarmerId { get; set; }
        public int? GarotdarId { get; set; }
        public int LandId { get; set; }
        public int YearId { get; set; }
        public string Date { get; set; }
        public DateTime DateEng { get; set; }
        public string LandArea { get; set; }
        public decimal LandAreaEng { get; set; }
        public string Rate { get; set; }
        public decimal RateEng { get; set; }
        public string Gross { get; set; }
        public decimal GrossEng { get; set; }
        public string LocalFund { get; set; }
        public decimal LocalFundEng { get; set; }
        public string Total { get; set; }
        public decimal TotalEng { get; set; }
        public string Crop { get; set; }
        public string WindowName { get; set; }
        public Farmer_model Farmer { get; set; }
        public Farmer_model Garotdar { get; set; }
        public FarmerLand_model Land { get; set; }
        public Year_model YearDetail { get; set; }
    }
}
