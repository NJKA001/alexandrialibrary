using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public interface IFeedContainerViewModel
    {
        IEnumerable<IFeedViewModel> Feeds { get; }

        void AddFeed(IFeedViewModel feed);
        void RemoveFeed(IFeedViewModel feed);
    }
}
