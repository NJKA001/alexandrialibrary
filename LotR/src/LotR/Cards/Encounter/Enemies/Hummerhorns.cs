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

            public void AfterEnemyEngages(IEnemyEngaged state)
            {
                state.AddEffect(this);
            }

            public override IEffectOptions GetOptions(IGame game)
            {
                var enemyEngage = game.CurrentPhase.GetEngagedEnemies().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyEngage == null)
                    return base.GetOptions(game);

                var choice = new ChooseHero(Source, enemyEngage.DefendingPlayer, enemyEngage.DefendingPlayer.CardsInPlay.OfType<IHeroInPlay>().ToList());
                return new EffectOptions(choice);
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var heroChoice = options.Choice as IChooseHero;
                if (heroChoice == null)
                    return GetCancelledString();

                var damagable = heroChoice.ChosenHero as IDamagableInPlay;
                if (damagable == null)
                    return GetCancelledString();

                damagable.Damage += 5;

                return ToString();
            }
        }

        private class ShadowDealOneDamageToEachCharacter
            : ShadowEffectBase
        {
            public ShadowDealOneDamageToEachCharacter(Hummerhorns source)
                : base("Deal 1 damage to each character the defending player controls. (2 damage instead if this attack is undefended.)", source)
            {
            }

            public override string Resolve(IGame game, IEffectOptions options)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == Source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    return GetCancelledString();

                var damage = enemyAttack.IsUndefended ? (byte)2 : (byte)1;

                foreach (var damageable in enemyAttack.DefendingPlayer.CardsInPlay.OfType<IDamagableInPlay>())
                {
                    damageable.Damage += damage;
                }

                return ToString();
            }
        }
    }
}
