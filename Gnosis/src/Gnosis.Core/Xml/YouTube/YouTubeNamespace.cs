using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public class YouTubeNamespace
        : Namespace
    {
        public YouTubeNamespace()
            : base("yt", new Uri("http://gdata.youtube.com/schemas/2007"))
        {
            AddElementConstructor("age", (parent, name) => new YouTubeAge(parent, name));
            AddElementConstructor("books", (parent, name) => new YouTubeBooks(parent, name));
            AddElementConstructor("channelStatistics", (parent, name) => new YouTubeChannelStatistics(parent, name));
            AddElementConstructor("company", (parent, name) => new YouTubeCompany(parent, name));
            AddElementConstructor("countHint", (parent, name) => new YouTubeCountHint(parent, name));
            AddElementConstructor("duration", (parent, name) => new YouTubeDuration(parent, name));
            AddElementConstructor("description", (parent, name) => new YouTubeDescription(parent, name));
            AddElementConstructor("playlistId", (parent, name) => new YouTubePlaylistId(parent, name));
        }
    }
}
