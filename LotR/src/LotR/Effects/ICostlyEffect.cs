using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards;

namespace LotR.Effects
{
    public interface ICostlyEffect
        : IEffect
    {
        Sphere ResourceSphere { get; }
        byte NumberOfResources { get; }
        bool IsVariableCost { get; }
    }
}
