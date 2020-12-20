using PiyatMandli.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiyatMandli
{
    public static class Mapper
    {
        public static Farmer_model ToModel(this Farmer farmer)
        {
            return new Farmer_model
            {
                Id = farmer.Id,
                FarmerCode = farmer.FarmerCode,
                ShareNo = farmer.ShareNo,
                Name = farmer.Name,
                DateOfBirth = farmer.DateOfBirth,
                DateOfBirthEng = farmer.DateOfBirthEng,
                AddressLine1 = farmer.AddressLine1,
                AddressLine2 = farmer.AddressLine2,
                Village = farmer.Village,
                City = farmer.City,
                State = farmer.State,
                Country = farmer.Country,
                Pincode = farmer.Pincode,
                PhoneNo1 = farmer.PhoneNo1,
                PhoneNo2 = farmer.PhoneNo2,
                MobileNo1 = farmer.MobileNo1,
                MobileNo2 = farmer.MobileNo2,
                DateOfRegistration = farmer.DateOfRegistration,
                DateOfRegistrationEng = farmer.DateOfRegistrationEng,
                IsActive = farmer.IsActive,
                IsDeleted = farmer.IsDeleted,
                DateCreated = farmer.DateCreated,
                DateModified = farmer.DateModified,
                TotalLands = farmer.FarmerLands.Count().ToString().ToGujarati(),
                Lands = farmer.FarmerLands.Select(y => y.ToModel()).ToList()
            };
        }
        public static FarmerLand_model ToModel(this FarmerLand land)
        {
            return new FarmerLand_model
            {
                Id = land.Id,
                FarmerId = land.FarmerId,
                BlockNo = land.BlockNo,
                SurveyNo = land.SurveyNo,
                LandName = land.LandName,
                LandArea = land.LandArea,
                LandAreaEng = land.LandAreaEng,
                WindowId = land.WindowId,
                WindowName = land.WindowMaster.WindowName,
                IsActive = land.IsActive,
                IsDeleted = land.IsDeleted,
                DateCreated = land.CreatedDate,
                DateModified = land.ModifiedDate
            };
        }

        public static Farmer ToEntity(this Farmer_model farmer)
        {
            return new Farmer
            {
                Id = farmer.Id,
                FarmerCode = farmer.FarmerCode,
                ShareNo = farmer.ShareNo,
                Name = farmer.Name,
                DateOfBirth = farmer.DateOfBirth,
                DateOfBirthEng = farmer.DateOfBirthEng,
                AddressLine1 = farmer.AddressLine1,
                AddressLine2 = farmer.AddressLine2,
                Village = farmer.Village,
                City = farmer.City,
                State = farmer.State,
                Country = farmer.Country,
                Pincode = farmer.Pincode,
                PhoneNo1 = farmer.PhoneNo1,
                PhoneNo2 = farmer.PhoneNo2,
                MobileNo1 = farmer.MobileNo1,
                MobileNo2 = farmer.MobileNo2,
                DateOfRegistration = farmer.DateOfRegistration,
                DateOfRegistrationEng = farmer.DateOfRegistrationEng,
                IsActive = farmer.IsActive,
                IsDeleted = farmer.IsDeleted,
                DateCreated = farmer.DateCreated,
                DateModified = farmer.DateModified,
            };
        }
        public static FarmerLand ToEntity(this FarmerLand_model land)
        {
            return new FarmerLand
            {
                Id = land.Id,
                FarmerId = land.FarmerId,
                BlockNo = land.BlockNo,
                SurveyNo = land.SurveyNo,
                LandName = land.LandName,
                LandArea = land.LandArea,
                LandAreaEng = land.LandAreaEng,
                WindowId = land.WindowId,
                IsActive = land.IsActive,
                IsDeleted = land.IsDeleted,
            };
        }

        public static WindowMaster ToEntity(this Window_model window)
        {
            return new WindowMaster
            {
                Id = window.Id,
                WindowName = window.WindowName,
                IsActive = window.IsActive,
                IsDeleted = window.IsDeleted,
                CreatedDate = window.DateCreated,
                ModifiedDate = window.DateModified,
            };
        }

        public static Window_model ToModel(this WindowMaster window)
        {
            return new Window_model
            {
                Id = window.Id,
                WindowName = window.WindowName,
                IsActive = window.IsActive,
                IsDeleted = window.IsDeleted,
                DateCreated = window.CreatedDate,
                DateModified = window.ModifiedDate,
            };
        }
    }
}
