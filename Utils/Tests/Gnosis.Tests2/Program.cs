using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    class Program
    {
        private static Cache cache;
        private static Repository repository;

        private static Artist artist;
        private static Work album;
        private static Work track1;
        private static Work track2;
        private static Work track3;

        static void Main(string[] args)
        {
            Console.WriteLine("Gnosis Repository Test");

            try
            {
                cache = new Cache();
                repository = new Repository(cache);
                repository.Initialize();

                //AddAlbum();
                DisplayInfo();

                Console.WriteLine("Artist Count: {0}", cache.Artists.Count());
                Console.WriteLine("Work Count: {0}", cache.Works.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
        }

        private static void AddAlbum()
        {
            artist = new Artist { Type = ArtistType.Group, Name = "Tool", Year = 1991 };
            album = new Work { Type = WorkType.Album, Artist = artist, Name = "Aenima", Year = 1996 };
            track1 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "Stinkfist", Number = 1 };
            track2 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "Eulogy", Number = 2 };
            track2.AddLink(new Link("Album Cover", Relationship.Thumbnail, Source.User_Input, "http://example.com/images/Tool/Aenima.jpg"));
            track2.AddTag(new Tag("Progressive Rock", Category.Genre, Source.Embedded_Metadata));
            track3 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "H.", Number = 3 };
            artist.AddWork(album);
            album.AddChild(track1);
            album.AddChild(track2);
            album.AddChild(track3);

            repository.Save(new List<Entity> { artist });
        }

        private static void DisplayInfo()
        {
            var tool = cache.Artists.Where(x => x.Name == "Tool").FirstOrDefault();
            var aenima = cache.Works.Where(x => x.Name == "Aenima" && x.Artist == tool).FirstOrDefault();
            var stinkfist = cache.Works.Where(x => x.Name == "Stinkfist" && x.Artist == tool).FirstOrDefault();
            var eulogy = cache.Works.Where(x => x.Name == "Eulogy" && x.Artist == tool).FirstOrDefault();
            var h = cache.Works.Where(x => x.Name == "H." && x.Artist == tool).FirstOrDefault();

            Console.WriteLine("Artist Id: {0}", cache.GetArtistId(tool));
            Console.WriteLine("Album Id: {0}", cache.GetWorkId(aenima));
            Console.WriteLine("Track #1 Id: {0}", cache.GetWorkId(stinkfist));
            Console.WriteLine("Track #2 Id: {0}", cache.GetWorkId(eulogy));
            Console.WriteLine("  Track #2 Link Id: {0}", cache.GetWorkLinkId(eulogy.Links.First()));
            Console.WriteLine("  Track #2 Tag Id: {0}", cache.GetWorkTagId(eulogy.Tags.First()));
            Console.WriteLine("Track #3 Id: {0}", cache.GetWorkId(h));
        }
    }
}
