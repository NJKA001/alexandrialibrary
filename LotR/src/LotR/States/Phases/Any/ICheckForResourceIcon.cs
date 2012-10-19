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
        ICharacterInPlay Target { get; }
        Sphere ResourceIcon { get; }
        
        bool HasResourceIcon { get; set; }
    }
}
