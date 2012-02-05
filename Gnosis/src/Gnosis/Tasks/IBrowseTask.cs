using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public interface IBrowseTask
        : ITask
    {
        MetadataCategory Category { get; }
    }
}
