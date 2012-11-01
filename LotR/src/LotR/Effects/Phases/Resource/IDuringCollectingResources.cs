using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Resource;

namespace LotR.Effects.Phases.Resource
{
    public interface IDuringCollectingResources
        : IEffect
    {
        void DuringCollectingResource(ICollectingResources state);
    }
}
