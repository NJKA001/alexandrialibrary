using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class LookupByLocation :
        LookupBase<IFeed>
    {
        public LookupByLocation(Uri location)
            : base("LookupByLocation", "Feed.Location = @Location", new List<string> { "Location" }, new Dictionary<string, object> { { "@Location", location }})
        {
        }
    }
}
