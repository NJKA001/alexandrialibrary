using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Games.Phases.Any
{
    public interface IEncounterCardRevealedStep
        : IPhaseStep
    {
        IEncounterCard Card { get; }
    }
}
