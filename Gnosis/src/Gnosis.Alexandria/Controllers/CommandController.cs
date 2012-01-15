using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Controllers
{
    public class CommandController
        : ICommandController
    {
        public CommandController(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;

            commands.Add(new CommandViewModel("Search", "Search your catalogs and the internet for media", "pack://application:,,,/Images/Sphinx.png"));
            commands.Add(new CommandViewModel("Catalogs", "Locate, identify, store and synchronize your media", "pack://application:,,,/Images/Pyramid.png"));
            commands.Add(new CommandViewModel("Artists", "The individuals and groups who create and contribute to media", "pack://application:,,,/Images/eye_of_horus_circle.jpg"));
            commands.Add(new CommandViewModel("Albums", "Collections of media that artists have named and released", "pack://application:,,,/Images/scarab.gif"));
            commands.Add(new CommandViewModel("Tracks", "Music, spoken word, sounds and other audio media", "pack://application:,,,/Images/File Audio-01.png"));
            commands.Add(new CommandViewModel("Clips", "Movies, TV shows, music videos and other video media", "pack://application:,,,/Images/File Video-01.png"));
            commands.Add(new CommandViewModel("Docs", "Web Pages, Books, Letters and other document media", "pack://application:,,,/Images/Web HTML-01.png"));
            commands.Add(new CommandViewModel("Pics", "Photographs, paintings, drawings and other image media", "pack://application:,,,/Images/Image JPEG-01.png"));
            commands.Add(new CommandViewModel("Apps", "Applications, utlities, games and other executable media", "pack://application:,,,/Images/abacus.png"));
            commands.Add(new CommandViewModel("Feeds", "RSS, Atom and other syndicated feed media", "pack://application:,,,/Images/feed.png"));
            commands.Add(new CommandViewModel("Playlists", "User defined collections of media", "pack://application:,,,/Images/blank_scroll.png"));
            commands.Add(new CommandViewModel("Users", "User profiles and accounts", "pack://application:,,,/Images/ankh.png"));
            commands.Add(new CommandViewModel("Settings", "Configure system settings", "pack://application:,,,/Images/Gear-01.png"));
        }

        private readonly ILogger logger;
        private readonly ObservableCollection<ICommandViewModel> commands = new ObservableCollection<ICommandViewModel>();

        public IEnumerable<ICommandViewModel> Commands
        {
            get { return commands; }
        }
    }
}
