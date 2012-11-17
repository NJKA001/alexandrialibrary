using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public class CheckForDefeatedEnemiesEffect
        : FrameworkEffectBase
    {
        public CheckForDefeatedEnemiesEffect(IGame game)
            : this(game, Enumerable.Empty<IAttackingInPlay>())
        {
        }

        public CheckForDefeatedEnemiesEffect(IGame game, IEnumerable<IAttackingInPlay> attackers)
            : base("Check For Defeated Enemies", "Compare damage to hit points for all enemies in play", game)
        {
            if (attackers == null)
                throw new ArgumentNullException("attackers");

            this.attackers = attackers;
        }

        private readonly IEnumerable<IAttackingInPlay> attackers;

        public override void Trigger(IGame game, IEffectHandle handle)
        {
            var defeatedEnemies = new List<IEnemyInPlay>();

            foreach (var enemy in game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>())
            {
                if (enemy.Damage >= enemy.Card.PrintedHitPoints)
                {
                    defeatedEnemies.Add(enemy);
                }
            }

            foreach (var player in game.Players)
            {
                foreach (var enemy in player.EngagedEnemies)
                {
                    if (enemy.Damage >= enemy.Card.PrintedHitPoints)
                    {
                        defeatedEnemies.Add(enemy);
                    }
                }
            }

            foreach (var enemy in defeatedEnemies)
            {
                var defeatedEffect = new EnemyDefeatedEffect(game, enemy, attackers);
                var defeatedHandle = defeatedEffect.GetHandle(game);
                game.AddEffect(defeatedEffect);
                game.TriggerEffect(defeatedHandle);
            }

            handle.Resolve(GetCompletedStatus());
        }
    }
}
