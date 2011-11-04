using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria.Controllers
{
    public interface ISourceController : IOldRepository<ISource>
    {
        ISource GetPlaylistItem(ISource parent, IOldTrack track);
        void LoadDirectories(ISource source);
        void LoadPodcast(ISource source, DependencyObject handle);
        void LoadSpider(ISource source, DependencyObject handle);
        void LoadDevices(ISource source, DependencyObject handle);
        void LoadYouTubeUser(ISource source, DependencyObject handle);
    }
}
