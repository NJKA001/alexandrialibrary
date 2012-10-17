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

            public void AfterEnemyEngages(IEnemyEngage state)
            {
                state.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var enemy = game.GetState<IEnemyInPlay>(Source.Id);
                if (enemy == null)
                    return;

                game.AddEffect(new AttackModifier(game.CurrentPhase, Source, enemy, TimeScope.Round, 1));
            }
        }

        private class ShadowDefendingPlayerMustDiscardAnAttachment
            : ShadowEffectBase
        {
            public ShadowDefendingPlayerMustDiscardAnAttachment(ForestSpider source)
                : base("Defending player must choose and discard 1 attachment he controls.", source)
            {
            }

            public override IChoice GetChoice(IGame game)
            {
                var enemyAttack = game.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return null;

                var playerArea = enemyAttack.DefendingPlayer.GetStates<IPlayerArea>().FirstOrDefault();
                if (playerArea == null)
                    return null;

                var attachments = new Dictionary<Guid, IList<IAttachableCard>>() { { enemyAttack.DefendingPlayer.StateId, new List<IAttachableCard>() } };

                foreach (var attachable in playerArea.GetStates<IAttachableInPlay>())
                {

                    if ((!(attachable.Card is IPlayerCard)) && (!(attachable.Card is IObjectiveCard)))
                        continue;

                    attachments[enemyAttack.DefendingPlayer.StateId].Add(attachable.Card);
                }

                if (attachments[enemyAttack.DefendingPlayer.StateId].Count == 0)
                    return null;

                return new PlayersChooseCards<IAttachableCard>("Defending player must choose 1 attachment he controls", Source, new List<IPlayer> { enemyAttack.DefendingPlayer }, 1, attachments);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var enemyAttack = game.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return;

                var playerArea = enemyAttack.DefendingPlayer.GetStates<IPlayerArea>().FirstOrDefault();
                if (playerArea == null)
                    return;

                var attachmentChoice = choice as IPlayersChooseCards<IAttachableCard>;
                if (attachmentChoice == null)
                    return;

                var attachmentToDiscard = attachmentChoice.GetChosenCards(enemyAttack.DefendingPlayer.StateId).FirstOrDefault();
                if (attachmentToDiscard == null)
                    return;

                var inPlay = playerArea.GetStates<IAttachableInPlay>().Where(x => x.Card.Id == attachmentToDiscard.Id).FirstOrDefault();
                if (inPlay == null)
                    return;

                playerArea.RemoveCard(inPlay);
                if (attachmentToDiscard is IPlayerCard)
                {
                    playerArea.Player.Deck.Discard(new List<IPlayerCard> { attachmentToDiscard as IPlayerCard });
                }
                else if (attachmentToDiscard is IObjectiveCard)
                {
                    var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                    if (stagingArea == null)
                        return;

                    stagingArea.EncounterDeck.Discard(new List<IEncounterCard> { attachmentToDiscard as IEncounterCard });
                }
            }
        }
    }
}
