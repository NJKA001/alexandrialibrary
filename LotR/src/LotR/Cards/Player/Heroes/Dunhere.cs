using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Games;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;

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

            public void BeforeChoosingEnemyToAttack(IChooseEnemyToAttackStep step)
            {
                if (step.Attackers.Count() != 1)
                    return;

                var attacker = step.Attackers.FirstOrDefault();
                if (attacker == null)
                    return;

                if (attacker.Id != Source.Id)
                    return;

                step.AddEffect(this);
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var chooseEnemyToAttackStep = step as IChooseEnemyToAttackStep;
                if (chooseEnemyToAttackStep == null)
                    return;

                foreach (var enemy in step.Phase.Round.Game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>())
                {
                    chooseEnemyToAttackStep.AddEnemy(enemy);
                }
            }
        }

        public class PlusOneAttackWhenAttackingAnEnemyInTheStagingArea
            : PassiveCharacterAbilityBase, IDetermineAttack
        {
            public PlusOneAttackWhenAttackingAnEnemyInTheStagingArea(Dunhere source)
                : base("When attacking an enemy in the staging area, Dunhere gets +1 Attack.", source)
            {
            }

            public void DetermineAttack(IDetermineAttackStep step)
            {
                if (step.Attackers.Count() != 1)
                    return;

                var attacker = step.Attackers.FirstOrDefault();
                if (attacker == null)
                    return;

                if (attacker.Id != Source.Id)
                    return;

                foreach (var enemy in step.Phase.Round.Game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>())
                {
                    if (enemy.CardId == step.Target.CardId)
                    {
                        step.AddEffect(this);
                        return;
                    }
                }
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var determineAttackStep = step as IDetermineAttackStep;
                if (determineAttackStep == null)
                    return;

                determineAttackStep.Attack += 1;
            }
        }
    }
}
