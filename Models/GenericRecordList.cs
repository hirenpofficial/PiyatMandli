using System.Collections.Generic;

namespace PiyatMandli
{
    public class GenericRecordList<T> : GenericClass
    {
        public List<T> RecordList { get; set; }
        public int TotalRecords { get; set; }
        public GenericRecordList()
        {
            RecordList = new List<T>();
        }
    }
}
