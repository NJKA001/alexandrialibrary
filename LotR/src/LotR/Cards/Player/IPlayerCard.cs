using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Cards.Player
{
    public interface IPlayerCard
        : ICard
    {
        IEnumerable<Sphere> SpheresOfInfluence { get; }
    }
}
