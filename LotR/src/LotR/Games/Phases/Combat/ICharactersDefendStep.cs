using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Games.Phases.Combat
{
    public interface ICharactersDefendStep
    {
        IEncounterInPlay Attacker { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
    }
}
