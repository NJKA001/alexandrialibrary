using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Games.Phases.Any;

namespace LotR.Cards
{
    public interface ICard
        : ICheckForTrait, ISource
    {
        CardSet CardSet { get; }
        uint CardNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool IsUnique { get; }
    }
}
