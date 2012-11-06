using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player.Events
{
    public abstract class EventCardBase
        : CostlyCardBase, IEventCard
    {
        protected EventCardBase(string title, CardSet cardSet, uint cardNumber, Sphere printedSphere, byte resourceCost)
            : base(CardType.Event, title, cardSet, cardNumber, printedSphere, resourceCost)
        {
        }

        public byte PlayerActionCost
        {
            get { return PrintedCost; }
        }
    }
}
