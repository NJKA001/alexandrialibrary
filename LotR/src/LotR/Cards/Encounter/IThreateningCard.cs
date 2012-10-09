using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Encounter
{
    public interface IThreateningCard
        : IEncounterCard
    {
        byte Threat { get; }
    }
}
