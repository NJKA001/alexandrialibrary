using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards
{
    public interface IGameLoader
    {
        IGameState Load(IEnumerable<PlayerInfo> playersInfo, Scenario scenario);
    }
}
