using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.Controllers
{
    public interface ITaskController
    {
        IEnumerable<ITaskViewModel> Tasks { get; }
        
        void AddTaskViewModel(ITaskViewModel taskViewModel);
        void RemoveTaskViewModel(ITaskViewModel taskViewModel);
        
        CatalogMediaTaskViewModel GetCatalogViewModel(string path);
        PlaylistTaskViewModel GetPlaylistViewModel(IPlaylistViewModel playlistViewModel);
        SearchTaskViewModel GetSearchViewModel(string search);
        MarqueeView GetMarqueeView(MetadataCategory category);
    }
}
