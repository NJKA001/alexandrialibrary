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

        IEnumerable<HouseType> Affiliations { get; }
        byte Cost { get; }

        IText Text { get; }
    }
}
