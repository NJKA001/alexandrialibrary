using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;

namespace LotR.States.Areas
{
    public interface IActiveLocationArea
        : IArea
    {
        ICardInPlay<ILocationCard> ActiveLocation { get; }

        void SetActiveLocation(ICardInPlay<ILocationCard> location);
        void RemoveActiveLocation();
    }
}
