using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR
{
    public interface IPlayerCard
        : ICard
    {
        IEnumerable<Sphere> SpheresOfInfluence { get; }
    }
}
