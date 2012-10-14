using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter
{
    public interface IEncounterCard
        : ICard, 
        IRevealableCard, 
        IShadowCard
    {
        EncounterSet EncounterSet { get; }
        byte Quantity { get; }
        bool IsGuarded { get; }
    }
}
