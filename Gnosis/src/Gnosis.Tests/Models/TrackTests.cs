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

        private const string title = "Loca (Featuring Dizzee Rascal)";
        private const string titleSort = "Loca (Featuring Dizzee Rascal)";
        private const string subtitle = "Featuring Dizzee Rascal";
        private const string grouping = "Song";

        private const string album = "Sale El Sol";
        private const string albumSort = "Sale El Sol";
        private const string albumSubtitle = "The Sun Comes Out";

        private const string artists = "Shakira; Dizzee Rascal";
        private const string artistsSort = "Shakira; Dizzee Rascal";
        private const string albumArtists = "Shakira";
        
        private const string composers = "None";
        private const string conductors = "None";
        private const string genres = "Latin Pop; Merengue; Rock en Español";
        private const string moods = "Spicy/Cheerful/Fin/Party/Sensual/Sexy/Confident/Energetic/Stylish/Carefree/Playful";
        private const string languages = "spa/eng";

        private const string originalTitle = "Loca Con Su Tigre";
        private const string originalArtists = "El Cata";
        
        

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
            //tag.OriginalTitle = originalTitle;
            //tag.OriginalArtists = new string[] { originalArtists };
            //tag.Genres = new string[] { "Latin Pop", "Merengue", "Rock en Español" };
            //tag.Composers = new string[] { composers };
            //tag.Conductor = conductors;
            //tag.AlbumSort = albumSort;
            //tag.AlbumSubtitle = albumSubtitle;
            //tag.ArtistsSort = artistsSort;
            //tag.Moods = new string[] { "Spicy", "Cheerful", "Fin", "Party", "Sensual", "Sexy", "Confident", "Energetic", "Stylish", "Carefree", "Playful" };
            //tag.Languages = new string[] { "spa", "eng" };
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

            Assert.AreEqual(title, tag.Title);
            Assert.AreEqual(titleSort, tag.TitleSort);
            Assert.AreEqual(grouping, tag.Grouping);
            Assert.AreEqual(subtitle, tag.Subtitle);

            Assert.AreEqual(album, tag.Album);
            Assert.AreEqual(albumSort, tag.AlbumSort);
            Assert.AreEqual(albumSubtitle, tag.AlbumSubtitle);

            Assert.AreEqual(artists, tag.JoinedPerformers);
            Assert.AreEqual(artistsSort, tag.ArtistsSort);
            Assert.AreEqual(albumArtists, tag.JoinedAlbumArtists);

            Assert.AreEqual(composers, tag.JoinedComposers);
            Assert.AreEqual(conductors, tag.Conductor);
            Assert.AreEqual(genres, tag.JoinedGenres);
            Assert.AreEqual(moods, tag.JoinedMoods);
            Assert.AreEqual(languages, tag.JoinedLanguages);

            Assert.AreEqual(originalTitle, tag.OriginalTitle);
            Assert.AreEqual(originalArtists, tag.JoinedOriginalArtists);

            Assert.AreEqual(track, tag.Track);
            Assert.AreEqual(trackCount, tag.TrackCount);
            Assert.AreEqual(disc, tag.Disc);
            Assert.AreEqual(discCount, tag.DiscCount);
            Assert.AreEqual(year, tag.Year);
        }
    }
}
