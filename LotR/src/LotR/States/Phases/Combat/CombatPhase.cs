using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;

namespace LotR.States.Phases.Combat
{
    public class CombatPhase
        : PhaseBase, ICombatPhase
    {
        public CombatPhase(IGame game)
            : base(game, PhaseCode.Combat, PhaseStep.Combat_Start)
        {
        }
    }
}
