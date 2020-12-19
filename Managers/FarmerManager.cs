using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PiyatMandli.DBModel;

namespace PiyatMandli
{
    public class FarmerManager
    {
        public ResponseModel<Farmer_model> GetAll()
        {
            ResponseModel<Farmer_model> model = new ResponseModel<Farmer_model>();
            try
            {

                var data = new Entities().GetAll_Farmers().Select(x => x.ToModel()).ToList();
                if (data.Count > 0)
                {
                    model.ResponseCode = ResponseCode.Success;
                    model.Records = data;
                }
                else
                {
                    model.ResponseCode = ResponseCode.NoData;
                    model.ResponseMessage = Constants.NoDataMessage;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = AppManager.LogException(ex);
                model.ResponseCode = ResponseCode.Error;
                model.Records = null;
                model.Record = null;
                model.ResponseMessage = Constants.NoDataMessage;
            }
            return model;
        }
        public ResponseModel<Farmer_model> Add(Farmer_model farmer)
        {
            var response = new ResponseModel<Farmer_model>();
            try
            {
                //var recordId = new Entities().AddEntity_Farmer(new Farmer
                //{
                //    FarmerCode = farmer.FarmerCode,
                //    ShareNo = farmer.ShareNo,
                //    Name = farmer.Name,
                //    DateOfBirth = farmer.DateOfBirth,
                //    DateOfBirthEng = farmer.DateOfBirthEng,
                //    AddressLine1 = farmer.AddressLine1,
                //    AddressLine2 = farmer.AddressLine2,
                //    Village = farmer.Village,
                //    City = farmer.City,
                //    State = farmer.State,
                //    Country = farmer.Country,
                //    Pincode = farmer.Pincode,
                //    PhoneNo1 = farmer.PhoneNo1,
                //    PhoneNo2 = farmer.PhoneNo2,
                //    MobileNo1 = farmer.MobileNo1,
                //    MobileNo2 = farmer.MobileNo2,
                //    DateOfRegistration = farmer.DateOfRegistration,
                //    FarmerLands = farmer.Lands.Select(x => new FarmerLand
                //    {
                //        BlockNo = x.BlockNo,
                //        SurveyNo = x.SurveyNo,
                //        WindowId = x.WindowId,
                //        LandName = x.LandName,
                //        LandArea = x.LandArea,
                //        LandAreaEng = x.LandArea.ToEnglish(),
                //    }).ToList()
                //});
                var recordId = new Entities().AddEntity_Farmer(farmer.ToEntity());
                response.RecordId = recordId;
                response.ResponseCode = ResponseCode.Success;
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ResponseMessage = ex.Message;
                response.ResponseCode = ResponseCode.Error;
            }
            return response;
        }

        public ResponseModel<Farmer_model> Update(Farmer_model farmer)
        {
            var response = new ResponseModel<Farmer_model>();
            try
            {
                //var recordId = new Entities().UpdateEntity_Farmer(new Farmer
                //{
                //    Id = farmer.Id,
                //    FarmerCode = farmer.FarmerCode,
                //    ShareNo = farmer.ShareNo,
                //    Name = farmer.Name,
                //    DateOfBirth = farmer.DateOfBirth,
                //    DateOfBirthEng = farmer.DateOfBirthEng,
                //    AddressLine1 = farmer.AddressLine1,
                //    AddressLine2 = farmer.AddressLine2,
                //    Village = farmer.Village,
                //    City = farmer.City,
                //    State = farmer.State,
                //    Country = farmer.Country,
                //    Pincode = farmer.Pincode,
                //    PhoneNo1 = farmer.PhoneNo1,
                //    PhoneNo2 = farmer.PhoneNo2,
                //    MobileNo1 = farmer.MobileNo1,
                //    MobileNo2 = farmer.MobileNo2,
                //    DateOfRegistration = farmer.DateOfRegistration,
                //    FarmerLands = farmer.Lands.Select(x => new FarmerLand
                //    {
                //        Id = x.Id,
                //        BlockNo = x.BlockNo,
                //        SurveyNo = x.SurveyNo,
                //        WindowId = x.WindowId,
                //        LandName = x.LandName,
                //        LandArea = x.LandArea,
                //        LandAreaEng = x.LandArea.ToEnglish(),
                //    }).ToList()
                //});
                var recordId = new Entities().UpdateEntity_Farmer(farmer.ToEntity());
                response.RecordId = recordId;
                response.ResponseCode = ResponseCode.Success;
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ResponseMessage = ex.Message;
                response.ResponseCode = ResponseCode.Error;
            }
            return response;
        }

        public ResponseModel<Farmer_model> Delete(int farmerId)
        {
            var response = new ResponseModel<Farmer_model>();
            try
            {
                if (new Entities().RemoveEntity_Farmer(farmerId))
                {
                    response.ResponseCode = ResponseCode.Success;
                }
                else
                {
                    response.ResponseCode = ResponseCode.NoData;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ResponseMessage = ex.Message;
                response.ResponseCode = ResponseCode.Error;
            }
            return response;
        }
    }
}
