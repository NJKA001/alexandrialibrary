using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITaskItem
    {
        Uri Id { get; }
        uint Number { get; }
        string Name { get; }
        TimeSpan Duration { get; }
        object Image { get; }
    }
}
