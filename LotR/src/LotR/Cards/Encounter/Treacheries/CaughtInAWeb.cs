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

            public override IEffectOptions GetOptions(IGame game)
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
                    return new EffectOptions();

                IChoice choice = null;

                if (mostThreateningPlayers.Count() == 1)
                    choice = new ChooseHero(Source, mostThreateningPlayer, mostThreateningPlayer.CardsInPlay.OfType<IHeroInPlay>().ToList());
                else
                    choice = new ChoosePlayerToChooseHero(Source, game.FirstPlayer, mostThreateningPlayers);

                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var heroChoice = options.Choice as IChooseHero;
                if (heroChoice == null || heroChoice.ChosenHero == null)
                    return GetCancelledString();

                var attachmentHost = heroChoice.ChosenHero as IAttachmentHostInPlay;
                if (attachmentHost == null)
                    return GetCancelledString();

                var attachable = this.Source as IAttachableCard;
                if (attachable == null)
                    return GetCancelledString();

                attachmentHost.AddAttachment(new AttachableInPlay(game, attachable, attachmentHost));

                return ToString();
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
                if (state.Exhaustable.Card.Id != Source.Id)
                    return;

                if (!state.Exhaustable.IsExhausted)
                    return;

                state.IsReadying = false;

                state.AddEffect(this);
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                IAttachableInPlay attachment = null;

                foreach (var player in game.Players)
                {
                    attachment = player.CardsInPlay.OfType<IAttachableInPlay>().Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                    if (attachment != null)
                        break;
                }

                if (attachment == null || attachment.AttachedTo == null)
                    return new EffectOptions();

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    return new EffectOptions();

                var cost = new PayResourcesFrom(Source, resourceful, 2, false);
                cost.IsOptional = true;

                return new EffectOptions(cost);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var refreshPhase = game.CurrentPhase as IRefreshPhase;
                if (refreshPhase == null)
                    return GetCancelledString();

                var readyingCard = refreshPhase.GetReadyingCards().Where(x => x.Exhaustable.Card.Id == Source.Id).FirstOrDefault();
                if (readyingCard == null)
                    return GetCancelledString();

                var resourcePayment = options.Payment as IResourcePayment;
                if (resourcePayment == null)
                    return GetCancelledString();

                if (resourcePayment.Characters.Count() != 1)
                    return GetCancelledString();

                var attachment = game.GetCardInPlay<IAttachableInPlay>(CardSource.Id);
                if (attachment == null || attachment.AttachedTo == null)
                    return GetCancelledString();

                var character = resourcePayment.Characters.First();
                if (character.Card.Id != attachment.AttachedTo.Card.Id)
                    return GetCancelledString();

                if (resourcePayment.GetPaymentBy(character.Card.Id) != 2)
                    return GetCancelledString();
                
                readyingCard.IsReadying = true;

                return ToString();
            }
        }
    }
}
