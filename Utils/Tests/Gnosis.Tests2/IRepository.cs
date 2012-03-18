using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IRepository
    {
        IEnumerable<Artist> Artists { get; }
        IEnumerable<Work> Works { get; }

        void Initialize();
        void Save(IEnumerable<Entity> entities);
        void Delete(IEnumerable<Entity> entities);
    }
}
