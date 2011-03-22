using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface ICache<T>
    {
        bool IsEmpty { get; }
        bool IsFull { get; }
        T GetOne(object id);
        ICollection<T> GetAll();
        void Put(object id, T model);
    }
}
