using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Objectives;
using LotR.Cards.Player;
using LotR.Effects;

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

            private void DiscardChosenAttachment(IGame game, IEffectHandle handle, IPlayer player, IAttachableInPlay attachment)
            {
                if (attachment.AttachedTo == null)
                {
                    handle.Cancel(string.Format("Could not discard '{1}', it was not attached to anything", attachment.Title));
                    return;
                }

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
                    handle.Cancel(string.Format("'{0}' could not be discarded because it is not a valid attachment controlled by {1}", attachment.Title, player.Name));
                    return;
                }

                handle.Resolve(string.Format("{0} chose to have '{1}' discarded by the shadow effect of '{2}'", player.Name, attachment.Title, CardSource.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return base.GetHandle(game);

                var attachments = enemyAttack.DefendingPlayer.CardsInPlay.OfType<IAttachableInPlay>().Where(x => (x.Card is IPlayerCard || x.Card is IObjectiveCard) && (x.AttachedTo != null)).ToList();
                if (attachments.Count == 0)
                    return base.GetHandle(game);

                var builder =
                    new ChoiceBuilder("Defending player must choose 1 attachment he controls", game, enemyAttack.DefendingPlayer)
                        .Question(string.Format("{0}, which attachment will be discarded?", enemyAttack.DefendingPlayer))
                            .Answers(attachments, item => string.Format("{0} (attached to {1}", item.AttachedTo.Title), (source, handle, attachment) => DiscardChosenAttachment(game, handle, enemyAttack.DefendingPlayer, attachment));
                
                return new EffectHandle(this, builder.ToChoice());
            }
        }
    }
}
