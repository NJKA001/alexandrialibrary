using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Any;

namespace LotR.Cards.Encounter
{
    public interface IThreateningCard
        : IEncounterCard
    {
        byte PrintedThreat { get; }

        void DetermineThreat(IDetermineThreat state);
    }
}
