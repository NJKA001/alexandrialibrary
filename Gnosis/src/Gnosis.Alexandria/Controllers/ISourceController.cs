using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria.Controllers
{
    public interface ISourceController : IRepository<ISource>
    {
        ISource GetPlaylistItem(ISource parent, ITrack track);
        void LoadDirectories(ISource source);
        void LoadPodcast(ISource source, DependencyObject handle);
    }
}
