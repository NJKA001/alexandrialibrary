﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Payments;

namespace LotR.Costs
{
    public class DiscardCardsFromHand
        : CostBase
    {
        public DiscardCardsFromHand(ICard source, IPhaseStep step, byte numberOfCards)
            : base(string.Format("Discard {0} cards from your hand", numberOfCards), source)
        {
            this.step = step;
            this.numberOfCards = numberOfCards;
        }

        private readonly IPhaseStep step;
        private readonly byte numberOfCards;

        public override bool IsMetBy(IPayment payment)
        {
            if (payment == null)
                return false;

            var choice = payment as IChooseCardInHandPayment;
            if (choice == null)
                return false;

            if (choice.Cards.Count() != numberOfCards)
                return false;

            foreach (var card in choice.Cards)
            {
                if (!choice.Player.CardsInHand.Any(x => x.Id == card.Id))
                    return false;
            }

            return true;
        }
    }
}