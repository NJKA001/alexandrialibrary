using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    class Program
    {
        //private static GlobalCache cache;
        //private static GlobalDatabase database;
        //private static GlobalRepository repository;

        private static ICache<Artist> artistCache;
        private static ICache<Work> workCache;
        private static IEntityStore<Artist> artistStore;
        private static IEntityStore<Work> workStore;
        private static IRepository repository;

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
                artistCache = new Cache<Artist>();
                workCache = new Cache<Work>();
                artistStore = new SQLiteArtistDatabase();
                workStore = new SQLiteWorkDatabase(artistCache, workCache);
                repository = new Repository(artistCache, artistStore, workCache, workStore);
                repository.Initialize();

                //cache = new GlobalCache();
                //database = new GlobalDatabase();
                //database.Initialize(cache);

                //repository = new GlobalRepository(cache, database);

                //AddAlbum();
                DisplayInfo();

                Console.WriteLine("Artist Count: {0}", artistCache.Entities.Count());
                Console.WriteLine("Work Count: {0}", workCache.Entities.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
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
            repository.Save(artist.Works);
        }

        private static void DisplayInfo()
        {
            var tool = artistCache.Entities.Where(x => x.Name == "Tool").FirstOrDefault();
            var aenima = workCache.Entities.Where(x => x.Name == "Aenima" && x.Artist == tool).FirstOrDefault();
            var stinkfist = workCache.Entities.Where(x => x.Name == "Stinkfist" && x.Artist == tool).FirstOrDefault();
            var eulogy = workCache.Entities.Where(x => x.Name == "Eulogy" && x.Artist == tool).FirstOrDefault();
            var h = workCache.Entities.Where(x => x.Name == "H." && x.Artist == tool).FirstOrDefault();

            Console.WriteLine("Artist Id: {0}", artistCache.GetId(tool));
            Console.WriteLine("Album Id: {0}", workCache.GetId(aenima));
            Console.WriteLine("Track #1 Id: {0}", workCache.GetId(stinkfist));
            Console.WriteLine("Track #2 Id: {0}", workCache.GetId(eulogy));
            Console.WriteLine("  Track #2 Parent Name: {0}", eulogy.Parent.Name);
            Console.WriteLine("  Track #2 Link Id: {0}", workCache.GetId(eulogy.Links.First()));
            Console.WriteLine("  Track #2 Tag Id: {0}", workCache.GetId(eulogy.Tags.First()));
            Console.WriteLine("Track #3 Id: {0}", workCache.GetId(h));
        }
    }
}
