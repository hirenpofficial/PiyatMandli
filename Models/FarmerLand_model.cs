using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiyatMandli
{
    public class FarmerLand_model : GenericModel
    {
        public int FarmerId { get; set; }
        public string BlockNo { get; set; }
        public string SurveyNo { get; set; }
        public string LandName { get; set; }
        public string LandArea { get; set; }
        public string LandAreaEng { get; set; }
        public int WindowId { get; set; }
        public string WindowName { get; set; }
    }
}
