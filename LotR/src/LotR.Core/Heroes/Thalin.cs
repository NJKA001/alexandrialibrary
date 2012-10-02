using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Costs;
using LotR.Core.Effects.CharacterAbilities;
using LotR.Core.Payments;
using LotR.Core.Phases.Any;
using LotR.Core.Phases.Quest;

namespace LotR.Core.Heroes
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

                if (step.CardInPlay == null || (!(step.CardInPlay is IEnemyInPlay)))
                    return;

                if (!questPhase.CharactersCommittedToQuest.Any(x => x.Id == Source.Id))
                    return;

                step.AddEffect(this);
            }

            public override void Resolve(IPhaseStep step, IPayment payment)
            {
                var choice = payment as IChooseEnemyPayment;
                if (choice == null)
                    return;

                choice.Enemy.AddDamage(1);
            }

            public override ICost GetCost(IPhaseStep step)
            {
                var revealedStep = step as IEncounterCardRevealedStep;
                if (revealedStep == null)
                    return null;

                return new ChooseRevealedEnemy(Source, revealedStep);
            }
        }

    }
}
