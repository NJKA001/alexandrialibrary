using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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
                if (state.Exhaustable.BaseCard.Id != source.Id)
                    return;

                if (!state.Exhaustable.IsExhausted)
                    return;

                state.IsReadying = false;

                state.AddEffect(this);
            }

            private void PayResourcesToReadyAttachedCharacter(IGame game, IEffectHandle handle, IPlayer player, ICharacterInPlay character)
            {
                var refreshPhase = game.CurrentPhase as IRefreshPhase;
                if (refreshPhase == null)
                    throw new InvalidOperationException("This effect can only be triggered during the refresh phase");

                var readyingCard = refreshPhase.GetReadyingCards().Where(x => x.Exhaustable.BaseCard.Id == source.Id).FirstOrDefault();
                if (readyingCard == null)
                {
                    handle.Cancel(string.Format("Could not determine the readying state of '{0}'", character.Title));
                    return;
                }

                character.Resources -= 2;
                readyingCard.IsReadying = true;

                handle.Resolve(string.Format("{0} chose to pay 2 resources from {1}'s resource pool to allow them to ready ({2})", player.Name, character.Title, CardSource.Title));
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
                    return base.GetHandle(game);

                var resourceful = attachment.AttachedTo as ICharacterInPlay;
                if (resourceful == null)
                    return base.GetHandle(game);

                var controller = resourceful.GetController(game);
                if (controller == null)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder(string.Format("You may pay 2 resources from {0}'s resource pool to allow them to ready as normal", resourceful.Title), game, controller)
                        .Question(string.Format("{0}, do you want to pay 2 resources from {1}'s resource pool?", controller.Name, resourceful.Title))
                            .Answer(string.Format("Yes, I want to pay 2 resources from {0}'s resource pool", resourceful.Title), true, (source, handle, item) => PayResourcesToReadyAttachedCharacter(source, handle, controller, resourceful))
                            .LastAnswer(string.Format("No, I do not want to pay 2 resources from {0}'s resource pool", resourceful.Title), false, (source, handle, item) => handle.Cancel(string.Format("{0} chose not to pay 2 resources from {1}'s resource pool (2)", controller.Name, resourceful.Title, CardSource.Title)));

                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
