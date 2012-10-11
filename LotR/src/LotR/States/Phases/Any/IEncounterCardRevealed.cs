using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.States.Phases.Any
{
    public interface IEncounterCardRevealed
        : IState
    {
        IEncounterCard EncounterCard { get; }
    }
}
