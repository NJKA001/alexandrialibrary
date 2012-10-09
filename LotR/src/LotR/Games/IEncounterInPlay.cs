using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter;

namespace LotR.Games
{
    public interface IEncounterInPlay
        : ICardInPlay
    {
        new IEncounterCard Card { get; }
        byte Threat { get; }
    }
}
