using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeed : IEntity
    {
        Uri Location { get; }
        string MediaType { get; }
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

        IOrderedSet<IFeedCategory> Categories { get; }
        IOrderedSet<IFeedLink> Links { get; }
        IOrderedSet<IFeedMetadata> Metadata { get; }
        IOrderedSet<IFeedItem> Items { get; }
    }
}
