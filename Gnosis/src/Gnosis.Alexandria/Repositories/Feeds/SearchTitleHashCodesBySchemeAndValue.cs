using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchTitleHashCodesBySchemeAndValue
        : ValueSearchBase<IFeed, IHashCode>
    {
        public SearchTitleHashCodesBySchemeAndValue()
            : base("Feed_TitleHashCodes.Scheme = @Scheme and Feed_TitleHashCodes.Value like @Value", feed => feed.TitleHashCodes, hashCode => hashCode.Scheme, hashCode => hashCode.Value)
        {
        }

        public IFilter GetFilter(Uri scheme, string value)
        {
            return GetFilter(new Dictionary<string, object> {{"@Scheme", scheme}, {"@Value", value} });
        }
    }
}
