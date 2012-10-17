using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Player.Heroes
{
    public class Dunhere
        : HeroCardBase
    {
        public Dunhere()
            : base("Dunhere", CardSet.Core, 9, Sphere.Spirit, 8, 1, 2, 1, 4)
        {
            AddTrait(Trait.Rohan);
            AddTrait(Trait.Warrior);

            AddEffect(new CanAttackEnemiesInTheStagingArea(this));
            AddEffect(new PlusOneAttackWhenAttackingAnEnemyInTheStagingArea(this));
        }

        public class CanAttackEnemiesInTheStagingArea
            : PassiveCharacterAbilityBase, IBeforeChoosingEnemyToAttack
        {
            public CanAttackEnemiesInTheStagingArea(Dunhere source)
                : base("Dunhere can target enemies in the staging area when he attacks alone. When doing so, he gets +1 Attack.", source)
            {
            }

            public void BeforeChoosingEnemyToAttack(IGame game)
            {
                var chooseEnemy = game.GetStates<IChooseEnemyToAttack>().FirstOrDefault();
                if (chooseEnemy == null)
                    return;

                if (chooseEnemy.Attackers.Count() != 1)
                    return;

                var attacker = chooseEnemy.Attackers.FirstOrDefault();
                if (attacker == null)
                    return;

                if (attacker.Card.Id != Source.Id)
                    return;

                game.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var chooseEnemy = game.GetStates<IChooseEnemyToAttack>().FirstOrDefault();
                if (chooseEnemy == null)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();

                foreach (var enemy in stagingArea.CardsInStagingArea.OfType<IEnemyInPlay>())
                {
                    chooseEnemy.AddEnemy(enemy);
                }
            }
        }

        public class PlusOneAttackWhenAttackingAnEnemyInTheStagingArea
            : PassiveCharacterAbilityBase, IDuringDetermineAttack
        {
            public PlusOneAttackWhenAttackingAnEnemyInTheStagingArea(Dunhere source)
                : base("When attacking an enemy in the staging area, Dunhere gets +1 Attack.", source)
            {
            }

            public void DuringDetermineAttack(IDetermineAttack state)
            {
                var determineAttack = state.GetStates<IDetermineAttack>().FirstOrDefault();
                if (determineAttack == null)
                    return;

                if (determineAttack.Attacker.Card.Id != Source.Id)
                    return;

                var game = state.GetStates<IGame>().FirstOrDefault();
                if (game == null)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                var enemy = stagingArea.CardsInStagingArea.OfType<IEnemyInPlay>().Where(x => x.Card.Id == determineAttack.Defender.Card.Id).FirstOrDefault();
                if (enemy == null)
                    return;

                game.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var determineAttack = game.GetStates<IDetermineAttack>().FirstOrDefault();
                if (determineAttack == null)
                    return;

                determineAttack.Attack += 1;
            }
        }
    }
}
