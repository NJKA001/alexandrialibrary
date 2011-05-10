using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public interface IRepository<T>
    {
        T GetOne(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAny(ISearch<T> search);

        void Save(IEnumerable<T> tracks);
    }
}
