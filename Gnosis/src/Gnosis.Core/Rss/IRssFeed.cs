﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    /// <summary>
    /// Defines an RSS 2.0 Feed based on the unofficial RSS specification
    /// </summary>
    /// <remarks>http://cyber.law.harvard.edu/rss/rss.html</remarks>
    public interface IRssFeed
        : IXmlDocument
    {
        IRssChannel Channel { get; }
        string Version { get; }
        Uri BaseId { get; }
    }
}