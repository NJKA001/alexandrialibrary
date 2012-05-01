using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    [Flags]
    public enum House
    {
        None = 0,
        Neutral = -1,
        Baratheon = 1,
        Greyjoy = 2,
        Lannister = 4,
        Martell = 8,
        Stark = 16,
        Targaryen = 32
    }
}
