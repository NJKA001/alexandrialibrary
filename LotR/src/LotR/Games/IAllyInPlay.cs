using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player.Allies;

namespace LotR.Games
{
    public interface IAllyInPlay
        : IExhaustableInPlay
    {
        new IAllyCard Card { get; }
    }
}
