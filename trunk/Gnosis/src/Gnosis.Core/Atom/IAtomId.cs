﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomId
        : IAtomCommon
    {
        Uri Value { get; }
    }
}
