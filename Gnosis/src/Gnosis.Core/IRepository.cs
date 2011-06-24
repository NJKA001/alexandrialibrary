using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Queries;

namespace Gnosis.Core
{
    public interface IRepository<T>
        where T : IEntity
    {
        T Lookup(Guid id);
        T Lookup(IQuery<T> query);
        IEnumerable<T> Search();
        IEnumerable<T> Search(IQuery<T> query);

        void Initialize();
        void Save(T item);
        void Save(IEnumerable<T> items);
        void Delete(T item);
        void Delete(IEnumerable<T> items);
    }
}
