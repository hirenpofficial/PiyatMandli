using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiyatMandli
{
    public class YearManager
    {
        public GenericRecordList<Year_model> GetAll(int id = 0)
        {
            GenericRecordList<Year_model> model = new GenericRecordList<Year_model>();
            try
            {
                var data = new Entities().GetAll_Years().Where(x => !x.IsDeleted);
                if (id > 0)
                {
                    data = data.Where(x => x.Id == id);
                }
                model.TotalRecords = data.Count();
                model.RecordList = data.OrderByDescending(x => x.Year).ToList().Select(x => x.ToModel()).ToList();
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

        public GenericClass Save(Year_model model)
        {
            var response = new GenericClass();
            try
            {
                var recordId = 0;
                if (model.Id > 0)
                {
                    recordId = new Entities().UpdateEntity_Year(model.ToEntity());
                }
                else
                {
                    recordId = new Entities().AddEntity_Year(model.ToEntity());
                }
                response.ReturnCode = ResponseMessages.SuccessCode;
                response.ReturnValue = recordId.ToString();
                response.ReturnMsg = ResponseMessages.SuccessCode;
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
                if (new Entities().RemoveEntity_Year(id))
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
