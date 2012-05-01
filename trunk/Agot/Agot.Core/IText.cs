﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IText
    {
        string FlavorText { get; }
        IEnumerable<Keyword> Keywords { get; }
        IEnumerable<Crest> Crests { get; }
        IEnumerable<Trait> Traits { get; }
        IEnumerable<IAbility> Abilities { get; }

        sbyte GoldBonus { get; }
        sbyte InfluenceBonus { get; }
        sbyte InitiativeBonus { get; }
        sbyte StrengthBonus { get; }
    }
}
