using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideoPlayer
    {
        void Initialize(ILogger logger, Func<IVideoHost> getHost);
        void Load(Uri location);
    }
}
