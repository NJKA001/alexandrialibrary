using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Effects;
using LotR.Effects.Phases.Any;

namespace LotR.Cards
{
    public interface ICard
        : ISource
    {
        CardSet CardSet { get; }
        uint CardNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool IsUnique { get; }
        bool HasTrait(Trait trait);
    }
}
