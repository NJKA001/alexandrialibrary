using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Games;

namespace LotR.Effects.Phases.Combat
{
    public interface ICharactersDefendStep
    {
        IEncounterInPlay Attacker { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
    }
}
