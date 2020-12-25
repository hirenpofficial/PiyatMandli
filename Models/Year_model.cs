using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiyatMandli
{
    public class Year_model : GenericModel
    {
        public string Year { get; set; }
        public string Rate { get; set; }
        public string ClosingDate { get; set; }
        public string StartingDate { get; set; }
    }
}
