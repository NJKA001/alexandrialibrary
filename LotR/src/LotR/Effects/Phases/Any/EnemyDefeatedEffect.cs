using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Phases.Combat;

namespace LotR.Effects.Phases.Any
{
    public class EnemyDefeatedEffect
        : FrameworkEffectBase
    {
        public EnemyDefeatedEffect(IGame game, IEnemyInPlay enemy, IEnumerable<IAttackingInPlay> attackers)
            : base("Enemy Defeated", GetDescription(enemy), game)
        {
            this.enemy = enemy;
            this.attackers = attackers;
        }

        private static string GetDescription(IEnemyInPlay enemy)
        {
            return string.Format("{0} has been defeated", enemy.Title);
        }

        private readonly IEnemyInPlay enemy;
        private readonly IEnumerable<IAttackingInPlay> attackers;

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            var defeatedState = new EnemyDefeated(game, enemy, attackers);

            foreach (var card in game.GetCardsInPlayWithEffect<ICardInPlay, IBeforeEnemyDefeated>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IBeforeEnemyDefeated>())
                {
                    effect.BeforeEnemyDefeated(defeatedState);
                }
            }

            foreach (var card in game.GetCardsInPlayWithEffect<ICardInPlay, IDuringEnemyDefeated>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IDuringEnemyDefeated>())
                {
                    effect.DuringEnemyDefeated(defeatedState);
                }
            }

            if (!defeatedState.IsEnemyDefeated)
                { handle.Cancel(GetCancelledString()); return; }

            foreach (var card in game.GetCardsInPlayWithEffect<ICardInPlay, IAfterEnemyDefeated>())
            {
                foreach (var effect in card.BaseCard.Text.Effects.OfType<IAfterEnemyDefeated>())
                {
                    effect.AfterEnemyDefeated(defeatedState);
                }
            }

            var leavingPlayEffect = new CardLeavesPlayEffect(game, enemy);
            game.AddEffect(leavingPlayEffect);
            var leavingPlayHandle = leavingPlayEffect.GetHandle(game);
            game.TriggerEffect(leavingPlayHandle);

            handle.Resolve(GetCompletedStatus());
        }
    }
}
