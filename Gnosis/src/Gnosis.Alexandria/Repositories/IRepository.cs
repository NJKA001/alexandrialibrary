using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        void Save(T record);
        void Save(IEnumerable<T> records);
        void Delete(Guid id);
        void Delete(IEnumerable<Guid> ids);
        IEnumerable<T> All();
        IEnumerable<T> Search(IEnumerable<KeyValuePair<string, object>> criteria);
    }
}
