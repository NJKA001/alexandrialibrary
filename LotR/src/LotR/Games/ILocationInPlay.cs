using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Locations;

namespace LotR.Games
{
    public interface ILocationInPlay
        : IProgressableInPlay
    {
        new ILocationCard Card { get; }
    }
}
