using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITaskItem
    {
        string Name { get; }
        int Number { get; }
        IMedia Media { get; }
    }
}
