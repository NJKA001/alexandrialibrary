using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Core;

namespace Gnosis.Alexandria.Models
{
    public class Album : MutableDeletable, IAlbum
    {
        public Album()
        {
        }

        private Album(long id)
        {
            Initialize(id);
        }

        private string _title = "Unknown";
        private string _titleHash = "Unknown".AsNameHash();
        private string _abbreviation = string.Empty;
        private IArtist _creator = Artist.Unknown;
        private DateTime _releaseDate = new DateTime(1900, 1, 1);
        private ICountry _releaseCountry = Country.Unknown;
        private string _note = string.Empty;
        private readonly IList<ITrack> _tracks = new List<ITrack>();
        private readonly IList<ITrack> _removedTracks = new List<ITrack>();

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    _titleHash = _title.AsNameHash();
                    IsChanged = true;
                }
            }
        }

        public string TitleHash
        {
            get { return _titleHash; }
        }

        public string Abbreviation
        {
            get { return _abbreviation; }
            set
            {
                if (_abbreviation != value)
                {
                    _abbreviation = value;
                    IsChanged = true;
                }
            }
        }

        public IArtist Creator
        {
            get { return _creator; }
            set
            {
                if (_creator != value)
                {
                    _creator = value;
                    IsChanged = true;
                }
            }
        }

        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                if (_releaseDate != value)
                {
                    _releaseDate = value;
                    IsChanged = true;
                }
            }
        }

        public ICountry ReleaseCountry
        {
            get { return _releaseCountry; }
            set
            {
                if (_releaseCountry != value)
                {
                    _releaseCountry = value;
                    IsChanged = true;
                }
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                if (_note != value)
                {
                    _note = value;
                    IsChanged = true;
                }
            }
        }

        public IEnumerable<ITrack> Tracks
        {
            get { return _tracks; }
        }

        public void AddTrack(ITrack track)
        {
            _tracks.Add(track);
        }

        public void RemoveTrack(ITrack track)
        {
            if (_tracks.Contains(track))
            {
                _removedTracks.Add(track);
                _tracks.Remove(track);
            }
        }

        public IEnumerable<ITrack> GetRemovedTracks()
        {
            return _removedTracks;
        }
    }
}
