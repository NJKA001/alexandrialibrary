using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public class YouTubeChannelStatistics
        : Element, IYouTubeChannelStatistics
    {
        public YouTubeChannelStatistics(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IYouTubeChannelStatistics Members

        public int CommentCount
        {
            get { return GetAttributeInt32("commentCount"); }
        }

        public int TotalUploadViewCount
        {
            get { return GetAttributeInt32("totalUploadViewCount"); }
        }

        public int VideoCount
        {
            get { return GetAttributeInt32("videoCount"); }
        }

        public int ViewCount
        {
            get { return GetAttributeInt32("viewCount"); }
        }

        #endregion
    }
}
