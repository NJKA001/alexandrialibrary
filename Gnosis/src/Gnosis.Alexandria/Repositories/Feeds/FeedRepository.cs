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

        protected override IFeed CreateDefault()
        {
            return Create(UriExtensions.EmptyUri);
        }

        protected override IEnumerable<IFeed> CreateItems(IDataReader reader)
        {
            var items = new List<IFeed>();

            var resultCount = 0;
            do
            {
                resultCount++;
                var rowCount = 0;
                while (reader.Read())
                {
                    rowCount++;
                    var table = reader[0].ToString();
                    System.Diagnostics.Debug.WriteLine("table: " + table + " columns: " + reader.FieldCount + " row: " + rowCount);
                }
            }
            while (reader.NextResult());

            return items;
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
            return Select();
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
