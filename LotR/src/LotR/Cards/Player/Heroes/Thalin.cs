using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Games;
using LotR.Games.Phases;
using LotR.Games.Phases.Any;
using LotR.Games.Phases.Quest;

namespace LotR.Cards.Player.Heroes
{
    public class Thalin
        : HeroCardBase
    {
        public Thalin()
            : base("Thalin", CardSet.Core, 6, Sphere.Tactics, 9, 1, 2, 2, 4)
        {
            AddTrait(Trait.Dwarf);
            AddTrait(Trait.Warrior);

            AddEffect(new DealOneDamageToEachEnemyRevealedWhileThalinIsCommitedToQuest(this));
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
