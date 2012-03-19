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

        private static object GetIcon(MetadataCategory category)
        {
            switch (category)
            {
                case MetadataCategory.Album:
                    return "pack://application:,,,/Images/scarab.gif";
                case MetadataCategory.Artist:
                    return "pack://application:,,,/Images/crown.png";
                case MetadataCategory.Clip:
                    return "pack://application:,,,/Images/eye_of_horus.jpg";
                case MetadataCategory.Doc:
                    return "pack://application:,,,/Images/scroll.gif";
                case MetadataCategory.Feed:
                    return "pack://application:,,,/Images/ouroboros.jpg";
                case MetadataCategory.Pic:
                    return "pack://application:,,,/Images/tablet.gif";
                case MetadataCategory.Playlist:
                    return "pack://application:,,,/Images/hawk.gif";
                case MetadataCategory.Program:
                    return "pack://application:,,,/Images/abacus.gif";
                case MetadataCategory.Track:
                    return "pack://application:,,,/Images/lyre.jpg";
                case MetadataCategory.FeedItem:
                case MetadataCategory.PlaylistItem:
                case MetadataCategory.None:
                default:
                    return "pack://application:,,,/Images/sphinx_circle.png";
            }    
        }
    }
}
