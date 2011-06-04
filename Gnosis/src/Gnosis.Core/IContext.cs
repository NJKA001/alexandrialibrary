using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Core
{
    public interface IContext
    {
        void Invoke(Action action);
    }
}
