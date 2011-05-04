using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        IEnumerable<IFeedCategory> Categories { get; }
        IEnumerable<IFeedLink> Links { get; }
        IEnumerable<IFeedMetadata> Metadata { get; }
        IEnumerable<IFeedItem> Items { get; }

        void AddCategory(IFeedCategory category);
        void RemoveCategory(IFeedCategory category);

        void AddLink(IFeedLink link);
        void RemoveLink(IFeedLink link);

        void AddMetadata(IFeedMetadata metadata);
        void RemoveMetadata(IFeedMetadata metadata);

        void AddItem(IFeedItem item);
        void RemoveItem(IFeedItem item);
    }
}
