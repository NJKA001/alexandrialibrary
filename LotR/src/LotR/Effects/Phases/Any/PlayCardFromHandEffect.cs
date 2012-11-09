using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Cards.Player.Allies;
using LotR.Cards.Player.Attachments;
using LotR.Cards.Player.Events;
using LotR.Cards.Player.Treasures;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayCardFromHandEffect
        : FrameworkEffectBase
    {
        public PlayCardFromHandEffect(IGame game, ICostlyCard card)
            : base(GetDescription(card), game)
        {
            this.card = card;
            this.cost = card.GetResourceCost(game) as IPayResources;
        }

        private static string GetDescription(IPlayerCard card)
        {
            return string.Format("Play {0} from your hand", card.Title);
        }

        private readonly ICostlyCard card;
        private readonly IPayResources cost;

        public override IChoice GetChoice(IGame game)
        {
            if (card.PrintedCardType == CardType.Attachment && card.PrintedCardType == CardType.Treasure)
            {
                return new ChooseAttachmentHost(card, card.Owner, card as IAttachableCard);
            }
            else if (card.PrintedCardType == CardType.Event)
            {
                var effect = card.Text.Effects.FirstOrDefault();
                if (effect == null)
                    return null;

                return effect.GetChoice(game);
            }
            
            return null;
        }

        public override ICost GetCost(IGame game)
        {
            return card.GetResourceCost(game);
        }

        public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
        {
            if (cost.NumberOfResources == 0)
                return true;

            var resourcePayment = payment as IResourcePayment;
            if (resourcePayment == null)
                return false;

            var total = resourcePayment.Payments.Select(x => (int)x.Item2).Sum();
            if (total < card.PrintedCost)
                return false;

            if (resourcePayment.Payments.Select(x => x.Item1).Any(x => !x.CanPayFor(card, cost)))
                return false;

            foreach (var item in resourcePayment.Payments)
            {
                byte remainder = (item.Item1.Resources - item.Item2 > 0) ? (byte)(item.Item1.Resources - item.Item2) : (byte)0;
                item.Item1.Resources = remainder;
            }

            return true;
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            switch (card.PrintedCardType)
            {
                case CardType.Ally:
                    {
                        var ally = new AllyInPlay(game, card as IAllyCard);
                        card.Owner.AddCardInPlay(ally);
                        card.Owner.Hand.RemoveCards(new List<IPlayerCard> { card });
                    }
                    break;
                case CardType.Attachment:
                    {
                        var hostChoice = choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                            return;

                        var attachment = new AttachmentInPlay(game, card as IAttachmentCard, hostChoice.ChosenAttachmentHost);
                        card.Owner.AddCardInPlay(attachment);
                        card.Owner.Hand.RemoveCards(new List<IPlayerCard> { card });
                    }
                    break;
                case CardType.Event:
                    {
                        var effect = card.Text.Effects.FirstOrDefault();
                        if (effect == null)
                            return;

                        game.AddEffect(effect);
                        game.ResolveEffect(effect, new EffectOptions(payment, choice));
                        card.Owner.Hand.RemoveCards(new List<IPlayerCard> { card });
                    }
                    break;
                case CardType.Treasure:
                    {
                        var hostChoice = choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                            return;

                        var treasure = new TreasureInPlay(game, card as ITreasureCard, hostChoice.ChosenAttachmentHost);
                        card.Owner.AddCardInPlay(treasure);
                        card.Owner.Hand.RemoveCards(new List<IPlayerCard> { card });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
