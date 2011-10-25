using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public class YouTubeHobbies
        : YouTubeSimpleContentElement, IYouTubeHobbies
    {
        public YouTubeHobbies(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
