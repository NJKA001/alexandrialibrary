using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public enum TaskStatus
    {
        Ready = 0,
        Running = 1,
        Paused = 2,
        Cancelled = 3,
        Completed = 4,
        Failed = 5
    }
}
