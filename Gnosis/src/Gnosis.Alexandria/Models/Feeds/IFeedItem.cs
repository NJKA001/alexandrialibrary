using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public interface IFeedItem : IChild
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

        IEnumerable<IFeedCategory> Categories { get; }
        IEnumerable<IFeedLink> Links { get; }
        IEnumerable<IFeedMetadatum> Metadata { get; }

        IEnumerable<ITag> TitleTags { get; }
        IEnumerable<ITag> AuthorTags { get; }
        IEnumerable<ITag> ContributorTags { get; }
        IEnumerable<ITag> SummaryTags { get; }

        void AddCategory(Uri scheme, string name, string label);
        void RemoveCategory(IFeedCategory category);

        void AddLink(string relationship, Uri location, string mediaType, uint length, string language);
        void RemoveLink(IFeedLink link);

        void AddMetadatum(string mediaType, Uri scheme, string name, string content);
        void RemoveMetadatum(IFeedMetadatum metadatum);
    }
}
