using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Quests
{
    public class PassageThroughMirkwood
        : ScenarioBase
    {
        public PassageThroughMirkwood()
            : base("Passage Through Mirkwood", CardSet.Core)
        {
            AddEncounterSet(EncounterSet.Passage_Through_Mirkwood);
            AddEncounterSet(EncounterSet.Dol_Guldur_Orcs);
            AddEncounterSet(EncounterSet.Spiders_of_Mirkwood);

            AddQuest(new FliesAndSpiders());
            AddQuest(new AForkInTheRoad());
            //AddQuest(new 
        }
    }
}
