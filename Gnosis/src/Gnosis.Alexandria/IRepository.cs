using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Save(T record);
        void Delete(Guid id);
        IEnumerable<T> All();
        IEnumerable<T> Search(IEnumerable<KeyValuePair<string, object>> criteria);
    }
}
