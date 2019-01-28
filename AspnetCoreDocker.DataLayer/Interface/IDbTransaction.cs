using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetCoreDocker.DataLayer.Interface
{
    public interface IDbTransaction<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        int Insert(T model);
        bool Update(int id, T model);
        bool Delete(int id);
    }
}
