﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.MediaRss
{
    public interface IMediaAdult
        : IOptionalMediaRssElement
    {
        bool IsAdult { get; }
    }
}