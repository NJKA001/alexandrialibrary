using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public interface IDoomedEffect
        : IRevealedEffect
    {
        byte Doomed { get; }
    }
}
