using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiyatMandli
{
    public class ResponseModel<T>
    {
        public ResponseCode ResponseCode { get; set; }
        public Operation Operation { get; set; }
        public string ResponseMessage { get; set; }
        public int RecordId { get; set; }
        public T Record { get; set; }
        public List<T> Records { get; set; }
    }

    public class RequestModel<T>
    {
        public Operation Operation { get; set; }
        public T Data { get; set; }
    }

    public enum ResponseCode
    {
        Success = 2,
        NoData = 4,
        Error = 5,
    }

    public enum Operation
    {
        GetAll = 1,
        GetData = 2,
        Insert = 3,
        Update = 4,
        Delete = 5
    }
}
