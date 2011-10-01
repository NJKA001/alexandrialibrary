﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITag
    {
        long Id { get; }
        Uri Target { get; }
        IAlgorithm Algorithm { get; }
        Uri Type { get; }
        string Name { get; }
    }
}
