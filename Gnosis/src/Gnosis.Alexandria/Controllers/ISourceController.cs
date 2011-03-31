using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface ISourceController : IRepository<ISource>
    {
        ISource GetPlaylistItem(ISource parent, ITrack track);
        void LoadDirectories(ISource source);
    }
}
