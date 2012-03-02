using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gnosis Tests");

            //const int TOTAL = 500000; //1000000;
            //1 Million Tracks = 200 MB

            var start = DateTime.Now;
            var artistRepo = new Repository<Artist>(new ArtistSerializer());
            var albumRepo = new Repository<Album>(new AlbumSerializer());
            var trackRepo = new Repository<Track>(new TrackSerializer());

            artistRepo.Initialize();
            albumRepo.Initialize();
            trackRepo.Initialize();

            //trackRepo.Compact();
            //trackRepo.Update(trackRepo.Entities.Where(x => x.Name.StartsWith("a")), "Album", "101");
            //trackRepo.Update(trackRepo.Entities.Where(x => x.Name.StartsWith("1")), "Artist", "202");
            //trackRepo.Delete(trackRepo.Entities.Where(x => x.Name.StartsWith("4")).Select(x => x.Id).ToList());

            //for (uint i = 1; i <= TOTAL; i++)
            //{
            //    var track = new Track() { Id = i, Album = 100, Artist = 200, Disc = 1, Number = 2, Duration = 300, Name = Guid.NewGuid().ToString().Replace('-', ' ') };
            //    trackRepo.Create(track);
            //}

            //for (uint i = 1; i < TOTAL; i++)
            //{
            //    var artist = new Artist() { Id = i, Name = Guid.NewGuid().ToString().Replace('-', ' ') };

            //}

            Console.WriteLine("  Elapsed: {0} (s)", DateTime.Now.Subtract(start).TotalSeconds);

            Console.WriteLine("  Count  : {0}", trackRepo.Entities.Count());
            Console.WriteLine("  Count where Name like 4%: {0}", trackRepo.Entities.Where(x => x.Name.StartsWith("4")).Count());
            Console.WriteLine("  Count where Name like 1% and Artist = 202: {0}", trackRepo.Entities.Where(x => x.Name.StartsWith("1") && x.Artist == 202).Count());

            //var trackByIdStart = DateTime.Now;
            //var trackByIdResult = trackById[1001];
            //if (trackByIdResult != null)
            //    Console.WriteLine("  By Id Elapsed: {0} (ms) [< 150 is ideal]", DateTime.Now.Subtract(trackByIdStart).TotalMilliseconds);
            //else
            //    Console.WriteLine("  Could not lookup track by id!");

            //var trackByNameFastStart = DateTime.Now;
            //var trackByNameFastResult = trackByName[nameToSearch];
            //if (trackByNameFastResult != null)
            //    Console.WriteLine("  By Name (Fast) Elapsed: {0} (ms) [<150 is ideal]", DateTime.Now.Subtract(trackByNameFastStart).TotalMilliseconds);
            //else
            //    Console.WriteLine("  Could not lookup track name (fast)!");

            //var trackByNameSlowStart = DateTime.Now;
            //var trackByNameSlowResult = trackById.Values.Where(x => x.Name == nameToSearch).FirstOrDefault();
            //if (trackByNameSlowResult != null)
            //    Console.WriteLine("  By Name (Slow) Elapsed: {0} (ms) [<150 is ideal]", DateTime.Now.Subtract(trackByNameSlowStart).TotalMilliseconds);
            //else
            //    Console.WriteLine("  Could not lookup track name (slow)!");

            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
        }
    }
}
