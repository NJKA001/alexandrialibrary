using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Encounter;
using LotR.States;

namespace LotR.Effects.Phases.Combat
{
    public interface ICharactersDefendStep
    {
        ICardInPlay<IEncounterCard> Attacker { get; }
        IEnumerable<IDefendingCard> Defenders { get; }
    }
}
