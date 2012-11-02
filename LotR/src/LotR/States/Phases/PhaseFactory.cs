using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Controllers;
using LotR.States.Phases.Combat;
using LotR.States.Phases.Encounter;
using LotR.States.Phases.Planning;
using LotR.States.Phases.Quest;
using LotR.States.Phases.Refresh;
using LotR.States.Phases.Resource;
using LotR.States.Phases.Travel;

namespace LotR.States.Phases
{
    public class PhaseFactory
    {
        public IPhase GetNextPhase(IGame game)
        {
            if (game.CurrentPhase == null)
                return new ResourcePhase(game);

            switch (game.CurrentPhase.Code)
            {
                case PhaseCode.Resource:
                    return new PlanningPhase(game);
                case PhaseCode.Planning:
                    return new QuestPhase(game);
                case PhaseCode.Quest:
                    return new TravelPhase(game);
                case PhaseCode.Travel:
                    return new EncounterPhase(game);
                case PhaseCode.Encounter:
                    return new CombatPhase(game);
                case PhaseCode.Combat:
                    return new RefreshPhase(game);
                case PhaseCode.Refresh:
                    return new ResourcePhase(game);
                case PhaseCode.None:
                default:
                    throw new InvalidOperationException("Current phase is unknown");
            }
        }
    }
}
