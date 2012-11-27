using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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

            private IEnumerable<IHeroInPlay> GetAttachableHeros(IGame game, IPlayer player, IAttachableCard attachable)
            {
                return player.CardsInPlay.OfType<IHeroInPlay>().Where(x => x.Card.IsValidAttachment(attachable) && attachable.CanBeAttachedTo(game, x.Card)).ToList();
            }

            private void AttachCaughtInAWebToHero(IGame game, IEffectHandle handle, IPlayer player, IAttachableCard attachable, IHeroInPlay hero)
            {
                var attachmentHost = hero as IAttachmentHostInPlay;
                
                attachmentHost.AddAttachment(new AttachableInPlay(game, attachable, attachmentHost));

                handle.Resolve(string.Format("{0} chose to attach '{1}' to '{2}'", player.Name, CardSource.Title, hero.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var mostThreateningPlayers = game.Players.Where(x => x.CurrentThreat == game.Players.Max(y => y.CurrentThreat)).ToList();

                var attachable = CardSource as IAttachableCard;

                if (mostThreateningPlayers.Count == 0)
                {
                    return new EffectHandle(this);
                }
                else if (mostThreateningPlayers.Count == 1)
                {
                    var player = mostThreateningPlayers.First();

                    var builder =
                        new ChoiceBuilder(string.Format("{0} has the highest threat and must choose a hero to attach '{0}' to", player.Name, CardSource.Title), game, player)
                            .Question(string.Format("{0}, which hero do you want to attach '{1}' to?", player.Name, CardSource.Title))
                                .LastAnswers(GetAttachableHeros(game, player, attachable), item => item.Title, (source, handle, hero) => AttachCaughtInAWebToHero(game, handle, player, attachable, hero));

                    return new EffectHandle(this, builder.ToChoice());
                }
                else
                {
                    var builder =
                        new ChoiceBuilder(string.Format("Multiple players are tied for the highest threat, The first player, {0}, must choose which of these players will attach '{1}' to one of their heroes.", game.FirstPlayer.Name, CardSource.Title), game, game.FirstPlayer)
                            .Question(string.Format("Which player will attach '{0}' to one of their heroes?", CardSource.Title));

                    foreach (var player in mostThreateningPlayers)
                    {
                        builder.Answer(player.Name, player)
                            .Question(string.Format("{0}, which hero do you want to attach '{1}' to?", player.Name, CardSource.Title))
                                .LastAnswers(GetAttachableHeros(game, player, attachable), item => item.Title, (source, handle, hero) => AttachCaughtInAWebToHero(game, handle, player, attachable, hero));
                    }

                    return new EffectHandle(this, builder.ToChoice());
                }
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
