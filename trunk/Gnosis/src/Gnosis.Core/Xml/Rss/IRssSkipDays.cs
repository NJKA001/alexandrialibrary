﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssSkipDays
        : IRssElement
    {
        IEnumerable<IRssDay> Days { get; }
    }
}
