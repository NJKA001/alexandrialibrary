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
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class ForestSpider
        : EnemyCardBase
    {
        public ForestSpider()
            : base("Forest Spider", CardSet.Core, 96, EncounterSet.Passage_Through_Mirkwood, 4, 2, 25, 2, 1, 4, 0)
        {
            AddTrait(Trait.Creature);
            AddTrait(Trait.Spider);

            AddEffect(new ForcedGainsPlusOneAttackAfterEngaging(this));
            AddEffect(new ShadowDefendingPlayerMustDiscardAnAttachment(this));
        }

        private class ForcedGainsPlusOneAttackAfterEngaging
            : ForcedCardEffectBase, IAfterEnemyEngages
        {
            public ForcedGainsPlusOneAttackAfterEngaging(ForestSpider source)
                : base("After Forest Spider engages a player, it gets +1 Attack until the end of the round.", source)
            {
            }

            public void AfterEnemyEngages(IEnemyEngaged state)
            {
                state.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                IEnemyInPlay enemy = null;

                foreach (var player in game.Players)
                {
                    enemy = player.EngagedEnemies.Where(x => x.Card.Id == source.Id).FirstOrDefault();
                    if (enemy != null)
                        break;
                }

                if (enemy == null)
                    { handle.Cancel(GetCancelledString()); return; }

                game.AddEffect(new AttackModifier(game.CurrentPhase.Code, source, enemy, TimeScope.Round, 1));

                handle.Resolve(GetCompletedStatus());
            }
        }

        private class ShadowDefendingPlayerMustDiscardAnAttachment
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerMustDiscardAnAttachment(ForestSpider source)
                : base("Defending player must choose and discard 1 attachment he controls.", source)
            {
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return null;

                var attachments = new Dictionary<Guid, IList<IAttachableCard>>() { { enemyAttack.DefendingPlayer.StateId, new List<IAttachableCard>() } };

                foreach (var attachable in enemyAttack.DefendingPlayer.CardsInPlay.OfType<IAttachableInPlay>())
                {

                    if ((!(attachable.Card is IPlayerCard)) && (!(attachable.Card is IObjectiveCard)))
                        continue;

                    attachments[enemyAttack.DefendingPlayer.StateId].Add(attachable.Card);
                }

                if (attachments[enemyAttack.DefendingPlayer.StateId].Count == 0)
                    return null;

                var choice = new PlayersChooseCards<IAttachableCard>("Defending player must choose 1 attachment he controls", source, new List<IPlayer> { enemyAttack.DefendingPlayer }, 1, attachments);
                return new EffectHandle(this, choice);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var attachmentChoice = handle.Choice as IPlayersChooseCards<IAttachableCard>;
                if (attachmentChoice == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var attachmentToDiscard = attachmentChoice.GetChosenCards(enemyAttack.DefendingPlayer.StateId).FirstOrDefault();
                if (attachmentToDiscard == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var inPlay = enemyAttack.DefendingPlayer.CardsInPlay.OfType<IAttachableInPlay>().Where(x => x.Card.Id == attachmentToDiscard.Id).FirstOrDefault();
                if (inPlay == null)
                    { handle.Cancel(GetCancelledString()); return; }

                enemyAttack.DefendingPlayer.RemoveCardInPlay(inPlay);
                if (attachmentToDiscard is IPlayerCard)
                {
                    enemyAttack.DefendingPlayer.Deck.Discard(new List<IPlayerCard> { attachmentToDiscard as IPlayerCard });
                }
                else if (attachmentToDiscard is IObjectiveCard)
                {
                    game.StagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachmentToDiscard as IEncounterCard });
                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
