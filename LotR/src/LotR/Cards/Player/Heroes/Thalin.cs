using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects;

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
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null)
                    return;

                var revealed = game.StagingArea.RevealedEncounterCard;
                if (revealed == null)
                    return;

                if (!(revealed is IEnemyInPlay))
                    return;

                if (!questPhase.IsCommittedToQuest(source.Id))
                    return;

                game.AddEffect(this);
            }

            public override void Trigger(IGame game, IEffectHandle handle)
            {
                var questPhase = game.CurrentPhase as IQuestPhase;
                if (questPhase == null || !questPhase.IsCommittedToQuest(CardSource.Id))
                {
                    handle.Cancel(string.Format("This ability only applies while '{0}' is committed to the quest", CardSource.Title));
                    return;
                }

                if (game.StagingArea.RevealedEncounterCard == null)
                {
                    handle.Cancel(string.Format("There is no revealed encounter card for '{0}' to deal damage to", CardSource.Title));
                    return;
                }

                var enemy = game.StagingArea.RevealedEncounterCard as IEnemyInPlay;
                if (enemy == null)
                {
                    handle.Cancel(string.Format("The revealed encounter card, '{0}' is not an enemy", game.StagingArea.RevealedEncounterCard.Title));
                    return;
                }

                enemy.Damage += 1;

                handle.Resolve(string.Format("'{0}' dealt 1 damage to '{1}' as it was revealed from the encounter deck", CardSource.Title, enemy.Title));
            }
        }
    }
}
