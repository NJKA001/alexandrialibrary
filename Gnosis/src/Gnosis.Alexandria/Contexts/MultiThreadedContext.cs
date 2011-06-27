using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace Gnosis.Alexandria.Contexts
{
    public class MultiThreadedContext
        : IContext
    {
        public MultiThreadedContext(Dispatcher dispatcher)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");

            this.dispatcher = dispatcher;
        }

        private readonly Dispatcher dispatcher;

        #region IContext Members

        public void Invoke(Action action)
        {
            if (dispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                dispatcher.Invoke(action, DispatcherPriority.DataBind, null);
            }
        }

        #endregion
    }
}
