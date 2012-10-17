using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Costs;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Quest;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;
using LotR.States.Phases.Quest;

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

            public void DuringEncounterCardRevealed(IGame game)
            {
                if (game.CurrentPhase != Phase.Quest)
                    return;

                var stagingArea = game.GetStates<IStagingArea>().FirstOrDefault();
                if (stagingArea == null)
                    return;

                var revealed = stagingArea.RevealedEncounterCard;
                if (revealed == null)
                    return;

                if (!(revealed is IEnemyCard))
                    return;

                var committedCharacters = game.GetStates<ICharactersCommittedToQuest>().FirstOrDefault();
                if (committedCharacters == null)
                    return;

                if (!committedCharacters.IsCommittedToQuest(Source.Id))
                    return;

                game.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var enemyChoice = choice as IChooseEnemy;
                if (enemyChoice == null || enemyChoice.Enemy == null)
                    return;

                var damageable = enemyChoice.Enemy as IDamagableInPlay;
                if (damageable == null)
                    return;

                damageable.Damage += 1;
            }

            public override ICost GetCost(IGame game)
            {
                var revealedStep = game.GetStates<IEncounterCardRevealed>().FirstOrDefault();
                if (revealedStep == null)
                    return null;

                return new EachRevealedEnemy(Source, revealedStep);
            }
        }
    }
}
