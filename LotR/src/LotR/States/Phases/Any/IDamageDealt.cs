using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Any
{
    public interface IDamageDealt
        : IState
    {
        ICard Source { get; }
        ICardInPlay Target { get; }
        
        byte Damage { get; set; }
        bool IsDamageDealt { get; set; }
    }
}
