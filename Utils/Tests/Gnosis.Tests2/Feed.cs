using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Feed
        : Entity
    {
        private readonly ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();

        public IEnumerable<FeedItem> FeedItems { get { return feedItems; } }
    }
}
