using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IPlaylist
        : IApplication
    {
        string Title { get; }
        DateTime Created { get; }
        Uri Creator { get; }
        string CreatorName { get; }

        Uri Thumbnail { get; }
    }
}
