using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace AspnetCoreDocker.BusinessLayer.Interface
{
    public interface ICatalogue<T>
    {
        IEnumerable<T> GetList();
        T GetDetails(int id);
        int AddNewRecord(T model);
        bool UpdateRecord(int id, T model);
        bool DeleteRecord(int id);
    }
}
