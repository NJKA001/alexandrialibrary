using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedItem : IChangeableModel
    {
        IFeed Feed { get; }
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

        IEnumerable<IFeedItemCategory> Categories { get; }
        IEnumerable<IFeedItemLink> Links { get; }
        IEnumerable<IFeedItemMetadata> Metadata { get; }

        void AddCategory(IFeedItemCategory category);
        void RemoveCategory(IFeedItemCategory category);

        void AddLink(IFeedItemLink link);
        void RemoveLink(IFeedItemLink link);

        void AddMetadata(IFeedItemMetadata metadata);
        void RemoveMetadata(IFeedItemMetadata metadata);
    }
}
