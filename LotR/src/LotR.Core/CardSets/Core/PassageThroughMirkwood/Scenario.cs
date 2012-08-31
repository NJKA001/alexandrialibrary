using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core.CardSets.Core.PassageThroughMirkwood
{
    public class Scenario
        : ScenarioBase
    {
        public Scenario()
        {
            AddQuest(new Quest1a());
            AddQuest(new Quest2a());
        }
    }
}
