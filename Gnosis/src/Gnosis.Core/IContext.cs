using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Core
{
    public interface IContext
    {
        Uri CurrentUser { get; }

        void ChangeCurrentUser(Uri user);
        void Invoke(Action action);
    }
}
