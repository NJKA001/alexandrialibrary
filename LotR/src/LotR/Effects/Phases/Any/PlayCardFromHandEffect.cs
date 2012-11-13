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
        : FrameworkEffectBase, IPlayCardFromHandEffect
    {
        public PlayCardFromHandEffect(IGame game, ICostlyCard costlyCard)
            : base("Play card from hand", GetDescription(costlyCard), game)
        {
            this.costlyCard = costlyCard;
            this.cost = costlyCard.GetResourceCost(game) as IPayResources;
        }

        private static string GetDescription(IPlayerCard card)
        {
            return string.Format("Pay the cost for and play {0} from your hand", card.Title);
        }

        private readonly ICostlyCard costlyCard;
        private readonly IPayResources cost;

        public ICostlyCard CostlyCard
        {
            get { return costlyCard; }
        }

        public override IChoice GetChoice(IGame game)
        {
            if (costlyCard.PrintedCardType == CardType.Attachment && costlyCard.PrintedCardType == CardType.Treasure)
            {
                return new ChooseAttachmentHost(costlyCard, costlyCard.Owner, costlyCard as IAttachableCard);
            }
            else if (costlyCard.PrintedCardType == CardType.Event)
            {
                var effect = costlyCard.Text.Effects.FirstOrDefault();
                if (effect == null)
                    return null;

                return effect.GetChoice(game);
            }
            
            return null;
        }

        public override ICost GetCost(IGame game)
        {
            return costlyCard.GetResourceCost(game);
        }

        public override bool PaymentAccepted(IGame game, IPayment payment, IChoice choice)
        {
            if (cost.NumberOfResources == 0)
                return true;

            var resourcePayment = payment as IResourcePayment;
            if (resourcePayment == null)
                return false;

            if (resourcePayment.GetTotalPayment() < costlyCard.PrintedCost && !cost.IsVariableCost)
                return false;

            if (resourcePayment.Characters.Any(x => !x.CanPayFor(costlyCard)))
                return false;

            foreach (var character in resourcePayment.Characters)
            {
                var numberOfResources = resourcePayment.GetPaymentBy(character.Card.Id);
                byte remainder = (character.Resources - numberOfResources > 0) 
                    ? (byte)(character.Resources - numberOfResources) : (byte)0;

                character.Resources = remainder;
            }

            return true;
        }

        public override void Resolve(IGame game, IPayment payment, IChoice choice)
        {
            switch (costlyCard.PrintedCardType)
            {
                case CardType.Ally:
                    {
                        var ally = new AllyInPlay(game, costlyCard as IAllyCard);
                        costlyCard.Owner.AddCardInPlay(ally);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Attachment:
                    {
                        var hostChoice = choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                            return;

                        var attachment = new AttachmentInPlay(game, costlyCard as IAttachmentCard, hostChoice.ChosenAttachmentHost);
                        costlyCard.Owner.AddCardInPlay(attachment);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Event:
                    {
                        var effect = costlyCard.Text.Effects.FirstOrDefault();
                        if (effect == null)
                            return;

                        game.AddEffect(effect);
                        game.ResolveEffect(effect, new EffectOptions(payment, choice));
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Treasure:
                    {
                        var hostChoice = choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                            return;

                        var treasure = new TreasureInPlay(game, costlyCard as ITreasureCard, hostChoice.ChosenAttachmentHost);
                        costlyCard.Owner.AddCardInPlay(treasure);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
