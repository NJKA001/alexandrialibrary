using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards.Encounter.Treacheries
{
    public abstract class TreacheryCardBase
        : EncounterCardBase, ITreacheryCard
    {
        protected TreacheryCardBase(string title, CardSet cardSet, uint cardNumber, EncounterSet encounterSet, byte quantity)
            : base(title, cardSet, cardNumber, encounterSet, quantity)
        {
        }
    }
}
