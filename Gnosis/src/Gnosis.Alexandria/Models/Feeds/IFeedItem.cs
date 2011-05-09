using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Feeds
{
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

        IOrderedSet<IFeedCategory> Categories { get; }
        IOrderedSet<IFeedLink> Links { get; }
        IOrderedSet<IFeedMetadata> Metadata { get; }
    }
}
