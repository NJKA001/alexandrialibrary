using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Contexts
{
    public class SingleThreadedContext
        : IContext
    {
        public SingleThreadedContext()
        {
        }

        #region IContext Members

        public void Invoke(Action action)
        {
            action();
        }

        #endregion
    }
}
