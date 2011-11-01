using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeStatistics
        : Element, IYouTubeStatistics
    {
        public YouTubeStatistics(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        #region IYouTubeStatistics Members

        public int ViewCount
        {
            get { return GetAttributeInt32("viewCount"); }
        }

        public int VideoWatchCount
        {
            get { return GetAttributeInt32("videoWatchCount"); }
        }

        public int SubscriberCount
        {
            get { return GetAttributeInt32("subscriberCount"); }
        }

        public DateTime LastWebAccess
        {
            get { return GetAttributeDateTime("lastWebAccess", DateTime.MinValue); }
        }

        public int FavoriteCount
        {
            get { return GetAttributeInt32("favoriteCount"); }
        }

        public int TotalUploadViews
        {
            get { return GetAttributeInt32("totalUploadView"); }
        }

        #endregion
    }
}
