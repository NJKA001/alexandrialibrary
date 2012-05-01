using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface ICharacter
        : IPermanent
    {
        byte PrintedStrength { get; }
        bool PrintedMilitaryIcon { get; }
        bool PrintedIntrigueIcon { get; }
        bool PrintedPowerIcon { get; }
    }
}
