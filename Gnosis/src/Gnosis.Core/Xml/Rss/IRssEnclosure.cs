﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssEnclosure
    {
        Uri Url { get; }
        IMediaType Type { get; }
        int Length { get; }
    }
}