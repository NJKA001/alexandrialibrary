using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISchemaRepository
    {
        ISchema Lookup(long localId);
        ISchema Lookup(Uri identifier);

        void Delete(ISchema schema);
        void Save(ISchema schema);
    }
}
