using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IEntityRepository
    {
        IEnumerable<IArtist> Artists { get; }
        IEnumerable<IWork> Works { get; }

        void Initialize();
        void Save(IEnumerable<IEntity> entities);
        void Delete(IEnumerable<IEntity> entities);
    }
}
