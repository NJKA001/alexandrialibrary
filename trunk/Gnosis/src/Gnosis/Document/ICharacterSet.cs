﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document
{
    public interface ICharacterSet
    {
        string Name { get; }
        string Description { get; }
        byte[] ByteOrderMark { get; }
    }
}
