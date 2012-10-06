using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Costs;
using LotR.Effects;
using LotR.Payments;
using LotR.Phases.Any;
using LotR.Phases.Quest;

namespace LotR.Heroes
{
    public class Thalin
        : HeroCardBase
    {
        public Thalin()
            : base("Thalin", SetNames.Core, 6, Sphere.Tactics, 9, 1, 2, 2, 4)
        {
            Trait(Traits.Dwarf);
            Trait(Traits.Warrior);

            Effect(new DealOneDamageToEachEnemyRevealedWhileThalinIsCommitedToQuest(this));
        }

        public class DealOneDamageToEachEnemyRevealedWhileThalinIsCommitedToQuest
            : PassiveCharacterAbilityBase, IDuringEncounterCardRevealed
        {
            public DealOneDamageToEachEnemyRevealedWhileThalinIsCommitedToQuest(Thalin source)
                : base("While Thalin is committed to a quest, deal 1 damage to each enemy as it is revealed by the encounter deck.", source)
            {
            }

            public void DuringEncounterCardRevealed(IEncounterCardRevealedStep step)
            {
                var questPhase = step.Phase as IQuestPhase;
                if (questPhase == null)
                    return;

                var revealed = step.Phase.Round.Game.StagingArea.RevealedEncounterCard;
                if (revealed == null)
                    return;

                if (!(revealed is IEnemyCard))
                    return;

                if (!questPhase.CharactersCommittedToQuest.Any(x => x.Id == Source.Id))
                    return;

                step.AddEffect(this);
            }

            public override void Resolve(IPhaseStep step, IChoice choice)
            {
                var enemyChoice = choice as IChooseEnemyPayment;
                if (enemyChoice == null)
                    return;

                enemyChoice.Enemy.AddDamage(1);
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var revealedStep = step as IEncounterCardRevealedStep;
                if (revealedStep == null)
                    return null;

                return new EachRevealedEnemy(Source, revealedStep);
            }
        }

    }
}
