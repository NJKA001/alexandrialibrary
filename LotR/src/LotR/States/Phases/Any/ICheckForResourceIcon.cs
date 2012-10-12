using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;

namespace LotR.States.Phases.Any
{
    public interface ICheckForResourceIcon
        : IState
    {
        ICostlyCard CostlyCard { get; }
        IResourcefulInPlay Target { get; }
        Sphere ResourceIcon { get; }
        
        bool HasResourceIcon { get; set; }
    }
}
