using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiyatMandli
{
    public class FarmerLandManager
    {
        public GenericRecordList<FarmerLand_model> GetAll(int farmerId)
        {
            GenericRecordList<FarmerLand_model> model = new GenericRecordList<FarmerLand_model>();
            try
            {
                var data = new Entities().GetAll_FarmerLands().Where(x => x.FarmerId == farmerId && x.IsDeleted == false);
                model.TotalRecords = data.Count();
                model.RecordList = data.ToList().Select(x => x.ToModel()).ToList();
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

        public GenericClass Save(FarmerLand_model model)
        {
            var response = new GenericClass();
            try
            {
                var recordId = 0;
                if (model.Id > 0)
                {
                    recordId = new Entities().UpdateEntity_FarmerLand(model.ToEntity());
                }
                else
                {
                    recordId = new Entities().AddEntity_FarmerLand(model.ToEntity());
                }
                response.ReturnValue = recordId.ToString();
                response.ReturnCode = ResponseMessages.SuccessCode;
            }
            catch (Exception ex)
            {
                var errorMessage = AppManager.LogException(ex);
                response.ReturnMsg = ex.Message;
                response.ReturnCode = ResponseMessages.ErrorCode;
            }
            return response;
        }

        public GenericClass Delete(int id)
        {
            var response = new GenericClass();
            try
            {
                if (new Entities().RemoveEntity_FarmerLand(id))
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
