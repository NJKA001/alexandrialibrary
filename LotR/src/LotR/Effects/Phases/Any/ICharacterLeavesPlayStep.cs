using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Player;
using LotR.States;

namespace LotR.Effects.Phases.Any
{
    public interface ICharacterLeavesPlayStep
    {
        ICardInPlay<ICharacterCard> Character { get; }
    }
}
