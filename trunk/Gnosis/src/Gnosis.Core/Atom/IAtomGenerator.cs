﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Atom
{
    public interface IAtomGenerator
        : IAtomCommon
    {
        string Name { get; }

        Uri Uri { get; }
        string Version { get; }
    }
}
