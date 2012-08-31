using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IStagingArea
    {
        IEnumerable<IEncounterInPlay> Cards { get; }
        void AddToStagingArea(IEncounterInPlay card);
        void RemoveFromStagingArea(IEncounterInPlay card);

        byte GetTotalThreat();
    }
}
