using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepository<T>
        where T : IEntity
    {
        T Lookup(Guid id);
        T Lookup(ILookup lookup, IEnumerable<KeyValuePair<string, object>> parameters);
        IEnumerable<T> Search();
        IEnumerable<T> Search(ISearch search, IEnumerable<KeyValuePair<string, object>> parameters);

        void Save(IEnumerable<T> items);
        void Delete(IEnumerable<T> items);
    }
}
