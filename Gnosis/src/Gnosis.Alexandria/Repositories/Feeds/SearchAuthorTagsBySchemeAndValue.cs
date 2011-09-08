using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchAuthorTagsBySchemeAndValue
        : ValueSearchBase<IFeed, Models.ITag>
    {
        public SearchAuthorTagsBySchemeAndValue()
            : base("Feed_AuthorTags.Scheme = @Scheme and Feed_AuthorTags.Value like @Value", feed => feed.TitleTags, tag => tag.Scheme, tag => tag.Value)
        {
        }

        public IFilter GetFilter(Uri scheme, string value)
        {
            return GetFilter(new Dictionary<string, object> { { "@Scheme", scheme }, { "@Value", value } });
        }
    }
}
