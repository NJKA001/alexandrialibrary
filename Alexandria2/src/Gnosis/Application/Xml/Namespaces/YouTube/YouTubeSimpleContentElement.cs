﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public abstract class YouTubeSimpleContentElement
        : Element, IYouTubeSimpleContentElement
    {
        protected YouTubeSimpleContentElement(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Content
        {
            get { return GetContentString(); }
        }
    }
}
