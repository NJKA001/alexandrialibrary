﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITag
    {
        long Id { get; }
        Uri Target { get; }
        ITagType Type { get; }
        string Name { get; }
        uint Number { get; }
        object Value { get; }
    }
}
