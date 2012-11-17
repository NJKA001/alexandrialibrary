using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Refresh;
using LotR.States;
using LotR.States.Phases.Refresh;

namespace LotR.Cards.Encounter.Treacheries
{
    public class CaughtInAWeb
        : AttachableTreacheryCardBase
    {
        public CaughtInAWeb()
            : base("Caught in a Web", CardSet.Core, 80, EncounterSet.Spiders_of_Mirkwood, 2)
        {
            AddTrait(Trait.Condition);

            AddEffect(new WhenRevealedAttachToHero(this));
            AddEffect(new HeroDoesNotReadyUnlessControllerPays(this));
        }

        public override bool CanBeAttachedTo(IGame game, ICanHaveAttachments cardInPlay)
        {
            return (cardInPlay is IHeroInPlay);
        }

        private class WhenRevealedAttachToHero
            : WhenRevealedEffectBase
        {
            public WhenRevealedAttachToHero(CaughtInAWeb source)
                : base("The player with the highest threat level attached this card to one of his heroes. (Counts as a Condition attachment with the text: \"Attached hero does not ready during the refresh phase unless you pay 2 resources from that hero's pool.\")", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var highestThreat = -1;
                var mostThreateningPlayers = new List<IPlayer>();
                foreach (var player in game.Players)
                {
                    if (player.CurrentThreat > highestThreat)
                    {
                        mostThreateningPlayers.Clear();
                        mostThreateningPlayers.Add(player);
                        highestThreat = player.CurrentThreat;
                    }
                    else if (player.CurrentThreat == highestThreat)
                    {
                        mostThreateningPlayers.Add(player);
                    }
                }

                var mostThreateningPlayer = mostThreateningPlayers.FirstOrDefault();
                if (mostThreateningPlayer == null)
                    return new EffectHandle(this);

                IChoice choice = null;

                if (mostThreateningPlayers.Count() == 1)
                    choice = new ChooseHero(source, mostThreateningPlayer, mostThreateningPlayer.CardsInPlay.OfType<IHeroInPlay>().ToList());
                else
                    choice = new ChoosePlayerToChooseHero(source, game.FirstPlayer, mostThreateningPlayers);

                return new EffectHandle(this, choice);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var heroChoice = handle.Choice as IChooseHero;
                if (heroChoice == null || heroChoice.ChosenHero == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var attachmentHost = heroChoice.ChosenHero as IAttachmentHostInPlay;
                if (attachmentHost == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var attachable = this.source as IAttachableCard;
                if (attachable == null)
                    { handle.Cancel(GetCancelledString()); return; }

                attachmentHost.AddAttachment(new AttachableInPlay(game, attachable, attachmentHost));

                handle.Resolve(GetCompletedStatus());
            }
        }

        private class HeroDoesNotReadyUnlessControllerPays
            : PassiveCardEffectBase, IDuringReadyingCard
        {
            public HeroDoesNotReadyUnlessControllerPays(CaughtInAWeb source)
                : base("Attached hero does not ready during the refresh phase unless you pay 2 resources from that hero's pool", source)
            {
            }

            public void DuringReadyingCard(ICardReadying state)
            {
                if (state.Exhaustable.Card.Id != source.Id)
                    return;

                if (!state.Exhaustable.IsExhausted)
                    return;

                state.IsReadying = false;

                state.AddEffect(this);
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                IAttachableInPlay attachment = null;

                foreach (var player in game.Players)
                {
                    attachment = player.CardsInPlay.OfType<IAttachableInPlay>().Where(x => x.Card.Id == source.Id).FirstOrDefault();
                    if (attachment != null)
                        break;
                }

                if (attachment == null || attachment.AttachedTo == null)
                    return new EffectHandle(this);

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    return new EffectHandle(this);

                var cost = new PayResourcesFrom(source, resourceful, 2, false);
                cost.IsOptional = true;

                return new EffectHandle(this, cost);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var refreshPhase = game.CurrentPhase as IRefreshPhase;
                if (refreshPhase == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var readyingCard = refreshPhase.GetReadyingCards().Where(x => x.Exhaustable.Card.Id == source.Id).FirstOrDefault();
                if (readyingCard == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var resourcePayment = handle.Payment as IResourcePayment;
                if (resourcePayment == null)
                    { handle.Cancel(GetCancelledString()); return; }

                if (resourcePayment.Characters.Count() != 1)
                    { handle.Cancel(GetCancelledString()); return; }

                var attachment = game.GetCardInPlay<IAttachableInPlay>(CardSource.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var character = resourcePayment.Characters.First();
                if (character.Card.Id != attachment.AttachedTo.Card.Id)
                    { handle.Cancel(GetCancelledString()); return; }

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 2)
                    { handle.Cancel(GetCancelledString()); return; }
                
                readyingCard.IsReadying = true;

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
