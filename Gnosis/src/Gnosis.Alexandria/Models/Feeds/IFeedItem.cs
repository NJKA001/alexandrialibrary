using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    //[UniqueIndex("FeedItem_Parent_FeedItemIdentifier", "Parent", "FeedItemIdentifier")]
    //[Index("FeedItem_Title", "Title")]
    //[Index("FeedItem_Authors", "Authors")]
    //[Index("FeedItem_Contributors", "Contributors")]
    //[Index("FeedItem_PublishedDate", "PublishedDate")]
    //[Index("FeedItem_Summary", "Summary")]
    //[Index("FeedItem_UpdatedDate", "UpdatedDate")]
    //[Index("FeedItem_Sort", "Parent", "Sequence")]
    //[DefaultSort("Parent ASC, Sequence ASC")]
    public interface IFeedItem : IEntity
    {
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

        //[ForeignUniqueIndex("FeedItemCategory_Parent_Scheme_Name", "Parent", "Scheme", "Name")]
        //[ForeignIndex("FeedItemCategory_Name", "Name")]
        IOrderedSet<IFeedCategory> Categories { get; }

        //[ForeignUniqueIndex("FeedItemLink_Parent_Relationship_MediaType_Language", "Parent", "Relationship", "MediaType", "Language")]
        //[ForeignIndex("FeedItemLink_Location", "Location")]
        IOrderedSet<IFeedLink> Links { get; }

        //[ForeignUniqueIndex("FeedItemMetadata_Parent_Scheme_Name", "Parent", "Scheme", "Name")]
        //[ForeignIndex("FeedItemMetadata_Content", "Content")]
        IOrderedSet<IFeedMetadata> Metadata { get; }
    }
}
