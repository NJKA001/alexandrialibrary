using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Tests
{
    public class SingleThreadedContext
        : IContext
    {
        public void Invoke(Action action)
        {
            action();
        }
    }
}
