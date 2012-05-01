using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPermanent
        : ICard
    {
        bool IsUnique { get; }
        bool IsLimited { get; }

        House Affiliation { get; }
        byte Cost { get; }

        IEnumerable<IAbility> Abilities { get; }
        IText Text { get; }
    }
}
