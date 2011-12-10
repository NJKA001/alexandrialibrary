using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Video
{
    public class AviVideo
        : VideoBase
    {
        public AviVideo(Uri location)
            : base(location, MediaType.VideoAvi)
        {
        }

        private TagLib.File file;
        private TagLib.Tag riffTag;

        private string GetAlbumName()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists && fileInfo.Directory != null && fileInfo.Directory.Name != null)
                    {
                        return fileInfo.Directory.Name;
                    }
                }
                catch (Exception)
                {
                }
            }

            return "Unknown Album";
        }

        private uint GetAlbumNumber()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists && fileInfo.Directory != null && fileInfo.Directory.Name != null)
                    {
                        var tokens = fileInfo.Directory.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var token in tokens)
                        {
                            var normalized = token.Trim();
                            uint number = 0;
                            if (uint.TryParse(normalized, out number))
                                return number;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return 0;
        }

        private DateTime GetDate()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists)
                    {
                        return fileInfo.CreationTimeUtc;
                    }
                }
                catch (Exception)
                {
                }
            }

            return DateTime.MinValue;
        }

        private string GetClipName()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists && fileInfo.Name != null)
                    {
                        if (fileInfo.Name.Contains('.'))
                        {
                            return fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
                        }
                        else return fileInfo.Name;
                        //var tokens = fileInfo.Name.Split('-');
                        //if (tokens == null || tokens.Length == 0)
                        //    return fileInfo.Name;

                        //return tokens[tokens.Length - 1];
                    }
                }
                catch (Exception)
                {
                }
            }

            return "Unknown Clip";
        }

        private uint GetClipNumber()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists && fileInfo.Name != null)
                    {
                        var tokens = fileInfo.Name.Split('-');
                        foreach (var token in tokens)
                        {
                            var normalized = token.Trim();
                            uint number = 0;
                            if (uint.TryParse(normalized, out number))
                                return number;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return 0;
        }

        public override void Load()
        {
            if (Location.IsFile)
            {
                file = TagLib.File.Create(Location.LocalPath);
                riffTag = file.GetTag(TagLib.TagTypes.RiffInfo);
            }
        }

        public override IEnumerable<ITag> GetTags()
        {
            var tags = new List<ITag>();

            if (riffTag != null)
            {
            }

            return tags;
        }

        public override IArtist GetArtist(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IArtist> artistRepository)
        {
            IArtist artist = null;
            var clip = clipRepository.GetByTarget(Location).FirstOrDefault();
            if (clip != null)
            {
                artist = artistRepository.GetByLocation(clip.Creator);
                if (artist != null)
                    return artist;
            }

            return GnosisArtist.Unknown;
        }

        public override IAlbum GetAlbum(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist)
        {
            IAlbum album = null;
            var clip = clipRepository.GetByTarget(Location).FirstOrDefault();
            if (clip != null)
            {
                album = albumRepository.GetByLocation(clip.Catalog);
                if (album != null)
                    return album;
            }

            var albumName = GetAlbumName();
            album = albumRepository.GetByName(albumName).FirstOrDefault();
            if (album != null)
            {
                return album;
            }

            var catalog = GnosisAlbum.Unknown;
            var albumNumber = GetAlbumNumber();
            var date = GetDate();

            return new GnosisAlbum(albumName, date, albumNumber, artist.Location, artist.Name, catalog.Location, catalog.Name, Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, Guid.Empty.ToUrn(), new byte[0]); 
        }

        public override IClip GetClip(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IArtist artist, IAlbum album)
        {
            var clip = clipRepository.GetByTarget(Location).FirstOrDefault();
            if (clip != null)
            {
                return clip;
            }

            var name = GetClipName();
            var number = GetClipNumber();
            var date = GetDate();
            var duration = file != null && file.Properties != null ? file.Properties.Duration : TimeSpan.FromMinutes(5);
            var height = file != null && file.Properties != null ? (uint)file.Properties.VideoHeight : 480;
            var width = file != null && file.Properties != null ? (uint)file.Properties.VideoWidth : 640;
            var user = securityContext.CurrentUser;

            return new GnosisClip(name, date, number, duration, height, width, artist.Location, artist.Name, album.Location, album.Name, Location, Type, user.Location, user.Name, Guid.Empty.ToUrn(), new byte[0]);
        }
    }
}
