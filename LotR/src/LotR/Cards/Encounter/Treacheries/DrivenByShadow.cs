using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Player;
using LotR.Effects;

using LotR.Effects.Modifiers;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Treacheries
{
    public class DrivenByShadow
        : TreacheryCardBase
    {
        public DrivenByShadow()
            : base("Driven by Shadow", CardSet.Core, 92, EncounterSet.Dol_Guldur_Orcs, 1)
        {
            AddEffect(new WhenRevealedEachEnemeyAndLocationInStagingAreaGainsOneThreat(this));
            AddEffect(new ShadowDiscardOneAttachmentFromDefendingCharacter(this));
        }

        private class WhenRevealedEachEnemeyAndLocationInStagingAreaGainsOneThreat
            : WhenRevealedEffectBase
        {
            public WhenRevealedEachEnemeyAndLocationInStagingAreaGainsOneThreat(DrivenByShadow source)
                : base("Each enemy and each location in the staging area gets +1 Threat until the of the phase. If there are no cards in the staging area, Driven by Shadow gains surge.", source)
            {
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                if (game.StagingArea.CardsInStagingArea.Count() == 0)
                {
                    game.StagingArea.RevealEncounterCard();
                    {
                        handle.Cancel(GetCancelledString());
                        return;
                    }
                }

                foreach (var threatening in game.StagingArea.CardsInStagingArea.OfType<IThreateningInPlay>())
                {
                    game.AddEffect(new ThreatModifier(game.CurrentPhase.Code, source, threatening, TimeScope.Phase, 1));
                }

                handle.Resolve(GetCompletedStatus());
            }
        }

        private class ShadowDiscardOneAttachmentFromDefendingCharacter
            : ShadowEffectBase
        {
            public ShadowDiscardOneAttachmentFromDefendingCharacter(DrivenByShadow source)
                : base("Choose and discard 1 attachment from the defending character. (If this attack is undefended, discard all attachments you control.)", source)
            {
            }

            private void DiscardAllAttachmentsControlledByDefendingPlayer(IGame game, IEffectHandle handle, IPlayer player)
            {
                foreach (var attachable in player.CardsInPlay.OfType<IAttachableInPlay>().Where(x => player.IsTheControllerOf(x) && x.AttachedTo != null))
                {
                    attachable.AttachedTo.RemoveAttachment(attachable);

                    if (attachable.Card is IPlayerCard)
                    {
                        player.Deck.Discard(new List<IPlayerCard> { attachable.Card as IPlayerCard });
                    }
                    else if (attachable.Card is IObjectiveCard)
                    {
                        game.StagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachable.Card as IObjectiveCard });
                    }
                }

                handle.Resolve(string.Format("All attachments controlled by {0} were discarded", player.Name));
            }

            private void DiscardOneAttachmentFromDefendingCharacter(IGame game, IEffectHandle handle, IPlayer player, IAttachableInPlay attachment)
            {
                attachment.AttachedTo.RemoveAttachment(attachment);

                if (attachment.Card is IObjectiveCard)
                {
                    game.StagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachment.Card as IObjectiveCard });
                }
                else if (attachment.Card is IPlayerCard)
                {
                    player.Deck.Discard(new List<IPlayerCard> { attachment.Card as IPlayerCard });
                }
                else
                {
                    handle.Cancel(string.Format("Could not discard '{0}' because it was not a valid attachment controlled by {1}", attachment.Title, player.Name));
                    return;
                }

                handle.Resolve(string.Format("{0} chose to have '{1}' discarded", player.Name, attachment.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.ShadowCards.Any(y => y.Card.Id == CardSource.Id)).FirstOrDefault();
                if (enemyAttack == null)
                    return base.GetHandle(game);

                var player = enemyAttack.DefendingPlayer;

                if (enemyAttack.IsUndefended)
                {
                    var undefendedChoiceBuilder =
                        new ChoiceBuilder("Defending player must discard all attachments they control", game, player)
                            .Question(string.Format("{0} must discard all attachments they control", player))
                                .Answer("Yes", player, (source, handle, item) => DiscardAllAttachmentsControlledByDefendingPlayer(source, handle, item));

                    return new EffectHandle(this, undefendedChoiceBuilder.ToChoice());
                }

                var builder =
                    new ChoiceBuilder("Choose and discard 1 attachment from defending character", game, player)
                        .Question("Which defending character must discard an attachment?");

                var attachedDefenders = 0;
                foreach (var defender in enemyAttack.Defenders.OfType<IAttachmentHostInPlay>())
                {
                    var controller = game.GetController(defender.Card.Id);
                    if (controller == null)
                        continue;

                    var attachments = defender.Attachments.OfType<IAttachableInPlay>().Where(x => (x.Card is IObjectiveCard || x.Card is IPlayerCard) && (x.AttachedTo != null)).ToList();

                    if (attachments.Count == 0)
                        continue;

                    attachedDefenders++;

                    builder.Answer(string.Format("{0} (controlled by {1}", defender.Title, controller.Name), defender)
                        .Question(string.Format("Which attachment will be discarded from '{0}'?", defender.Title))
                            .Answers(attachments, item => item.Title, (source, handle, attachment) => DiscardOneAttachmentFromDefendingCharacter(game, handle, player, attachment));
                }

                if (attachedDefenders == 0)
                    return base.GetHandle(game);

                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
