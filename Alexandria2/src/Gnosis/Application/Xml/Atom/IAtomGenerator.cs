﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public interface IAtomGenerator
        : IAtomCommon
    {
        string GeneratorName { get; }

        Uri Uri { get; }
        string Version { get; }
    }
}
