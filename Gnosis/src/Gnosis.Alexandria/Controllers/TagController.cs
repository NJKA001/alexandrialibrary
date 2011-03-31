using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TagLib;

namespace Gnosis.Alexandria.Controllers
{
    public class TagController : ITagController
    {
        public File GetFile(string path)
        {
            return File.Create(path);
        }

        public Tag GetTag(string path)
        {
            var file = GetFile(path);
            return file.Tag;
        }

        public void LoadPicture(ITrack track)
        {
            var file = GetFile(track.Path);
            if (file != null && file.Tag != null && file.Tag.Pictures.Length > 0)
            {
                track.ImageData = file.Tag.Pictures[0].Data;
            }
        }

        public void SaveTag(ITrack track)
        {
            var file = GetFile(track.Path);
            if (file.Tag != null)
            {
                if (!string.IsNullOrEmpty(track.Title))
                    file.Tag.Title = track.Title;

                if (!string.IsNullOrEmpty(track.Album))
                    file.Tag.Album = track.Album;

                file.Tag.Track = track.TrackNumber;
                file.Tag.Disc = track.DiscNumber;

                if (!string.IsNullOrEmpty(track.Artist))
                    file.Tag.Performers = track.Artist.Split(',', ';');

                if (!string.IsNullOrEmpty(track.Genre))
                    file.Tag.Genres = track.Genre.Split(',', ';');

                file.Tag.Year = Convert.ToUInt32(track.ReleaseYear);

                file.Save();
            }
        }
    }
}
