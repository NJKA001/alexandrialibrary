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
        private const string location1 = @"Files\03 - Antes De Las Seis.mp3";
        private const string location2 = @"Files\13 - Loca (Featuring Dizzee Rascal).mp3";

        private TagLib.File file;
        private TagLib.Id3v2.Tag tag;

        private void InitializeTag()
        {
            /*
            file = TagLib.File.Create(location2);
            tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;
            tag.Performers = new string[] { "Shakira", "Dizzee Rascal" };
            tag.Subtitle = subtitle;
            tag.TitleSort = titleSort;
            tag.Grouping = grouping;
            tag.OriginalTitle = originalTitle;
            tag.OriginalArtists = new string[] { originalArtists };
            tag.Genres = new string[] { "Latin Pop", "Merengue", "Rock en Español" };
            tag.Composers = new string[] { composers };
            tag.Conductor = conductors;
            tag.AlbumSort = albumSort;
            tag.AlbumSubtitle = albumSubtitle;
            tag.ArtistsSort = artistsSort;
            tag.Moods = new string[] { "Spicy", "Cheerful", "Fin", "Party", "Sensual", "Sexy", "Confident", "Energetic", "Stylish", "Carefree", "Playful" };
            tag.Languages = new string[] { "spa", "eng" };
            tag.RecordingDate = recordingDate;
            tag.ReleaseDate = releaseDate;
            tag.OriginalArtists = new string[] { originalArtists };
            tag.OriginalReleaseDate = originalReleaseDate;
            tag.Duration = duration;
            tag.BeatsPerMinute = beatsPerMinute;
            tag.PlayCount = playCount;
            tag.PlaylistDelay = playlistDelay;
            tag.OriginalFilename = originalFilename;
            tag.EncodingDate = encodingDate;
            tag.TaggingDate = taggingDate;
            tag.Publisher = publisher;
            tag.InternationalStandardRecordingCode = isrc;
            file.Save()
            */
        }

        private void InitializeLyrics()
        {
            //tag.AddUnsynchronizedLyrics(descSpanish, langSpanish, lyricsSpanish);
            //tag.AddUnsynchronizedLyrics(descEnglish, langEnglish, lyricsEnglish);
            //file.Save();
        }

        [SetUp]
        public void TestSetUp()
        {
        }

        [TearDown]
        public void TestTearDown()
        {
        }

        [Test]
        public void ReadTag()
        {
            #region Constants
            const string title = "Loca (Featuring Dizzee Rascal)";
            const string titleSort = "Loca (Featuring Dizzee Rascal)";
            const string subtitle = "Featuring Dizzee Rascal";
            const string grouping = "Song";

            const string album = "Sale El Sol";
            const string albumSort = "Sale El Sol";
            const string albumSubtitle = "The Sun Comes Out";

            const string artists = "Shakira; Dizzee Rascal";
            const string artistsSort = "Shakira; Dizzee Rascal";
            const string albumArtists = "Shakira";
        
            const string composers = "None";
            const string conductors = "None";
            const string genres = "Latin Pop; Merengue; Rock en Español";
            const string moods = "Spicy/Cheerful/Fin/Party/Sensual/Sexy/Confident/Energetic/Stylish/Carefree/Playful";
            const string languages = "spa/eng";

            const string originalTitle = "Loca Con Su Tigre";
            const string originalArtists = "El Cata";
            DateTime originalReleaseDate = new DateTime(2009, 10, 20); 

            DateTime releaseDate = new DateTime(2010, 10, 15);
            DateTime recordingDate = new DateTime(2010, 1, 1);

            const uint track = 13;
            const uint trackCount = 15;
            const uint disc = 1;
            const uint discCount = 1;

            TimeSpan duration = new TimeSpan(0, 0, 193);
            const uint beatsPerMinute = 128;

            ulong playCount = 394;
            TimeSpan playlistDelay = new TimeSpan(0, 0, 2);

            const string originalFilename = "13 - Loca (Featuring Dizzee Rascal).mp3";
            DateTime encodingDate = new DateTime(2011, 6, 14);
            DateTime taggingDate = new DateTime(2011, 6, 16);

            const string publisher = "Epic Records";
            const string isrc = "8869 777433 2";
            #endregion

            file = TagLib.File.Create(location2);
            tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;

            Assert.IsTrue(System.IO.File.Exists(location2));
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

            Assert.AreEqual(recordingDate, tag.RecordingDate);
            Assert.AreEqual(releaseDate, tag.ReleaseDate);

            Assert.AreEqual(originalTitle, tag.OriginalTitle);
            Assert.AreEqual(originalArtists, tag.JoinedOriginalArtists);
            Assert.AreEqual(originalReleaseDate, tag.OriginalReleaseDate);

            Assert.AreEqual(track, tag.Track);
            Assert.AreEqual(trackCount, tag.TrackCount);
            Assert.AreEqual(disc, tag.Disc);
            Assert.AreEqual(discCount, tag.DiscCount);

            Assert.AreEqual(duration, tag.Duration);
            Assert.AreEqual(beatsPerMinute, tag.BeatsPerMinute);

            Assert.AreEqual(playCount, tag.PlayCount);
            Assert.AreEqual(playlistDelay, tag.PlaylistDelay);

            Assert.AreEqual(originalFilename, tag.OriginalFilename);
            Assert.AreEqual(encodingDate, tag.EncodingDate);
            Assert.AreEqual(taggingDate, tag.TaggingDate);

            Assert.AreEqual(publisher, tag.Publisher);
            Assert.AreEqual(isrc, tag.InternationalStandardRecordingCode);
        }

        [Test]
        public void ReadLyrics()
        {
            file = TagLib.File.Create(location1);
            tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;
            Assert.IsTrue(System.IO.File.Exists(location1));
            Assert.IsNotNull(file);
            Assert.IsNotNull(tag);

            const string langEnglish = "eng";
            const string descEnglish = "After 6 PM";

            #region English Lyrics
            const string lyricsEnglish =
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

            const string langSpanish = "spa";
            const string descSpanish = "Antes De Las Seis";
            
            #region Spanish Lyrics
            const string lyricsSpanish = 
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

            Assert.AreEqual(2, tag.GetUnsynchronizedLyrics().Count());
            var frameSpanish = tag.GetUnsynchronizedLyrics(descSpanish, langSpanish);
            Assert.IsNotNull(frameSpanish);
            Assert.AreEqual(descSpanish, frameSpanish.Description);
            Assert.AreEqual(langSpanish, frameSpanish.Language);
            Assert.AreEqual(lyricsSpanish, frameSpanish.Text);
            var frameEnglish = tag.GetUnsynchronizedLyrics(descEnglish, langEnglish);
            Assert.IsNotNull(frameEnglish);
            Assert.AreEqual(descEnglish, frameEnglish.Description);
            Assert.AreEqual(langEnglish, frameEnglish.Language);
            Assert.AreEqual(lyricsEnglish, frameEnglish.Text);
        }
    }
}
