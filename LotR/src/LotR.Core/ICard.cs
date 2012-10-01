using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Core.Phases.Any;

namespace LotR.Core
{
    public interface ICard
        : ICheckForTrait
    {
        Guid Id { get; }

        string Title { get; }
        string SetName { get; }
        uint SetNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool IsUnique { get; }
    }
}
