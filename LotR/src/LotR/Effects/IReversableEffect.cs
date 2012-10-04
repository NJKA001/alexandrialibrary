﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Effects
{
    public interface IReversableEffect
        : IExecutableEffect
    {
        void Undo();
    }
}
