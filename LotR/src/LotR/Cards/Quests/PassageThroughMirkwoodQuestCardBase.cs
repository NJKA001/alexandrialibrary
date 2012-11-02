using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Combat;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Quests
{
    public abstract class PassageThroughMirkwoodQuestCardBase
        : QuestCardBase
    {
        protected PassageThroughMirkwoodQuestCardBase(string title, uint cardNumber, byte sequence, byte questPoints)
            : base(title, CardSet.Core, cardNumber, ScenarioCode.Passage_Through_Mirkwood, new List<EncounterSet> { EncounterSet.Passage_Through_Mirkwood, EncounterSet.Spiders_of_Mirkwood, EncounterSet.Dol_Guldur_Orcs }, sequence, questPoints, 0)
        {
            AddEffect(new AfterUngoliantsSpawnIsDefeatedAddToVictoryDisplay(this));
        }

        private class AfterUngoliantsSpawnIsDefeatedAddToVictoryDisplay
            : PassiveCardEffectBase, IAfterEnemyDefeated
        {
            public AfterUngoliantsSpawnIsDefeatedAddToVictoryDisplay(PassageThroughMirkwoodQuestCardBase source)
                : base("Adding \"Ungoliant's Spawn\" to the victory display", source)
            {
            }

            public void AfterEnemyDefeated(IEnemyDefeated state)
            {
                if (state.Enemy == null || state.Enemy.Title != "Ungoliant's Spawn")
                    return;

                state.Game.VictoryDisplay.AddCard(state.Enemy.Card);
                state.Game.ResolveEffect(this, EffectOptions.Empty);
            }
        }
    }
}
