using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface IDoomedEffect
        : ICardEffect
    {
        byte Doomed { get; }
    }
}
