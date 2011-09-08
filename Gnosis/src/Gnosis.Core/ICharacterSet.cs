﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ICharacterSet
    {
        long Id { get; }
        string Name { get; }
        string Description { get; }
        byte[] ByteOrderMark { get; }
    }
}
