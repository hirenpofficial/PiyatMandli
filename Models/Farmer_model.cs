using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PiyatMandli
{
    public class Farmer_model : GenericModel
    {
        public string FarmerCode { get; set; }
        public string ShareNo { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime? DateOfBirthEng { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string DateOfRegistration { get; set; }
        public DateTime? DateOfRegistrationEng { get; set; }
        public string TotalLands { get; set; }
        public List<FarmerLand_model> Lands { get; set; }
    }
}
