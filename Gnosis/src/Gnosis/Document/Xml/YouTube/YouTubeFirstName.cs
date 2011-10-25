﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeFirstName
        : YouTubeSimpleContentElement, IYouTubeFirstName
    {
        public YouTubeFirstName(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
