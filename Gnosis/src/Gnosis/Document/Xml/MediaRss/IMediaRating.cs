﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.MediaRss
{
    public interface IMediaRating
        : IOptionalMediaRssElement
    {
        Uri Scheme { get; }
        string Content { get; }
    }
}