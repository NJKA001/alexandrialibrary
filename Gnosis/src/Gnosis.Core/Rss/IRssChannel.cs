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
    public interface IRssChannel
    {
        string Title { get; }
        Uri Link { get; }
        string Description { get; }
        ILanguageTag Language { get; }
        string Copyright { get; }
        string ManagingEditor { get; }
        string WebMaster { get; }
        DateTime PubDate { get; }
        DateTime LastBuildDate { get; }
        string Generator { get; }
        Uri Docs { get; }
        IRssCloud Cloud { get; }
        TimeSpan Ttl { get; }
        IRssImage Image { get; }
        IPicsRating Rating { get; }
        IRssTextInput TextInput { get; }
        IEnumerable<RssHour> SkipHours { get; }
        IEnumerable<RssDay> SkipDays { get; }

        IEnumerable<INamespace> Namespaces { get; }
        IEnumerable<IRssCategory> Categories { get; }
        IEnumerable<IRssItem> Items { get; }
        IEnumerable<IRssExtension> Extensions { get; }
    }
}
