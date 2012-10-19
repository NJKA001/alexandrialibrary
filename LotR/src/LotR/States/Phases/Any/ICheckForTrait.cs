using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.States.Phases.Any
{
    public interface ICheckForTrait
        : IState
    {
        ICardInPlay Target { get; }
        Trait Trait { get; }

        bool HasTrait { get; set; }
    }
}
