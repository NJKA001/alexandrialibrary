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

        public override IEffectHandle GetHandle(IGame game)
        {
            var cost = costlyCard.GetResourceCost(game);
            IChoice choice = null;

            if (costlyCard.PrintedCardType == CardType.Attachment && costlyCard.PrintedCardType == CardType.Treasure)
            {
                choice = new ChooseAttachmentHost(costlyCard, costlyCard.Owner, costlyCard as IAttachableCard);
                return new EffectHandle(this, choice, cost);
            }
            else if (costlyCard.PrintedCardType == CardType.Event)
            {
                var effect = costlyCard.Text.Effects.FirstOrDefault();
                if (effect == null)
                    return new EffectHandle(this, cost);

                var effectHandle = effect.GetHandle(game);
                choice = effectHandle.Choice;

                return new EffectHandle(this, choice, cost);
            }

            return new EffectHandle(this, cost);
        }

        public override void Validate(IGame game, IEffectHandle handle)
        {
            if (cost.NumberOfResources == 0)
            {
                handle.Accept();
                return;
            }

            var resourcePayment = handle.Payment as IResourcePayment;
            if (resourcePayment == null)
            {
                handle.Reject();
                return;
            }

            if (resourcePayment.GetTotalPayment() < costlyCard.PrintedCost && !cost.IsVariableCost)
            {
                handle.Reject();
                return;
            }

            if (resourcePayment.Characters.Any(x => !x.CanPayFor(costlyCard)))
            {
                handle.Reject();
                return;
            }

            foreach (var character in resourcePayment.Characters)
            {
                var numberOfResources = resourcePayment.GetPaymentBy(character.Card.Id);
                byte remainder = (character.Resources - numberOfResources > 0) 
                    ? (byte)(character.Resources - numberOfResources) : (byte)0;

                character.Resources = remainder;
            }

            handle.Accept();
        }

        public override void Trigger(IGame game, IEffectHandle handle)
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
                        var hostChoice = handle.Choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                        {
                            handle.Cancel(GetCancelledString());
                            return;
                        }

                        var attachment = new AttachmentInPlay(game, costlyCard as IAttachmentCard, hostChoice.ChosenAttachmentHost);
                        costlyCard.Owner.AddCardInPlay(attachment);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Event:
                    {
                        var costlyEffect = costlyCard.Text.Effects.FirstOrDefault();
                        if (costlyEffect == null)
                        {
                            handle.Cancel(GetCancelledString());
                            return;
                        }

                        var costlyHandle = costlyEffect.GetHandle(game);
                        game.AddEffect(costlyEffect);
                        game.TriggerEffect(costlyHandle);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Treasure:
                    {
                        var hostChoice = handle.Choice as IChooseAttachmentHost;
                        if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                        {
                            handle.Cancel(GetCancelledString());
                            return;
                        }

                        var treasure = new TreasureInPlay(game, costlyCard as ITreasureCard, hostChoice.ChosenAttachmentHost);
                        costlyCard.Owner.AddCardInPlay(treasure);
                        costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                default:
                    break;
            }

            handle.Resolve(GetCompletedStatus());
        }
    }
}
