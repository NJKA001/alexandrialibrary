using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICard
    {
        string Title { get; }
        string SetName { get; }
        uint SetNumber { get; }
        ICardText Text { get; }
        object Image { get; }

        bool HasTrait(Trait trait);
        bool IsUnique { get; }
    }
}
