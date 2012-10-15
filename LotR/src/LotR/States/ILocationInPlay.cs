using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;

namespace LotR.States
{
    public interface ILocationInPlay
        : ICardInPlay<ILocationCard>
    {
        byte Progress { get; set; }
    }
}
