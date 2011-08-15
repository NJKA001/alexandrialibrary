using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.W3c;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeed : IEntity
    {
        Uri Location { get; set; }
        string MediaType { get; set; }
        string Title { get; set; }
        string Authors { get; set; }
        string Contributors { get; set; }
        string Description { get; set; }
        ILanguageTag Language { get; set; }
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
        IEnumerable<IFeedMetadatum> Metadata { get; }
        IEnumerable<IFeedItem> Items { get; }

        IEnumerable<ITag> TitleTags { get; }
        IEnumerable<ITag> AuthorTags { get; }
        IEnumerable<ITag> ContributorTags { get; }
        IEnumerable<ITag> DescriptionTags { get; }

        void AddCategory(Uri scheme, string name, string label);
        void RemoveCategory(IFeedCategory category);

        void AddLink(string relationship, Uri location, string mediaType, uint length, string language);
        void RemoveLink(IFeedLink link);

        void AddMetadatum(string mediaType, Uri scheme, string name, string content);
        void RemoveMetadatum(IFeedMetadatum metadatum);

        void AddItem(IFeedItem item);
        void RemoveItem(IFeedItem item);
    }
}
