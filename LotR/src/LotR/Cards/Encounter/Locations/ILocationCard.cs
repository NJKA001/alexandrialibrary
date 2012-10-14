using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

namespace LotR.Cards.Encounter.Locations
{
    public interface ILocationCard
        : IEncounterCard, 
        IProgressableCard, 
        IThreateningCard,
        ICanHaveAttachments,
        IVictoryCard
    {
        ITravelEffect Travel();
    }
}
