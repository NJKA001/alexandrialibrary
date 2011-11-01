using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public class YouTubeNamespace
        : Namespace
    {
        public YouTubeNamespace()
            : base("yt", new Uri("http://gdata.youtube.com/schemas/2007"))
        {
            AddElementConstructor("abutMe", (parent, name) => new YouTubeAboutMe(parent, name));
            AddElementConstructor("accessControl", (parent, name) => new YouTubeAccessControl(parent, name));
            AddElementConstructor("age", (parent, name) => new YouTubeAge(parent, name));
            AddElementConstructor("aspectRatio", (parent, name) => new YouTubeAspectRatio(parent, name));
            AddElementConstructor("availability", (parent, name) => new YouTubeAvailability(parent, name));
            AddElementConstructor("books", (parent, name) => new YouTubeBooks(parent, name));
            AddElementConstructor("channelStatistics", (parent, name) => new YouTubeChannelStatistics(parent, name));
            AddElementConstructor("company", (parent, name) => new YouTubeCompany(parent, name));
            AddElementConstructor("countHint", (parent, name) => new YouTubeCountHint(parent, name));
            AddElementConstructor("duration", (parent, name) => new YouTubeDuration(parent, name));
            AddElementConstructor("description", (parent, name) => new YouTubeDescription(parent, name));
            AddElementConstructor("episode", (parent, name) => new YouTubeEpisode(parent, name));
            AddElementConstructor("firstName", (parent, name) => new YouTubeFirstName(parent, name));
            AddElementConstructor("firstReleased", (parent, name) => new YouTubeFirstReleased(parent, name));
            AddElementConstructor("gender", (parent, name) => new YouTubeGender(parent, name));
            AddElementConstructor("hobbies", (parent, name) => new YouTubeHobbies(parent, name));
            AddElementConstructor("hometown", (parent, name) => new YouTubeHometown(parent, name));
            AddElementConstructor("incomplete", (parent, name) => new YouTubeIncomplete(parent, name));
            AddElementConstructor("lastName", (parent, name) => new YouTubeLastName(parent, name));
            AddElementConstructor("location", (parent, name) => new YouTubeLocation(parent, name));
            AddElementConstructor("movies", (parent, name) => new YouTubeMovies(parent, name));
            AddElementConstructor("music", (parent, name) => new YouTubeMusic(parent, name));
            AddElementConstructor("noembed", (parent, name) => new YouTubeNoEmbed(parent, name));
            AddElementConstructor("occupation", (parent, name) => new YouTubeOccupation(parent, name));
            AddElementConstructor("playlistId", (parent, name) => new YouTubePlaylistId(parent, name));
            AddElementConstructor("position", (parent, name) => new YouTubePosition(parent, name));
            AddElementConstructor("private", (parent, name) => new YouTubePrivate(parent, name));
            AddElementConstructor("rating", (parent, name) => new YouTubeRating(parent, name));
            AddElementConstructor("recorded", (parent, name) => new YouTubeRecorded(parent, name));
            AddElementConstructor("relationship", (parent, name) => new YouTubeRelationship(parent, name));
            AddElementConstructor("school", (parent, name) => new YouTubeSchool(parent, name));
            AddElementConstructor("spam", (parent, name) => new YouTubeSpam(parent, name));
            AddElementConstructor("state", (parent, name) => new YouTubeState(parent, name));
            AddElementConstructor("statistics", (parent, name) => new YouTubeStatistics(parent, name));
            AddElementConstructor("status", (parent, name) => new YouTubeStatus(parent, name));
            AddElementConstructor("uploaded", (parent, name) => new YouTubeUploaded(parent, name));
            AddElementConstructor("username", (parent, name) => new YouTubeUsername(parent, name));
            AddElementConstructor("videoid", (parent, name) => new YouTubeVideoId(parent, name));
            AddElementConstructor("when", (parent, name) => new YouTubeWhen(parent, name));
        }
    }
}
