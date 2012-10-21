using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Resource
{
    public class ResourcePhase
        : PhaseBase, IResourcePhase
    {
        public ResourcePhase(IGame game)
            : base(game, PhaseCode.Resource, PhaseStep.Resource_Start)
        {
        }
    }
}
