using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>, IFeedRepository
    {
        public FeedRepository(IContext context)
            : base(context)
        {
        }

        protected override IFeed Create()
        {
            return Create(UriExtensions.EmptyUri);
        }

        protected override IFeed Create(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected IFeed Create(Uri location)
        {
            return new Feed(Context, location);
        }

        public IFeed New(Uri location)
        {
            throw new NotImplementedException();
        }

        public IFeed GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public IFeed GetOne(Uri location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFeed> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFeed> GetAny(IFeedSearch search)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IFeed> tracks)
        {
            throw new NotImplementedException();
        }
    }
}
