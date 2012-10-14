using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter.Objectives
{
    public interface IObjectiveCard
        : IEncounterCard,
        IVictoryCard
    {
    }
}
