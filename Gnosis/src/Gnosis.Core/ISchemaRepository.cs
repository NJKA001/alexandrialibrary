using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISchemaRepository
    {
        ISchema Get(Uri identifier);

        void Add(ISchema schema);
        void Remove(ISchema schema);
    }
}
