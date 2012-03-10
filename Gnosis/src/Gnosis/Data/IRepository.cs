using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IRepository<T>
        where T : IEntity
    {
        IEnumerable<T> Entities { get; }

        T Get(uint id);
        IEnumerable<T> Get(Func<T, bool> predicate);

        void Initialize();
        void Delete(Func<T, bool> predicate);
        void Save(IEnumerable<T> entities);
    }
}
