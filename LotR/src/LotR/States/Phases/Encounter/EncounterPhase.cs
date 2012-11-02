using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;

namespace LotR.States.Phases.Encounter
{
    public class EncounterPhase
        : PhaseBase, IEncounterPhase
    {
        public EncounterPhase(IGame game)
            : base(game, PhaseCode.Encounter, PhaseStep.Combat_Start)
        {
        }
    }
}
