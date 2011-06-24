using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Feeds
{
    public class FeedOutline
        : OutlineBase<IFeed>, IFeedOutline
    {
        public FeedOutline()
        {
        }

        protected override void InitializeProperties(IDataRecord record)
        {
            Location = record.GetUri("Location");
            Title = record.GetString("Title");
            Authors = record.GetString("Authors");
            Description = record.GetString("Description");
            ImagePath = record.GetUri("ImagePath");
            IconPath = record.GetUri("IconPath");
        }

        #region IFeedOutline Members

        public Uri Location { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public Uri ImagePath { get; set; }
        public Uri IconPath { get; set; }

        #endregion
    }
}
