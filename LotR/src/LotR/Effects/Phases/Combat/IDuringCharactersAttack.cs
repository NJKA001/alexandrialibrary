﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.States.Phases.Combat;

namespace LotR.Effects.Phases.Combat
{
    public interface IDuringCharactersAttack
    {
        void DuringCharactersAttack(ICharactersAttack state);
    }
}
