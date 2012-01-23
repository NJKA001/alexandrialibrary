using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class BrowseTaskViewModel
        : TaskViewModel
    {
        public BrowseTaskViewModel(ILogger logger, IBrowseTask task)
            : base(logger, task, "Browse", task.Category.ToString(), GetIcon(task.Category))
        {
        }

        private static object GetIcon(MediaCategory category)
        {
            switch (category)
            {
                case MediaCategory.Album:
                    return "pack://application:,,,/Images/scarab.gif";
                case MediaCategory.Artist:
                    return "pack://application:,,,/Images/crown.png";
                case MediaCategory.Clip:
                    return "pack://application:,,,/Images/eye_of_horus.jpg";
                case MediaCategory.Doc:
                    return "pack://application:,,,/Images/scroll.gif";
                case MediaCategory.Feed:
                    return "pack://application:,,,/Images/ouroboros.jpg";
                case MediaCategory.Pic:
                    return "pack://application:,,,/Images/tablet.gif";
                case MediaCategory.Playlist:
                    return "pack://application:,,,/Images/hawk.gif";
                case MediaCategory.Program:
                    return "pack://application:,,,/Images/abacus.gif";
                case MediaCategory.Track:
                    return "pack://application:,,,/Images/lyre.jpg";
                case MediaCategory.FeedItem:
                case MediaCategory.PlaylistItem:
                case MediaCategory.None:
                default:
                    return "pack://application:,,,/Images/sphinx_circle.png";
            }    
        }
    }
}
