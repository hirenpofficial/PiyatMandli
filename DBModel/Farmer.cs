//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PiyatMandli.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Farmer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Farmer()
        {
            this.FarmerLands = new HashSet<FarmerLand>();
        }
    
        public int Id { get; set; }
        public string FarmerCode { get; set; }
        public string ShareNo { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public Nullable<System.DateTime> DateOfBirthEng { get; set; }
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
        public Nullable<System.DateTime> DateOfRegistrationEng { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FarmerLand> FarmerLands { get; set; }
    }
}