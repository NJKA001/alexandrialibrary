using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.ViewModels;
using Gnosis.Alexandria.ViewModels.Commands;

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

            //commands.Add(new CommandViewModel("Search", "Search your catalogs and the internet for media", "pack://application:,,,/Images/sphinx_circle.png"));

            commands.Add(new CatalogCommandViewModel());
            commands.Add(new ArtistCommandViewModel());
            commands.Add(new AlbumCommandViewModel());
            commands.Add(new TrackCommandViewModel());
            commands.Add(new ClipCommandViewModel());

            //commands.Add(new CommandViewModel("Catalogs", "Locate, identify, store and synchronize your media", "pack://application:,,,/Images/pyramid_black2.jpg"));
            //commands.Add(new CommandViewModel("Artists", "The individuals and groups who create and contribute to media", "pack://application:,,,/Images/crown.png"));
            //commands.Add(new CommandViewModel("Albums", "Collections of media that artists have named and released", "pack://application:,,,/Images/scarab.gif"));
            //commands.Add(new CommandViewModel("Tracks", "Music, spoken word, sounds and other audio media", "pack://application:,,,/Images/lyre.jpg"));
            //commands.Add(new CommandViewModel("Clips", "Movies, TV shows, music videos and other video media", "pack://application:,,,/Images/eye_of_horus.jpg"));
            //commands.Add(new CommandViewModel("Docs", "Web Pages, Books, Letters and other document media", "pack://application:,,,/Images/scroll.gif"));
            //commands.Add(new CommandViewModel("Pics", "Photographs, paintings, drawings and other image media", "pack://application:,,,/Images/tablet.gif"));
            //commands.Add(new CommandViewModel("Apps", "Applications, utlities, games and other executable media", "pack://application:,,,/Images/abacus.gif"));
            //commands.Add(new CommandViewModel("Feeds", "RSS, Atom and other syndicated feed media", "pack://application:,,,/Images/ouroboros.jpg"));
            //commands.Add(new CommandViewModel("Playlists", "User defined collections of media", "pack://application:,,,/Images/hawk.gif"));
            //commands.Add(new CommandViewModel("Users", "User profiles and accounts", "pack://application:,,,/Images/cat.jpg"));
            //commands.Add(new CommandViewModel("Settings", "Configure system settings", "pack://application:,,,/Images/ankh.png"));
        }

        private readonly ILogger logger;
        private readonly ObservableCollection<ICommandViewModel> commands = new ObservableCollection<ICommandViewModel>();

        public IEnumerable<ICommandViewModel> Commands
        {
            get { return commands; }
        }
    }
}
