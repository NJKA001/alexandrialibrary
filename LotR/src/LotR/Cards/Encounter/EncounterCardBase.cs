using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter
{
    public abstract class EncounterCardBase
        : CardBase, IEncounterCard
    {
        protected EncounterCardBase(string title, CardSet cardSet, uint cardNumber)
            : base(title, cardSet, cardNumber)
        {
        }
    }
}
