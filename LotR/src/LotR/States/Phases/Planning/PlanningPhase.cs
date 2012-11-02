using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;

namespace LotR.States.Phases.Planning
{
    public class PlanningPhase
        : PhaseBase, IPlanningPhase
    {
        public PlanningPhase(IGame game)
            : base(game, PhaseCode.Planning, PhaseStep.Planning_Start)
        {
        }
    }
}
