using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public abstract class Immutable : Model, IImmutable
    {
        protected abstract void OnPopulate(IEnumerable<KeyValuePair<string, object>> data);
        
        public void Populate(IEnumerable<KeyValuePair<string, object>> data)
        {
            OnPopulate(data);
        }
    }
}
