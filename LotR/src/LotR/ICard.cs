using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Phases.Any;

namespace LotR
{
    public interface ICard
        : ICheckForTrait
    {
        Guid Id { get; }

        string Title { get; }
        CardSet CardSet { get; }
        uint CardNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool IsUnique { get; }
    }
}
