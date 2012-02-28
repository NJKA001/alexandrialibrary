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

            const int TOTAL = 500000; //1000000;
            //1 Million Tracks = 200 MB

            var start = DateTime.Now;
            var artistById = new Dictionary<uint, Artist>();
            var artistByName = new Dictionary<string, Artist>();
            var trackById = new Dictionary<uint, Track>();
            var trackByName = new Dictionary<string, Track>();

            var nameToSearch = string.Empty;

            for (uint i = 1; i <= TOTAL; i++)
            {
                var track = new Track() { Id = i, Album = 100, Artist = 200, Disc = 1, Number = 2, Duration = 300, Name = Guid.NewGuid().ToString().Replace('-', ' ') };
                if (i == TOTAL)
                    nameToSearch = track.Name;
                //Console.WriteLine("{0:0000000} {1}", track.Id, track.Name);
                trackById.Add(track.Id, track);
                trackByName.Add(track.Name, track);
                //var artist = new ArtistEntity() { Id = i, Name = Guid.NewGuid().ToString().Replace('-', ' ') };
                //Console.WriteLine("{0:0000000} {1}", artist.Id, artist.Name);
                //artistById.Add(artist.Id, artist);
                //artistByName.Add(artist.Name, artist);
            }

            Console.WriteLine("  Count  : {0}", TOTAL);

            Console.WriteLine("  Elapsed: {0} (s)", DateTime.Now.Subtract(start).TotalSeconds);

            var trackByIdStart = DateTime.Now;
            var trackByIdResult = trackById[1001];
            if (trackByIdResult != null)
                Console.WriteLine("  By Id Elapsed: {0} (ms) [< 150 is ideal]", DateTime.Now.Subtract(trackByIdStart).TotalMilliseconds);
            else
                Console.WriteLine("  Could not lookup track by id!");

            var trackByNameFastStart = DateTime.Now;
            var trackByNameFastResult = trackByName[nameToSearch];
            if (trackByNameFastResult != null)
                Console.WriteLine("  By Name (Fast) Elapsed: {0} (ms) [<150 is ideal]", DateTime.Now.Subtract(trackByNameFastStart).TotalMilliseconds);
            else
                Console.WriteLine("  Could not lookup track name (fast)!");

            var trackByNameSlowStart = DateTime.Now;
            var trackByNameSlowResult = trackById.Values.Where(x => x.Name == nameToSearch).FirstOrDefault();
            if (trackByNameSlowResult != null)
                Console.WriteLine("  By Name (Slow) Elapsed: {0} (ms) [<150 is ideal]", DateTime.Now.Subtract(trackByNameSlowStart).TotalMilliseconds);
            else
                Console.WriteLine("  Could not lookup track name (slow)!");

            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
        }
    }
}
