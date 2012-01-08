﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Video
{
    public abstract class VideoBase
        : IVideo
    {
        protected VideoBase(Uri location, IMediaType type)
        {
            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IMediaType type;

        protected DateTime GetDate()
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

        protected string GetAlbumName()
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

        protected uint GetAlbumNumber()
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

        protected string GetClipName()
        {
            if (Location.IsFile)
            {
                try
                {
                    var fileInfo = new System.IO.FileInfo(Location.LocalPath);
                    if (fileInfo.Exists && fileInfo.Name != null)
                    {
                        var name = fileInfo.Name;
                        if (name.Contains('.'))
                        {
                            name = name.Substring(0, fileInfo.Name.LastIndexOf('.'));
                        }

                        var tokens = name.Split('-');
                        if (tokens == null || tokens.Length < 2)
                            return name;

                        return tokens[tokens.Length - 1];
                    }
                }
                catch (Exception)
                {
                }
            }

            return "Unknown Clip";
        }

        protected uint GetClipNumber()
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

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public virtual void Load()
        {
        }

        public virtual IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public virtual IArtist GetArtist(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IArtist> artistRepository)
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
        
        public virtual IAlbum GetAlbum(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist)
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
            var summary = string.Empty;
            album = albumRepository.GetByName(albumName).FirstOrDefault();
            if (album != null)
            {
                return album;
            }

            var catalog = GnosisAlbum.Unknown;
            var albumNumber = GetAlbumNumber();
            var date = GetDate();

            return new GnosisAlbum(albumName, summary, date, albumNumber, artist.Location, artist.Name, catalog.Location, catalog.Name, Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, securityContext.CurrentUser.Location, securityContext.CurrentUser.Name, Guid.Empty.ToUrn(), new byte[0]);
        }

        public virtual IClip GetClip(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IArtist artist, IAlbum album)
        {
            var clip = clipRepository.GetByTarget(Location).FirstOrDefault();
            if (clip != null)
            {
                return clip;
            }

            var name = GetClipName();
            var summary = string.Empty;
            var number = GetClipNumber();
            var date = GetDate();
            var duration = TimeSpan.FromMinutes(10); //file != null && file.Properties != null ? file.Properties.Duration : TimeSpan.FromMinutes(5);
            uint height = 480; //file != null && file.Properties != null ? (uint)file.Properties.VideoHeight : 480;
            uint width = 640; //file != null && file.Properties != null ? (uint)file.Properties.VideoWidth : 640;
            var user = securityContext.CurrentUser;

            return new GnosisClip(name, summary, date, number, duration, height, width, artist.Location, artist.Name, album.Location, album.Name, Location, Type, user.Location, user.Name, Guid.Empty.ToUrn(), new byte[0]);
        }
    }
}