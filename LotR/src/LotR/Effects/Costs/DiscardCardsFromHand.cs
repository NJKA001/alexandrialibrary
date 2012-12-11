using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.States;

namespace LotR.Effects.Costs
{
    //public class DiscardCardsFromHand
    //    : CostBase
    //{
    //    public DiscardCardsFromHand(ISource source, IGame game, byte numberOfCards)
    //        : base(string.Format("Discard {0} cards from your hand", numberOfCards), source)
    //    {
    //        this.game = game;
    //        this.numberOfCards = numberOfCards;
    //    }

    //    private readonly IGame game;
    //    private readonly byte numberOfCards;

    //    public override bool IsMetBy(IPayment payment)
    //    {
    //        if (payment == null)
    //            return false;

    //        var choice = payment as IChooseCardInHandPayment;
    //        if (choice == null)
    //            return false;

    //        if (choice.Cards.Count() != numberOfCards)
    //            return false;

    //        foreach (var card in choice.Cards)
    //        {
    //            if (!choice.Player.Hand.Cards.Any(x => x.Id == card.Id))
    //                return false;
    //        }

    //        return true;
    //    }
    //}
}
