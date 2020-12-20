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
        public GenericRecordList<Farmer_model> GetAll(int? id, int startIndex = -1, int fetchRecords = -1, string searchString = "", bool? isActive = null, bool? isDeleted = false)
        {
            GenericRecordList<Farmer_model> model = new GenericRecordList<Farmer_model>();
            try
            {
                var data = new Entities().GetAll_Farmers().Where(x => x.IsDeleted == isDeleted);
                if (isActive.HasValue)
                {
                    data = data.Where(x => x.IsActive == isActive);
                }
                if (id.HasValue)
                {
                    data = data.Where(x => x.Id == id);
                }
                model.TotalRecords = data.Count();
                if (!string.IsNullOrEmpty(searchString))
                {
                    data = data.Where(x => x.Name.Contains(searchString) || x.FarmerCode.Contains(searchString) || x.ShareNo.Contains(searchString) || x.Village.Contains(searchString));
                }
                if (startIndex > -1 && fetchRecords > -1)
                {
                    model.RecordList = data.OrderBy(x => x.Name).Skip(startIndex).Take(fetchRecords).ToList().Select(x => x.ToModel()).ToList();
                }
                else
                {
                    model.RecordList = data.ToList().Select(x => x.ToModel()).ToList();
                }
                if (model.RecordList.Count != 0)
                {
                    model.ReturnCode = ResponseMessages.SuccessCode;
                    model.ReturnMsg = ResponseMessages.SuccessMsg;
                    model.ReturnValue = model.RecordList.Count.ToString();
                }
                else
                {
                    model.ReturnCode = ResponseMessages.NoDataCode;
                    model.ReturnMsg = ResponseMessages.NoDataMsg;
                    model.ReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                model.ReturnCode = ResponseMessages.ErrorCode;
                model.ReturnMsg = ResponseMessages.ErrorMsg + ":" + ex.Message.ToString();
                model.ReturnValue = string.Empty;
                model.RecordList = null;
                AppManager.LogException(ex);
            }
            return model;
        }

        public ResponseModel<Farmer_model> Save(Farmer_model farmer)
        {
            var response = new ResponseModel<Farmer_model>();
            try
            {
                var recordId = 0;
                if (farmer.Id > 0)
                {
                    recordId = new Entities().UpdateEntity_Farmer(farmer.ToEntity());
                }
                else
                {
                    recordId = new Entities().AddEntity_Farmer(farmer.ToEntity());
                }
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

        public GenericClass Delete(int farmerId)
        {
            var response = new GenericClass();
            try
            {
                if (new Entities().RemoveEntity_Farmer(farmerId))
                {
                    response.ReturnCode = ResponseMessages.SuccessCode;
                }
                else
                {
                    response.ReturnCode = ResponseMessages.NoDataCode;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ReturnMsg = ex.Message;
                response.ReturnCode = ResponseMessages.ErrorCode;
            }
            return response;
        }
    }
}
