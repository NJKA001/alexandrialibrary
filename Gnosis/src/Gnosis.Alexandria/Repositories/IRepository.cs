using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        T Lookup(Guid id);
        T Lookup(ILookup<T> lookup);
        IEnumerable<T> Search();
        IEnumerable<T> Search(ISearch<T> search);

        void Save(IEnumerable<T> items);
        void Delete(IEnumerable<T> items);
    }
}
