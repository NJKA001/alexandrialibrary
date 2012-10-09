using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.States
{
    public enum Phase
        : byte
    {
        None = 0,
        Resource = 1,
        Planning = 2,
        Quest = 3,
        Travel = 4,
        Encounter = 5,
        Combat = 6,
        Refresh = 7
    }
}
