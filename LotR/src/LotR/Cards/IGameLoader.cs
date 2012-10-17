using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Cards
{
    public interface IGameLoader
    {
        IGame Load(IEnumerable<PlayerInfo> playersInfo, ScenarioCode scenarioCode);
    }
}
