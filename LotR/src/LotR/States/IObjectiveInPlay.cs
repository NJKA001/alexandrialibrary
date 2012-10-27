using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Objectives;

namespace LotR.States
{
    public interface IObjectiveInPlay
        : ICardInPlay<IObjectiveCard>
    {
        bool HasGuards { get; }
        IEnumerable<IEncounterInPlay> Guards { get; }

        void AddGuard(IEncounterInPlay guard);
        void RemoveGuard(IEncounterInPlay guard);
    }
}
