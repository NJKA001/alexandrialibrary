using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.States;

namespace LotR.Effects.Phases.Setup
{
    public class SetQuestCards
        : FrameworkEffectBase, IDuringSetup
    {
        public SetQuestCards(IGame game)
            : base("Set Quest Cards", game)
        {
        }

        public void DuringSetup(IGame game)
        {
            game.QuestArea.SetQuestCards();
            game.StagingArea.ChangeEncounterDeck(game.QuestArea.ActiveEncounterDeck);
        }
    }
}
