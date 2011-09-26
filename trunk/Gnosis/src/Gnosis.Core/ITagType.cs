﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagType
    {
        int Id { get; }
        string Name { get; }
        Uri Scheme { get; }
    }
}