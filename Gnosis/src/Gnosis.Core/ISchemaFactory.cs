using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISchemaFactory
    {
        ISchema Create(Uri identifier, string name);
        ISchema Create(Uri identifier, string name, long localId);
    }
}
