using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;
using LotR.Cards.Player;
using LotR.Effects;
using LotR.Effects.Costs;

namespace LotR.States.Phases.Any
{
    public interface ICheckForResourceMatch
        : IState
    {
        ICostlyCard CostlyCard { get; }
        ICardEffect CardEffect { get; }

        bool IsResourceMatch { get; set; }
    }
}
