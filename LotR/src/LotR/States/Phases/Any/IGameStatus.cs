using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States.Phases.Any
{
    public interface IGameStatus
        : IState
    {
        bool IsGameRunning { get; }
        bool IsPlayerDefeat { get; set; }
        bool IsPlayerVictory { get; set; }
    }
}
