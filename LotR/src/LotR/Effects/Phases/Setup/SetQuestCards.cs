using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class SetQuestCards
        : FrameworkEffectBase, IDuringSetup
    {
        public SetQuestCards(IGame game)
            : base("Set Quest Cards", "Arrange the quest cards in sequential order, based off the numbers on the back of each card.", game)
        {
        }

        public void DuringSetup(IGame game)
        {
            game.QuestArea.SetQuestCards();
            game.StagingArea.ChangeEncounterDeck(game.QuestArea.ActiveEncounterDeck);
        }
    }
}
