using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;

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

            private IEnumerable<IHeroInPlay> GetHeroes(IPlayer player)
            {
                return player.CardsInPlay.OfType<IHeroInPlay>().ToList();
            }

            private void DealFiveDamageToHero(IGame game, IEffectHandle handle, IPlayer player, IHeroInPlay hero)
            {
                hero.Damage += 5;

                handle.Resolve(string.Format("{0} chose to have '{1}' deal 5 damage to '{2}'", player.Name, CardSource.Title, hero.Title));
            }

            public override IEffectHandle GetHandle(IGame game)
            {
                var enemyEngage = game.CurrentPhase.GetEngagedEnemies().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyEngage == null)
                    return base.GetHandle(game);

                var player = enemyEngage.DefendingPlayer;

                var builder = 
                    new ChoiceBuilder(string.Format("{0} must choose a hero for '{1}' to deal 5 damage to", player.Name, CardSource.Title), game, player)
                        .Question(string.Format("Which hero will '{0}' deal 5 damage to?", CardSource.Title))
                            .LastAnswers(GetHeroes(player), item => item.Title, (source, handle, hero) => DealFiveDamageToHero(game, handle, player, hero));

                var choice = builder.ToChoice();

                return new EffectHandle(this, choice);
            }
        }

        private class ShadowDealOneDamageToEachCharacter
            : ShadowEffectBase
        {
            public ShadowDealOneDamageToEachCharacter(Hummerhorns source)
                : base("Deal 1 damage to each character the defending player controls. (2 damage instead if this attack is undefended.)", source)
            {
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var enemyAttack = game.CurrentPhase.GetEnemyAttacks().Where(x => x.Enemy.Card.Id == source.Id).FirstOrDefault();
                if (enemyAttack == null)
                    { handle.Cancel(GetCancelledString()); return; }

                var damage = enemyAttack.IsUndefended ? (byte)2 : (byte)1;

                foreach (var damageable in enemyAttack.DefendingPlayer.CardsInPlay.OfType<IDamagableInPlay>())
                {
                    damageable.Damage += damage;
                }

                handle.Resolve(GetCompletedStatus());
            }
        }
    }
}
