using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Resource
{
    public interface ICollectingResources
    {
        IResourcefulInPlay Resourceful { get; }

        bool IsCollectingResources { get; set; }
        byte ResourcesToCollect { get; set; }
    }
}
