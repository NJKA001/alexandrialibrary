using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Encounter.Enemies
{
    public class Hummerhorns
        : EnemyCardBase
    {
        public Hummerhorns()
            : base("Hummerhorns", CardSet.Core, 75, EncounterSet.Spiders_of_Mirkwood, 1, 1, 40, 2, 0, 3, 5)
        {
            AddTrait(Trait.Creature);
            AddTrait(Trait.Insect);
        }

        private class ForcedDealsFiveDamageToAHeroAfterEngaging
            : ForcedCardEffectBase, IAfterEnemyEngages
        {
            public ForcedDealsFiveDamageToAHeroAfterEngaging(Hummerhorns source)
                : base("After Hummerhorns engages you, deal 5 damage to a single hero you control.", source)
            {
            }

            public void AfterEnemyEngages(IEnemyEngage state)
            {
                state.AddEffect(this);
            }

            public override IChoice GetChoice(IGameState state)
            {
                var enemyEngage = state.GetStates<IEnemyEngage>().FirstOrDefault();
                if (enemyEngage == null)
                    return null;

                return new ChooseHero(Source, enemyEngage.DefendingPlayer);
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var heroChoice = choice as IChooseHero;
                if (heroChoice == null)
                    return;

                var damagable = heroChoice.ChosenHero as IDamagableInPlay;
                if (damagable == null)
                    return;

                damagable.Damage += 5;
            }
        }

        private class ShadowDealOneDamageToEachCharacter
            : ShadowEffectBase
        {
            public ShadowDealOneDamageToEachCharacter(Hummerhorns source)
                : base("Deal 1 damage to each character the defending player controls. (2 damage instead if this attack is undefended.)", source)
            {
            }

            public override void Resolve(IGameState state, IPayment payment, IChoice choice)
            {
                var enemyAttack = state.GetStates<IEnemyAttack>().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return;

                var playerArea = enemyAttack.DefendingPlayer.GetStates<IPlayerArea>().FirstOrDefault();
                if (playerArea == null)
                    return;

                var damage = enemyAttack.IsUndefended ? (byte)2 : (byte)1;

                foreach (var damageable in playerArea.GetStates<IDamagableInPlay>())
                {
                    damageable.Damage += damage;
                }
            }
        }
    }
}
