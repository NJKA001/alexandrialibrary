using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IImmutable : IModel
    {
        void Populate(IEnumerable<KeyValuePair<string, object>> data);
    }
}
