using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Any;
using LotR.States;

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

        public override IPlayCardFromHandEffect GetPlayFromHandEffect(IGame game, IPlayer player)
        {
            return new PlayEventEffect(game, PrintedSphere, PrintedCost, IsVariableCost, player, this);
        }
    }
}
