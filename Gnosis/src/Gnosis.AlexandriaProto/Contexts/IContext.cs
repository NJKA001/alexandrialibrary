using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Contexts
{
    public interface IContext
    {
        void Invoke(Action action);
    }
}
