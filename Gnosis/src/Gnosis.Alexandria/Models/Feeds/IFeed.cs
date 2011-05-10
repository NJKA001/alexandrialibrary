using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    [Table("Feed")]
    [Index("Feed_TimeStamp_CreatedDate", "TimeStamp_CreatedDate")]
    [UniqueIndex("Feed_Location", "Location")]
    [Index("Feed_Title", "Title")]
    [Index("Feed_Sort", "Authors", "PublishedDate", "Title")]
    public interface IFeed : IEntity
    {
        Uri Location { get; }
        string MediaType { get; set; }
        string Title { get; set; }
        string Authors { get; set; }
        string Contributors { get; set; }
        string Description { get; set; }
        string Language { get; set; }
        Uri OriginalLocation { get; set; }
        string Copyright { get; set; }
        DateTime PublishedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        string Generator { get; set; }
        Uri ImagePath { get; set; }
        Uri IconPath { get; set; }
        string FeedIdentifier { get; set; }

        [OneToMany("FeedCategory")]
        IOrderedSet<IFeedCategory> Categories { get; }

        [OneToMany("FeedLink")]
        [ForeignIndex("FeedLink_Location", "Location")]
        IOrderedSet<IFeedLink> Links { get; }

        [OneToMany("FeedMetadata")]
        IOrderedSet<IFeedMetadata> Metadata { get; }

        [OneToMany("FeedItem")]
        IOrderedSet<IFeedItem> Items { get; }
    }
}
