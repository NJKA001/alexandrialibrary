using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ISourceRepository
    {
        ISource Get(Guid id);
        void Save(ISource track);
        void Delete(Guid id);
        IEnumerable<ISource> Sources();
        IEnumerable<ISource> Sources(IEnumerable<KeyValuePair<string, object>> criteria);
    }
}
