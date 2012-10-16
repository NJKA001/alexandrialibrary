using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Choices;
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

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                if (stagingArea.CardsInStagingArea.Count() == 0)
                {
                    stagingArea.RevealEncounterCards(1);
                    return;
                }

                foreach (var threatening in stagingArea.CardsInStagingArea.OfType<IThreateningInPlay>())
                {
                    state.AddEffect(new ThreatModifier(state.CurrentPhase, Source, threatening, TimeScope.Phase, 1));
                }
            }
        }

        private class ShadowDiscardOneAttachmentFromDefendingCharacter
            : ShadowEffectBase
        {
            public ShadowDiscardOneAttachmentFromDefendingCharacter(DrivenByShadow source)
                : base("Choose and discard 1 attachment from the defending character. (If this attack is undefended, discard all attachments you control.)", source)
            {
            }

            public override IChoice GetChoice(IGameState state)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().FirstOrDefault();
                if (enemyAttack == null || enemyAttack.IsUndefended)
                    return null;

                var players = new List<IPlayer>();
                var attachments = new Dictionary<Guid, IList<IAttachableCard>>();

                foreach (var defender in enemyAttack.Defenders)
                {
                    var attachmentHost = defender as IAttachmentHostInPlay;
                    if (attachmentHost == null)
                        continue;

                    if (attachmentHost.Attachments.Count() == 0)
                        continue;
                    
                    var controller = attachmentHost.GetController(state);
                    if (controller == null)
                        continue;

                    if (!attachments.ContainsKey(controller.StateId))
                        attachments.Add(controller.StateId, new List<IAttachableCard>());

                    foreach (var attachable in attachmentHost.Attachments)
                    {
                        attachments[controller.StateId].Add(attachable.Card);
                    }
                }

                if (players.Count == 0 || attachments.All(x => x.Value.Count == 0))
                    return null;

                return new PlayersChooseCards<IAttachableCard>("Choose and discard 1 attachment from the defending character.", Source, players, 1, attachments);
            }

            private void DiscardAllAttachmentsControlledByDefendingPlayer(IGameState state, IEnemyAttack attack)
            {
                var playerArea = attack.DefendingPlayer.GetStates<IPlayerArea>().FirstOrDefault();
                if (playerArea == null)
                    return;

                foreach (var attachable in playerArea.CardsInPlay.OfType<IAttachableInPlay>().Where(x => playerArea.IsControlledByPlayer(x) && x.AttachedTo != null))
                {
                    attachable.AttachedTo.RemoveAttachment(attachable);

                    if (attachable.Card is IPlayerCard)
                    {
                        playerArea.Player.Deck.Discard(new List<IPlayerCard> { attachable.Card as IPlayerCard });
                    }
                    else if (attachable.Card is IObjectiveCard)
                    {
                        var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                        if (stagingArea != null)
                        {
                            stagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachable.Card as IObjectiveCard });
                        }
                    }
                }
            }

            private void DiscardOneAttachmentFromDefendingCharacter(IGameState state, IChoice choice, IEnemyAttack enemyAttack)
            {
                var attachmentChoice = choice as IPlayersChooseCards<IAttachableCard>;
                if (attachmentChoice == null)
                    return;

                IPlayer controllingPlayer = null;
                IAttachableCard card = null;
                foreach (var player in attachmentChoice.Players)
                {
                    card = attachmentChoice.GetChosenCards(player.StateId).FirstOrDefault();
                    if (card != null)
                    {
                        controllingPlayer = player;
                        break;
                    }
                }

                if (card == null || controllingPlayer == null)
                    return;

                var playerArea = controllingPlayer.GetStates<IPlayerArea>().FirstOrDefault();
                if (playerArea == null)
                    return;

                var attachable = playerArea.GetCardInPlay(card.Id) as IAttachableInPlay;
                if (attachable == null || attachable.AttachedTo == null || !playerArea.IsControlledByPlayer(attachable))
                    return;

                attachable.AttachedTo.RemoveAttachment(attachable);

                if (attachable.Card is IPlayerCard)
                {
                    playerArea.Player.Deck.Discard(new List<IPlayerCard> { attachable.Card as IPlayerCard });
                }
                else if (attachable.Card is IObjectiveCard)
                {
                    var stagingArea = state.GetStates<IStagingArea>().FirstOrDefault();
                    if (stagingArea != null)
                    {
                        stagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachable.Card as IObjectiveCard });
                    }
                }
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().FirstOrDefault();
                if (enemyAttack == null)
                    return;

                if (enemyAttack.IsUndefended)
                {
                    DiscardAllAttachmentsControlledByDefendingPlayer(state, enemyAttack);
                }
                else
                {
                    DiscardOneAttachmentFromDefendingCharacter(state, choice, enemyAttack);
                }
            }
        }
    }
}
