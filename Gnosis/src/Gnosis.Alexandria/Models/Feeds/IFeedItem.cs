using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    [Table("FeedItem")]
    [UniqueIndex("FeedItem_Feed_FeedItemIdentifier", "Feed", "FeedItemIdentifier")]
    [Index("FeedItem_Title", "Title")]
    [Index("FeedItem_Authors", "Authors")]
    [Index("FeedItem_Contributors", "Contributors")]
    [Index("FeedItem_PublishedDate", "PublishedDate")]
    [Index("FeedItem_Summary", "Summary")]
    [Index("FeedItem_UpdatedDate", "UpdatedDate")]
    [Index("FeedItem_Sort", "Feed", "Sequence")]
    [DefaultSort("Feed ASC, Sequence ASC")]
    public interface IFeedItem : IEntity
    {
        IFeed Feed { get; }
        int Sequence { get; }
        string Title { get; set; }
        string TitleMediaType { get; set; }
        string Authors { get; set; }
        string Contributors { get; set; }
        DateTime PublishedDate { get; set; }
        string Copyright { get; set; }
        string Summary { get; set; }
        string Content { get; set; }
        string ContentMediaType { get; set; }
        Uri ContentLocation { get; set; }
        DateTime UpdatedDate { get; set; }
        string FeedItemIdentifier { get; set; }

        [OneToMany("FeedItemCategories")]
        IOrderedSet<IFeedCategory> Categories { get; }

        [OneToMany("FeedItemLinks")]
        IOrderedSet<IFeedLink> Links { get; }

        [OneToMany("FeedItemMetadata")]
        IOrderedSet<IFeedMetadata> Metadata { get; }
    }
}
