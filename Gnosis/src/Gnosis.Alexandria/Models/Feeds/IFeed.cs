using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    //[Index("Feed_TimeStamp_CreatedDate", "TimeStamp_CreatedDate")]
    //[UniqueIndex("Feed_Location", "Location")]
    //[Index("Feed_Title", "Title")]
    //[Index("Feed_Sort", "Authors", "PublishedDate", "Title")]
    //[DefaultSort("Authors ASC, PublishedDate ASC, Title ASC")]
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

        //[ForeignUniqueIndex("FeedCategory_Parent_Scheme_Name", "Parent", "Scheme", "Name")]
        //[ForeignIndex("FeedCategory_Name", "Name")]
        IOrderedSet<IFeedCategory> Categories { get; }

        //[ForeignUniqueIndex("FeedLink_Parent_Relationship_MediaType_Language", "Parent", "Relationship", "MediaType", "Language")]
        //[ForeignIndex("FeedLink_Location", "Location")]
        IOrderedSet<IFeedLink> Links { get; }

        //[ForeignUniqueIndex("FeedMetadata_Parent_Scheme_Name", "Parent", "Scheme", "Name")]
        //[ForeignIndex("FeedMetadata_Content", "Content")]
        IOrderedSet<IFeedMetadata> Metadata { get; }

        IOrderedSet<IFeedItem> Items { get; }
    }
}
