using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
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
