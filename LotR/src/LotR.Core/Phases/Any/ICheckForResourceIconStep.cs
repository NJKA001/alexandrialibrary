using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Any
{
    public interface ICheckForResourceIconStep
        : IPhaseStep
    {
        Sphere ResourceIcon { get; }
        IResourcefulCard Source { get; }
        bool HasResourceIcon { get; set; }
    }
}
