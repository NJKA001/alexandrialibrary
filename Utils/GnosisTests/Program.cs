using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GnosisTests.Entities;
using GnosisTests.Values;

namespace GnosisTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gnosis Tests");

            const int TOTAL = 1000000;
            //1 Million Tracks = 200 MB

            var start = DateTime.Now;
            var artistById = new Dictionary<uint, ArtistEntity>();
            var artistByName = new Dictionary<string, ArtistEntity>();
            var trackById = new Dictionary<uint, TrackEntity>();
            var trackByName = new Dictionary<string, TrackEntity>();

            for (uint i = 1; i <= TOTAL; i++)
            {
                var track = new TrackEntity() { Id = i, Album = 100, Artist = 200, Disc = 1, Number = 2, Duration = 300, Name = Guid.NewGuid().ToString().Replace('-', ' ') };
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
            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
        }
    }
}
