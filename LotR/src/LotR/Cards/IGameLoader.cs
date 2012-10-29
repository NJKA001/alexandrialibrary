using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.States;
using LotR.States.Controllers;

namespace LotR.Cards
{
    public interface IGameLoader
    {
        void Load(IGame game, IEnumerable<PlayerInfo> playersInfo, ScenarioCode scenarioCode);
    }
}
