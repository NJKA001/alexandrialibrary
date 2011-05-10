using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public interface IFeedRepository
    {
        IFeed New(Uri location);

        IFeed GetOne(Guid id);
        IFeed GetOne(Uri location);
        IEnumerable<IFeed> GetAll();
        IEnumerable<IFeed> GetAny(IFeedSearch search);

        void Save(IEnumerable<IFeed> tracks);
    }
}
