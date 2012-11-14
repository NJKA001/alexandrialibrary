using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States;

namespace LotR.Effects.Choices
{
    public interface IChooseGaladhrimsGreetingEffect
        : IChoice
    {
        IPlayer ReduceOnePlayersThreatBySix { get; set; }
        bool ReduceEachPlayersThreatByTwo { get; set; }
    }
}
