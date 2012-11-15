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

            public override string Resolve(IGame game, IEffectOptions options)
            {
                IEnemyInPlay enemy = null;

                foreach (var player in game.Players)
                {
                    enemy = player.EngagedEnemies.Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                    if (enemy != null)
                        break;
                }

                if (enemy == null)
                    return GetCancelledString();

                game.AddEffect(new AttackModifier(game.CurrentPhase.Code, Source, enemy, TimeScope.Round, 1));

                return ToString();
            }
        }

        private class ShadowDefendingPlayerMustDiscardAnAttachment
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerMustDiscardAnAttachment(ForestSpider source)
                : base("Defending player must choose and discard 1 attachment he controls.", source)
            {
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
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

                var choice = new PlayersChooseCards<IAttachableCard>("Defending player must choose 1 attachment he controls", Source, new List<IPlayer> { enemyAttack.DefendingPlayer }, 1, attachments);
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return GetCancelledString();

                var attachmentChoice = options.Choice as IPlayersChooseCards<IAttachableCard>;
                if (attachmentChoice == null)
                    return GetCancelledString();

                var attachmentToDiscard = attachmentChoice.GetChosenCards(enemyAttack.DefendingPlayer.StateId).FirstOrDefault();
                if (attachmentToDiscard == null)
                    return GetCancelledString();

                var inPlay = enemyAttack.DefendingPlayer.CardsInPlay.OfType<IAttachableInPlay>().Where(x => x.Card.Id == attachmentToDiscard.Id).FirstOrDefault();
                if (inPlay == null)
                    return GetCancelledString();

                enemyAttack.DefendingPlayer.RemoveCardInPlay(inPlay);
                if (attachmentToDiscard is IPlayerCard)
                {
                    enemyAttack.DefendingPlayer.Deck.Discard(new List<IPlayerCard> { attachmentToDiscard as IPlayerCard });
                }
                else if (attachmentToDiscard is IObjectiveCard)
                {
                    game.StagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachmentToDiscard as IEncounterCard });
                }

                return ToString();
            }
        }
    }
}
