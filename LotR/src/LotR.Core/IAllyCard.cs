using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IAllyCard
        : ICharacterCard
    {
        byte Cost { get; }
        Sphere SpherOfInfluence { get; }
    }
}
