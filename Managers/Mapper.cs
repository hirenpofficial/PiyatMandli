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
                TotalLands = farmer.FarmerLands.Count(x => !x.IsDeleted).ToString().ToGujarati(),
                Lands = farmer.FarmerLands.Where(x => !x.IsDeleted).Select(y => y.ToModel()).ToList()
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

        public static YearMaster ToEntity(this Year_model model)
        {
            return new YearMaster
            {
                Id = model.Id,
                Year = model.Year,
                Rate = model.Rate,
                ClosingDate = model.ClosingDate,
                StartingDate = model.StartingDate,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedDate = model.DateCreated,
                ModifiedDate = model.DateModified,
            };
        }

        public static Year_model ToModel(this YearMaster entity)
        {
            return new Year_model
            {
                Id = entity.Id,
                Year = entity.Year,
                Rate = entity.Rate,
                ClosingDate = entity.ClosingDate,
                StartingDate = entity.StartingDate,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                DateCreated = entity.CreatedDate,
                DateModified = entity.ModifiedDate,
            };
        }

        public static Transaction_model ToModel(this Transaction entity)
        {
            var model = new Transaction_model
            {
                Id = entity.Id,
                FarmerId = entity.FarmerId,
                GarotdarId = entity.GarotdarId,
                LandId = entity.LandId,
                LandArea = entity.LandArea,
                LandAreaEng = entity.LandAreaEng,
                Crop = entity.Crop,
                Date = entity.Date,
                DateEng = entity.DateEng,
                Rate = entity.Rate,
                RateEng = entity.RateEng,
                WindowName = entity.FarmerLand.WindowMaster.WindowName,
                YearId = entity.YearId,
                YearDetail = entity.YearMaster.ToModel(),
                Farmer = entity.Farmer.ToModel(),
                Garotdar = entity.Garotdar.ToModel(),
                Land = entity.FarmerLand.ToModel(),
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified,
            };
            model.LocalFundEng = HelperMethods.CalculateLocalFundAmount(model.RateEng, model.LandAreaEng);
            model.LocalFund = model.LocalFundEng.ToString().ToGujarati();
            model.GrossEng = HelperMethods.CalculateGrossAmount(model.RateEng, model.LandAreaEng);
            model.Gross = model.GrossEng.ToString().ToGujarati();
            model.TotalEng = model.LocalFundEng + model.GrossEng;
            model.Total = model.TotalEng.ToString().ToGujarati();
            return model;
        }
        public static Transaction ToEntity(this Transaction_model model)
        {
            return new Transaction
            {
                Id = model.Id,
                Crop = model.Crop,
                Date = model.Date,
                FarmerId = model.FarmerId,
                GarotdarId = model.GarotdarId,
                LandArea = model.LandArea,
                Rate = model.Rate,
                LandId = model.LandId,
                YearId = model.YearId,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                DateEng = Convert.ToDateTime(model.Date.ToEnglish()),
                LandAreaEng = Convert.ToDecimal(model.LandArea.ToEnglish()),
                RateEng = Convert.ToDecimal(model.Rate.ToEnglish())
            };
        }
    }
}
