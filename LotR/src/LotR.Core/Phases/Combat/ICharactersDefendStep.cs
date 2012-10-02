using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.Phases.Combat
{
    public interface ICharactersDefendStep
    {
        IEncounterInPlay Attacker { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
    }
}
