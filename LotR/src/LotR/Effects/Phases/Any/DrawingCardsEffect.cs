using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class DrawingCardsEffect
        : FrameworkEffectBase
    {
        public DrawingCardsEffect(IGame game, IPlayer player, uint numberOfCards)
            : base("Draw cards", GetDescription(player, numberOfCards), game)
        {
            if (numberOfCards == 0)
                throw new ArgumentException("numberOfCards cannot be zero");

            this.player = player;
            this.numberOfCards = numberOfCards;
        }

        private readonly IPlayer player;
        private readonly uint numberOfCards;

        private static string GetDescription(IPlayer player, uint numberOfCards)
        {
            if (numberOfCards == 1)
                return string.Format("{0} draws 1 card", player.Name);
            else
                return string.Format("{0} draws {1} cards", player.Name, numberOfCards);
        }

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            if (numberOfCards == 0)
                { handle.Cancel(GetCancelledString()); return; }

            player.DrawCards(numberOfCards);

            handle.Resolve(GetCompletedStatus());
        }
    }
}
