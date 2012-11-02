using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Resource
{
    public class DrawingCardsEffect
        : FrameworkEffectBase
    {
        public DrawingCardsEffect(IGame game, IPlayer player, uint numberOfCards)
            : base(GetDescription(player, numberOfCards), game)
        {
            this.player = player;
            this.numberOfCards = numberOfCards;
        }

        private readonly IPlayer player;
        private readonly uint numberOfCards;

        private static string GetDescription(IPlayer player, uint numberOfCards)
        {
            if (numberOfCards == 0)
                return string.Format("{0} does not draw any cards", player.Name);
            else if (numberOfCards == 1)
                return string.Format("{0} draws 1 card", player.Name);
            else
                return string.Format("{0} draws {1} cards", player.Name, numberOfCards);
        }

        public override void Resolve(IGame game, Payments.IPayment payment, Choices.IChoice choice)
        {
            if (numberOfCards == 0)
                return;

            player.DrawCards(numberOfCards);
        }
    }
}
