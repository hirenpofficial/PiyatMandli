using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PiyatMandli.DBModel;

namespace PiyatMandli
{
    public class TransactionManager
    {
        public GenericRecordList<Transaction_model> GetAll(int? id, int startIndex = -1, int fetchRecords = -1, string searchString = "", bool? isActive = null, bool? isDeleted = false)
        {
            GenericRecordList<Transaction_model> model = new GenericRecordList<Transaction_model>();
            try
            {
                var data = new Entities().GetAll_Transactions().Where(x => x.IsDeleted == isDeleted);
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
                    data = data.Where(x => x.Farmer.Name.Contains(searchString) || x.Farmer.FarmerCode.Contains(searchString) || x.Farmer.ShareNo.Contains(searchString) || x.Farmer.Village.Contains(searchString));
                }
                if (startIndex > -1 && fetchRecords > -1)
                {
                    model.RecordList = data.OrderByDescending(x => x.DateEng).Skip(startIndex).Take(fetchRecords).ToList().Select(x => x.ToModel()).ToList();
                }
                else
                {
                    model.RecordList = data.OrderByDescending(x => x.DateEng).ToList().Select(x => x.ToModel()).ToList();
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

        public GenericClass Save(Transaction_model model)
        {
            var response = new GenericClass();
            try
            {
                var recordId = 0;
                if (model.Id > 0)
                {
                    recordId = new Entities().UpdateEntity_Transaction(model.ToEntity());
                }
                else
                {
                    recordId = new Entities().AddEntity_Transaction(model.ToEntity());
                }
                response.ReturnValue = recordId.ToString();
                response.ReturnCode = ResponseMessages.SuccessCode;
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ReturnMsg= ex.Message;
                response.ReturnCode= ResponseMessages.ErrorCode;
            }
            return response;
        }

        public GenericClass Delete(int transactionId)
        {
            var response = new GenericClass();
            try
            {
                if (new Entities().RemoveEntity_Transaction(transactionId))
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
