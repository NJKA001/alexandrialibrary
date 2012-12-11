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
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class PlayCardFromHandEffect
        : FrameworkEffectBase, IPlayCardFromHandEffect
    {
        public PlayCardFromHandEffect(IGame game, IPlayer player, ICostlyCard costlyCard)
            : base("Play card from hand", GetDescription(costlyCard), game)
        {
            this.player = player;
            this.costlyCard = costlyCard;
            //this.cost = costlyCard.GetResourceCost(game) as IPayResources;
        }

        private static string GetDescription(IPlayerCard card)
        {
            return string.Format("Pay the cost for and play {0} from your hand", card.Title);
        }

        private readonly IPlayer player;
        private readonly ICostlyCard costlyCard;
        //private readonly IPayResources cost;

        public ICostlyCard CostlyCard
        {
            get { return costlyCard; }
        }

        private IEnumerable<IAttachmentHostInPlay> GetAttachmentHosts(IGame game, IAttachableCard attachment)
        {
            var hosts = new List<IAttachmentHostInPlay>();

            foreach (var player in game.Players)
            {
                hosts.AddRange(player.CardsInPlay.OfType<IAttachmentHostInPlay>().Where(x => x.Card.IsValidAttachment(attachment) && attachment.CanBeAttachedTo(game, x.Card)));
            }

            hosts.AddRange(game.StagingArea.CardsInStagingArea.OfType<IAttachmentHostInPlay>().Where(x => x.Card.IsValidAttachment(attachment) && attachment.CanBeAttachedTo(game, x.Card)));

            if (game.QuestArea.ActiveLocation != null && game.QuestArea.ActiveLocation.Card.IsValidAttachment(attachment) && attachment.CanBeAttachedTo(game, game.QuestArea.ActiveLocation.Card))
            {
                hosts.Add(game.QuestArea.ActiveLocation as IAttachmentHostInPlay);
            }

            return hosts;
        }

        private void AttachCardToHost(IGame game, IEffectHandle handle, IPlayer player, IAttachableCard attachable, IAttachmentHostInPlay host)
        {
            if (attachable is IAttachmentCard)
            {
                var attachment = attachable as IAttachmentCard;
                var attachmentInPlay = new AttachmentInPlay(game, attachment, host);
                player.AddCardInPlay(attachmentInPlay);
                player.Hand.RemoveCards(new List<IPlayerCard> { attachment });
                handle.Resolve(string.Format("{0} attached '{1}' to '{2}'", player.Name, attachment.Title, host.Title));
            }
            else if (attachable is ITreasureCard)
            {
                var treasure = attachable as ITreasureCard;
                var treasureInPlay = new TreasureInPlay(game, treasure, host);
                player.AddCardInPlay(treasureInPlay);
                player.Hand.RemoveCards(new List<IPlayerCard> { treasure });
                handle.Resolve(string.Format("{0} attached '{1}' to '{2}'", player.Name, treasure.Title, host.Title));
            }
        }

        private void PlayAllyFromYourHand(IGame game, IEffectHandle handle, IPlayer player, IAllyCard allyCard)
        {
            var allyInPlay = new AllyInPlay(game, allyCard);
            costlyCard.Owner.AddCardInPlay(allyInPlay);
            costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { allyCard });
            handle.Resolve(string.Format("{0} played '{1}' from their hand", player.Name, allyCard.Title));
        }

        private void PlayEventFromYourHand(IGame game, IEffectHandle handle, IPlayer player, IEventCard eventCard)
        {
            var eventEffect = eventCard.Text.Effects.FirstOrDefault();
            if (eventEffect == null)
            {
                handle.Cancel(string.Format("{0} does not have a valid effect to trigger", eventCard.Title));
                return;
            }

            var eventHandle = eventEffect.GetHandle(game);
            
            game.AddEffect(eventEffect);
            game.TriggerEffect(eventHandle);
            player.Hand.RemoveCards(new List<IPlayerCard> { eventCard });
            handle.Resolve(string.Format("{0} played '{1}' from their hand", player.Name, eventCard.Title));
        }

        public override IEffectHandle GetHandle(IGame game)
        {
            if (costlyCard.PrintedCardType == CardType.Ally)
            {
                var allyCard = costlyCard as IAllyCard;

                var builder =
                    new ChoiceBuilder(string.Format("Play '{0}' from your hand", costlyCard.Title), game, costlyCard.Owner)
                        .Question("You must play this card from your hand")
                            .LastAnswer("Play ally from your hand", allyCard, (source, handle, card) => PlayAllyFromYourHand(game, handle, costlyCard.Owner, card));

                return new EffectHandle(this, builder.ToChoice());
            }
            if (costlyCard.PrintedCardType == CardType.Attachment || costlyCard.PrintedCardType == CardType.Treasure)
            {
                var attachable = costlyCard as IAttachableCard;

                var builder =
                    new ChoiceBuilder(string.Format("Choose the card to attach '{0}' to", attachable.Title), game, costlyCard.Owner)
                        .Question(string.Format("Which card will '{0}' be attached to?", attachable.Title))
                            .LastAnswers(GetAttachmentHosts(game, attachable), (item) => item.Title, (source, handle, host) => AttachCardToHost(game, handle, costlyCard.Owner, attachable, host));
                
                return new EffectHandle(this, builder.ToChoice());
            }
            else if (costlyCard.PrintedCardType == CardType.Event)
            {
                var effect = costlyCard.Text.Effects.FirstOrDefault();
                if (effect == null)
                    return null;
                    //return new EffectHandle(this, cost);

                var eventCard = costlyCard as IEventCard;

                var builder =
                    new ChoiceBuilder(string.Format("Play {0} from your hand", costlyCard.Title), game, costlyCard.Owner)
                        .Question("You must play this card from your hand")
                            .LastAnswer("Play event from your hand", eventCard, (source, handle, card) => PlayEventFromYourHand(game, handle, eventCard.Owner, card));

                return new EffectHandle(this, builder.ToChoice());
            }

            return base.GetHandle(game);
        }

        /*
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
        */

        /*
        public override void Trigger(IGame game, IEffectHandle handle)
        {
            switch (costlyCard.PrintedCardType)
            {
                case CardType.Ally:
                    {
                        //var ally = new AllyInPlay(game, costlyCard as IAllyCard);
                        //costlyCard.Owner.AddCardInPlay(ally);
                        //costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Attachment:
                    {
                        //var hostChoice = handle.Choice as IChooseAttachmentHost;
                        //if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                        //{
                        //    handle.Cancel(GetCancelledString());
                        //    return;
                        //}

                        //var attachment = new AttachmentInPlay(game, costlyCard as IAttachmentCard, hostChoice.ChosenAttachmentHost);
                        //costlyCard.Owner.AddCardInPlay(attachment);
                        //costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Event:
                    {
                        //var costlyEffect = costlyCard.Text.Effects.FirstOrDefault();
                        //if (costlyEffect == null)
                        //{
                        //    handle.Cancel(GetCancelledString());
                        //    return;
                        //}

                        //var costlyHandle = costlyEffect.GetHandle(game);
                        //game.AddEffect(costlyEffect);
                        //game.TriggerEffect(costlyHandle);
                        //costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                case CardType.Treasure:
                    {
                        //var hostChoice = handle.Choice as IChooseAttachmentHost;
                        //if (hostChoice == null || hostChoice.ChosenAttachmentHost == null)
                        //{
                        //    handle.Cancel(GetCancelledString());
                        //    return;
                        //}

                        //var treasure = new TreasureInPlay(game, costlyCard as ITreasureCard, hostChoice.ChosenAttachmentHost);
                        //costlyCard.Owner.AddCardInPlay(treasure);
                        //costlyCard.Owner.Hand.RemoveCards(new List<IPlayerCard> { costlyCard });
                    }
                    break;
                default:
                    break;
            }

            handle.Resolve(GetCompletedStatus());
        }
        */
    }
}
