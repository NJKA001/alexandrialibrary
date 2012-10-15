using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class EastBightPatrol
        : EnemyCardBase
    {
        public EastBightPatrol()
            : base("East Bight Patrol", CardSet.Core, 97, EncounterSet.Passage_Through_Mirkwood, 1, 3, 5, 3, 1, 2, 0)
        {
            AddTrait(Trait.Goblin);
            AddTrait(Trait.Orc);

            AddEffect(new ShadowEnemyGetsPlusOneAttack(this));
        }

        private class ShadowEnemyGetsPlusOneAttack
            : ShadowEffectBase
        {
            public ShadowEnemyGetsPlusOneAttack(EastBightPatrol source)
                : base("Attacking enemy gets +1 Attack. (If this attack is undefended, also raise your threat by 3.)", source)
            {
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return;

                enemyAttack.Attack += 1;

                if (enemyAttack.IsUndefended)
                {
                    enemyAttack.DefendingPlayer.IncreaseThreat(3);
                }
            }
        }
    }
}
