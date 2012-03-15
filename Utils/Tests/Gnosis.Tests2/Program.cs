using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gnosis Repository Test");

            try
            {
                var cache = new Cache();
                var repository = new Repository(cache);
                repository.Initialize();

                //var artist = new Artist { Type = ArtistType.Group, Name = "Tool", Year = 1991 };
                //var album = new Work { Type = WorkType.Album, Artist = artist, Name = "Aenima", Year = 1996 };
                //var track1 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "Stinkfist", Number = 1 };
                //var track2 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "Eulogy", Number = 2 };
                //var track3 = new Work { Type = WorkType.Track, Artist = artist, Parent = album, Name = "H.", Number = 3 };
                //artist.AddWork(album);
                //album.AddChild(track1);
                //album.AddChild(track2);
                //album.AddChild(track3);

                //repository.Save(new List<Entity> { artist });

                //Console.WriteLine("Artist Id: {0}", cache.GetId(artist));
                //Console.WriteLine("Album Id: {0}", cache.GetId(album));
                //Console.WriteLine("Track #1 Id: {0}", cache.GetId(track1));
                //Console.WriteLine("Track #2 Id: {0}", cache.GetId(track2));
                //Console.WriteLine("Track #3 Id: {0}", cache.GetId(track3));

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
    }
}
