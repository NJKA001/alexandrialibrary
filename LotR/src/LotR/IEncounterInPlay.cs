using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IEncounterInPlay
        : ICardInPlay
    {
        new IEncounterCard Card { get; }
        byte Threat { get; }
    }
}
