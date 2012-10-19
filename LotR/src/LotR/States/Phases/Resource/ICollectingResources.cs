using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Resource
{
    public interface ICollectingResources
    {
        ICharacterInPlay Character { get; }

        bool IsCollectingResources { get; set; }
        byte ResourcesToCollect { get; set; }
    }
}
