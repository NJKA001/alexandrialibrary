using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter
{
    public interface IEncounterSet
    {
        IEnumerable<IEncounterCard> Cards { get; }
    }
}
