using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IFeedItemContainerViewModel
    {
        IEnumerable<IFeedItemViewModel> FeedItems { get; }

        void AddFeedItem(IFeedItemViewModel feedItem);
        void RemoveFeedItem(IFeedItemViewModel feedItem);
    }
}
