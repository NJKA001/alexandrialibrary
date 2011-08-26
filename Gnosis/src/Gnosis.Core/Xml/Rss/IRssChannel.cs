using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public interface IRssChannel
        : IRssElement
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
        string Rating { get; }
        IRssTextInput TextInput { get; }
        IRssSkipHours SkipHours { get; }
        IRssSkipDays SkipDays { get; }

        IEnumerable<IRssCategory> Categories { get; }
        IEnumerable<IRssItem> Items { get; }
    }
}
