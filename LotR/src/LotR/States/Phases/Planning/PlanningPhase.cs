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

        public override void Run()
        {
            StepCode = PhaseStep.Planning_Start;

            StepCode = PhaseStep.Planning_Play_Allies_and_Attachments;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Planning_Player_Actions_Before_End;

            Game.OpenPlayerActionWindow();

            StepCode = PhaseStep.Planning_End;
        }
    }
}
