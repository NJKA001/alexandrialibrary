using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    /// <summary>
    /// YouTube Statistics Element
    /// </summary>
    /// <remarks>http://code.google.com/apis/youtube/2.0/reference.html</remarks>
    public interface IYouTubeStatistics
        : IYouTubeElement
    {
        /// <summary>
        /// When the viewCount attribute refers to a video entry, the attribute specifies the number of times that the video has been viewed. When the viewCount attribute refers to a user profile, the attribute specifies the number of times that the user's profile has been viewed.
        /// </summary>
        int ViewCount { get; }

        /// <summary>
        /// The videoWatchCount attribute specifies the number of videos that a user has watched on YouTube. The videoWatchCount attribute is only specified when the &lt;yt:statistics&gt; tag appears within a user profile entry.
        /// </summary>
        int VideoWatchCount { get; }

        /// <summary>
        /// The subscriberCount attribute specifies the number of YouTube users who have subscribed to a particular user's YouTube channel. The subscriberCount attribute is only specified when the &lt;yt:statistics&gt; tag appears within a user profile entry.
        /// </summary>
        int SubscriberCount	{ get; }
        
        /// <summary>
        /// The lastWebAccess attribute indicates the most recent time that a particular user used YouTube.
        /// </summary>
        DateTime LastWebAccess { get; }

        /// <summary>
        /// The favoriteCount attribute specifies the number of YouTube users who have added a video to their list of favorite videos. The favoriteCount attribute is only specified when the &lt;yt:statistics&gt; tag appears within a video entry.
        /// </summary>
        int FavoriteCount { get; }

        /// <summary>
        /// The totalUploadViews attribute specifies the total number of views for all of a channel's videos. This attribute is only specified when the &lt;yt:statistics&gt; tag appears within a user profile entry.
        /// </summary>
        int TotalUploadViews { get; }
    }
}
