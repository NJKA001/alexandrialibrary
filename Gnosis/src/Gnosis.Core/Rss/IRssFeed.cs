using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    /// <summary>
    /// Defines an RSS 2.0 Feed based on the unofficial RSS specification
    /// </summary>
    /// <remarks>http://cyber.law.harvard.edu/rss/rss.html</remarks>
    public interface IRssFeed
    {
        string Title { get; set; }
        Uri Link { get; set; }
        string Description { get; set; }
        ILanguageTag Language { get; set; }
        string Copyright { get; set; }
        string ManagingEditorName { get; set; }
        Uri ManagingEditorEmail { get; set; }
        string WebMasterName { get; set; }
        Uri WebMasterEmail { get; set; }
        DateTime PublicationDate { get; set; }
        DateTime LastBuildDate { get; set; }
        string Generator { get; set; }
        Uri Documents { get; set; }
        string Cloud { get; set; }
        TimeSpan TimeToLive { get; set; }
        IRssImage Image { get; set; }
        IPicsRating Rating { get; set; }
        string TextInput { get; set; }
        IEnumerable<RssHour> SkipHours { get; set; }
        IEnumerable<RssDay> SkipDays { get; set; }

        IEnumerable<IRssCategory> Categories { get; }
        IEnumerable<IRssFeedItem> Items { get; }
    }
}
