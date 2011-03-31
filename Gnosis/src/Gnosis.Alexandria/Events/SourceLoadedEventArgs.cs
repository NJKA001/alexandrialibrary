using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Events
{
    public class SourceLoadedEventArgs : EventArgs
    {
        public SourceLoadedEventArgs(ISource source)
        {
            Source = source;
        }

        public ISource Source
        {
            private set;
            get;
        }
    }
}
