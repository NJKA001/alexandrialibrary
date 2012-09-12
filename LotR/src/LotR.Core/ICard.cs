using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICard
    {
        Guid Id { get; }

        string Title { get; }
        string SetName { get; }
        uint SetNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool HasTrait(Traits trait);
        bool IsUnique { get; }
    }
}
