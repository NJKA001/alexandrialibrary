using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using TagLib;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class TrackTests
    {
        //private const string location1 = @"Files\03 - Antes De Las Seis.mp3";
        private const string location = @"Files\13 - Loca (Featuring Dizzee Rascal).mp3";
        private const string joinedPerformers = "Shakira; Dizzee Rascal";
        private const string joinedAlbumArtists = "Shakira";
        private const string joinedGenres = "Latin Music";
        private const string album = "Sale El Sol";
        private const string title = "Loca (Featuring Dizzee Rascal)";
        private const string titleSort = "Loca (Featuring Dizzee Rascal)";
        private const string subtitle = "Featuring Dizzee Rascal";
        private const string grouping = "Song";
        private const uint track = 13;
        private const uint trackCount = 15;
        private const uint disc = 1;
        private const uint discCount = 1;
        private const uint year = 2010;

        private TagLib.File file;
        private TagLib.Id3v2.Tag tag;

        #region Spanish Lyrics
        private const string textSpanish = 
@"No actúes tan extraño
Duro como una roca
Si te mostre pedazos de piel
Que la luz del sol aun no toca
Y tantos lunares que ni yo misma conocia
Te mostre mi fuerza bruta
Mi talon de Aquiles, mi poesia

Que haras solo una historia mas
Que hare si no te vuelvo a ver
Oh, oh

Si desde el dia en que no estas
Vi la noche llegar mucho antes de las seis
Si desde el dia en que no estas
Vi la noche llegar mucho antes de las seis
Mucho antes

No dejes el barco
Tanto antes de que zarpemos
Hacia alguna isla desierta

Y despues, despues veremos
Si me ves desarmada
Porque lanzas tus misiles
Si ya conoces mis puntos cardinales
Los mas sensibles y sutiles

Que haras la vida lo dira
Que hare si no te vuelvo a ver
Oh, oh

Si desde el dia en que no estas
Vi la noche llegar mucho antes de las seis
Si desde el dia en que no estas
Vi la noche llegar mucho antes de las seis

Mucho antes de las seis

Mucho antes";
        #endregion

        #region English Lyrics
        private const string lyricsEnglish =
@"Don't act so weird
Hard as a rock
If I showed you pieces of skin
That even the sunlight doesn't touch
And so many beauty spots that even I didn't know
I showed you my sheer force
Achilles' heel, my poetry

What will you do, just one more story
What will I do if I don't see you again
Oh, oh

If since the day you're not there
I saw the night fall way before 6 p.m.
If since the day you're not there
I saw the night fall way before 6 p.m.
Way before

Don't leave the boat
When we haven't even set sail yet
Toward some desert island

And later, later we'll see
If you see me unarmed
Because you launch your missiles
If you already know my cardinal points
The most sensitive and subtle ones

What will you do, life will tell
What will I do if I don't see you again
Oh, oh

If since the day you're not there
I saw the night fall way before 6 p.m.
If since the day you're not there
I saw the night fall way before 6 p.m.

Way before 6 p.m.

Way before";
        #endregion

        [SetUp]
        public void TestSetUp()
        {
            file = TagLib.File.Create(location);
            tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;
            //tag.Performers = new string[] { "Shakira", "Dizzee Rascal" };
            //tag.Subtitle = subtitle;
            //tag.TitleSort = titleSort;
            //tag.Grouping = grouping;
            //tag.t
            //file.Save();
        }

        [TearDown]
        public void TestTearDown()
        {
        }

        [Test]
        public void ReadTag()
        {
            Assert.IsTrue(System.IO.File.Exists(location));
            Assert.IsNotNull(file);
            Assert.IsNotNull(tag);
            Assert.AreEqual(joinedPerformers, tag.JoinedPerformers);
            Assert.AreEqual(joinedAlbumArtists, tag.JoinedAlbumArtists);
            Assert.AreEqual(joinedGenres, tag.JoinedGenres);
            Assert.AreEqual(album, tag.Album);
            Assert.AreEqual(title, tag.Title);
            Assert.AreEqual(titleSort, tag.TitleSort);
            Assert.AreEqual(subtitle, tag.Subtitle);
            Assert.AreEqual(grouping, tag.Grouping);
            Assert.AreEqual(track, tag.Track);
            Assert.AreEqual(trackCount, tag.TrackCount);
            Assert.AreEqual(disc, tag.Disc);
            Assert.AreEqual(discCount, tag.DiscCount);
            Assert.AreEqual(year, tag.Year);
        }
    }
}
