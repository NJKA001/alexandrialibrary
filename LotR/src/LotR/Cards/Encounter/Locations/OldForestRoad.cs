using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter.Locations
{
    public class OldForestRoad
        : LocationCardBase
    {
        public OldForestRoad()
            : base("Old Forest Road", CardSet.Core, 99, EncounterSet.Passage_Through_Mirkwood, 2, 1, 3, 0)
        {
            AddTrait(Trait.Forest);
        }
    }
}
