using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Phases.Encounter
{
    public class EngagamentCheckEffect
        : FrameworkEffectBase
    {
        public EngagamentCheckEffect(IGame game, IPlayer player, IEnemyInPlay enemy)
            : base("Engagement Check", string.Format("{0} must check to see if '{1}' engages them", player.Name, enemy.Title), game)
        {
            this.player = player;
            this.enemy = enemy;
        }

        private readonly IPlayer player;
        private readonly IEnemyInPlay enemy;
    }
}
