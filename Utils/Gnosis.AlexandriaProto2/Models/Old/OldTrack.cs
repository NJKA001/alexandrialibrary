﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace Gnosis.Alexandria.Models
{
    public class OldTrack : IOldTrack
    {
        public OldTrack() : this(Guid.NewGuid())
        {
        }

        public OldTrack(Guid id)
        {
            this.id = id;
            TitleHash = title.ToAmericanizedString();
            TitleMetaphone = title.ToDoubleMetaphoneString();
            ArtistHash = artist.ToAmericanizedString();
            ArtistMetaphone = artist.ToDoubleMetaphoneString();
            AlbumHash = album.ToAmericanizedString();
            AlbumMetaphone = album.ToDoubleMetaphoneString();
        }

        public const string DEFAULT_TITLE = "Untitled";
        public const string DEFAULT_ARTIST = "Unknown Artist";
        public const string DEFAULT_ALBUM = "Unknown Album";
        public const uint DEFAULT_TRACK = 0;
        public const uint DEFAULT_DISC = 0;
        public const string DEFAULT_GENRE = "Unknown Genre";
        public static readonly DateTime DEFAULT_RELEASE_DATE = new DateTime(2000, 1, 1);
        public const string DEFAULT_COUNTRY = "United States of America";
        public const string COLOR_TRANSPARENT = "#00FFFFFF";
        public const string COLOR_LIGHTBLUE = "#FFADD8E6";

        private readonly Guid id;
        private string path;
        private string imagePath = string.Empty;
        private ICollection<byte> imageData;
        private string title = DEFAULT_TITLE;
        private string artist = DEFAULT_ARTIST;
        private string album = DEFAULT_ALBUM;
        private uint trackNumber = DEFAULT_TRACK;
        private uint discNumber = DEFAULT_DISC;
        private string genre = DEFAULT_GENRE;
        private DateTime releaseDate = DEFAULT_RELEASE_DATE;
        private string country = DEFAULT_COUNTRY;
        private string comment = string.Empty;
        private string lyrics = string.Empty;
        private string grouping = string.Empty;
        private string cachePath = string.Empty;

        private bool isSelected;
        private bool isHovered;
        private bool isCountryBeingEdited;
        private string playbackStatus;
        private string durationLabel;
        private string elapsedLabel;
        private double elapsed;
        private readonly IList<Tuple<TimeSpan, TimeSpan>> clips = new List<Tuple<TimeSpan, TimeSpan>>() { new Tuple<TimeSpan, TimeSpan>(TimeSpan.Zero, TimeSpan.MaxValue) };

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ITrack Members

        public Guid Id
        {
            get { return id; }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ICollection<byte> ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;
                    OnPropertyChanged("ImageData");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public object ImageSource
        {
            get
            {
                return !string.IsNullOrEmpty(imagePath) ? (object)imagePath : imageData;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value && value != null)
                {
                    title = value;
                    OnPropertyChanged("Title");
                    TitleHash = title.ToAmericanizedString();
                    TitleMetaphone = title.ToDoubleMetaphoneString();
                }
            }
        }

        public string TitleHash
        {
            get;
            private set;
        }

        public string TitleMetaphone
        {
            get;
            private set;
        }

        public string Artist
        {
            get
            {
                return artist;
            }
            set
            {
                if (artist != value && value != null)
                {
                    artist = value;
                    OnPropertyChanged("Artist");
                    ArtistHash = artist.ToAmericanizedString();
                    ArtistMetaphone = artist.ToDoubleMetaphoneString();
                }
            }
        }

        public string ArtistHash
        {
            get;
            private set;
        }

        public string ArtistMetaphone
        {
            get;
            private set;
        }

        public string Album
        {
            get
            {
                return album;
            }
            set
            {
                if (album != value && value != null)
                {
                    album = value;
                    OnPropertyChanged("Album");
                    AlbumHash = album.ToAmericanizedString();
                    AlbumMetaphone = album.ToDoubleMetaphoneString();
                }
            }
        }

        public string AlbumHash
        {
            get;
            private set;
        }

        public string AlbumMetaphone
        {
            get;
            private set;
        }

        public uint TrackNumber
        {
            get
            {
                return trackNumber;
            }
            set
            {
                if (trackNumber != value)
                {
                    trackNumber = value;
                    OnPropertyChanged("TrackNumber");
                }
            }
        }

        public uint DiscNumber
        {
            get
            {
                return discNumber;
            }
            set
            {
                if (discNumber != value)
                {
                    discNumber = value;
                    OnPropertyChanged("DiscNumber");
                }
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value && value != null)
                {
                    genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if (releaseDate != value)
                {
                    releaseDate = value;
                    OnPropertyChanged("ReleaseYear");
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                if (country != value && value != null)
                {
                    country = value;
                    OnPropertyChanged("Country");
                    OnPropertyChanged("CountryLargeImagePath");
                    OnPropertyChanged("CountrySmallImagePath");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value && !string.IsNullOrEmpty(value))
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                    SetClips();
                }
            }
        }

        public string Lyrics
        {
            get { return lyrics; }
            set
            {
                if (lyrics != value && !string.IsNullOrEmpty(value))
                {
                    lyrics = value;
                    OnPropertyChanged("Lyrics");
                }
            }
        }

        public string Grouping
        {
            get { return grouping; }
            set
            {
                if (grouping != value && !string.IsNullOrEmpty(value))
                {
                    grouping = value;
                    OnPropertyChanged("Grouping");
                }
            }
        }

        private void SetClips()
        {
            clips.Clear();

            var regex = new Regex("clips=['\"](?<CLIPS>[^\"']+)");
            var match = regex.Match(comment);
            if (match != null)
            {
                var clipsGroup = match.Groups["CLIPS"].Value;
                if (!string.IsNullOrEmpty(clipsGroup))
                {
                    var clipTokens = clipsGroup.Split(' ');
                    foreach (var clipToken in clipTokens)
                    {
                        var partTokens = clipToken.Split('-');
                        var start = TimeSpan.Zero; var startSeconds = 0; var startMinutes = 0;
                        var end = TimeSpan.MaxValue; var endSeconds = 0; var endMinutes = 0;

                        if (partTokens.Length > 0)
                        {
                            if (partTokens[0].Contains(':'))
                            {
                                var subTokens = partTokens[0].Split(':');
                                if (subTokens.Length == 2)
                                {
                                    int.TryParse(subTokens[0], out startMinutes);
                                    int.TryParse(subTokens[1], out startSeconds);
                                }
                            }
                            else int.TryParse(partTokens[0], out startSeconds);
                            
                            start = new TimeSpan(0, startMinutes, startSeconds);

                            if (partTokens.Length > 1)
                            {
                                if (partTokens[1].Contains(':'))
                                {
                                    var subTokens = partTokens[1].Split(':');
                                    if (subTokens.Length == 2)
                                    {
                                        int.TryParse(subTokens[0], out endMinutes);
                                        int.TryParse(subTokens[1], out endSeconds);
                                    }
                                }
                                else int.TryParse(partTokens[1], out endSeconds);

                                end = new TimeSpan(0, endMinutes, endSeconds);
                            }

                            clips.Add(new Tuple<TimeSpan, TimeSpan>(start, end));
                        }
                        else
                        {
                            //Invalid clip
                        }
                    }
                }
            }

            if (clips.Count == 0)
            {
                clips.Add(new Tuple<TimeSpan, TimeSpan>(TimeSpan.Zero, TimeSpan.MaxValue));
            }

            OnPropertyChanged("Clips");
        }

        public IEnumerable<Tuple<TimeSpan, TimeSpan>> Clips
        {
            get { return clips; }
        }

        public bool HasClipAt(TimeSpan elapsed)
        {
            var clip = clips.Where(x => x.Item1 <= elapsed && x.Item2 >= elapsed).FirstOrDefault();

            return clip != null;
        }

        public Tuple<TimeSpan, TimeSpan> GetNextClipFrom(TimeSpan elapsed)
        {
            return clips.Where(x => x.Item1 >= elapsed).FirstOrDefault();
        }

        public string CountryLargeImagePath
        {
            get { return string.Format("pack://application:,,,/Images/Countries/large/{0}.png", Country ?? DEFAULT_COUNTRY); }
        }

        public string CountrySmallImagePath
        {
            get { return string.Format("pack://application:,,,/Images/Countries/small/{0}.png", Country ?? DEFAULT_COUNTRY); }
        }

        public int ReleaseYear
        {
            get { return releaseDate.Year; }
            set
            {
                if (releaseDate.Year != value && value != 0)
                {
                    ReleaseDate = new DateTime(value, 1, 1);
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool IsHovered
        {
            get { return isHovered; }
            set
            {
                if (isHovered != value)
                {
                    isHovered = value;
                    OnPropertyChanged("IsHovered");
                    OnPropertyChanged("Background");
                }
            }
        }

        public string Background
        {
            get { return isHovered ? COLOR_LIGHTBLUE : COLOR_TRANSPARENT; }
        }

        public string PlaybackStatus
        {
            get { return playbackStatus; }
            set
            {
                if (playbackStatus != value)
                {
                    playbackStatus = value;
                    OnPropertyChanged("PlaybackStatus");
                }
            }
        }

        public string DurationLabel
        {
            get { return durationLabel; }
            set
            {
                if (durationLabel != value)
                {
                    durationLabel = value;
                    OnPropertyChanged("DurationLabel");
                }
            }
        }

        public string ElapsedLabel
        {
            get { return elapsedLabel; }
            set
            {
                if (elapsedLabel != value)
                {
                    elapsedLabel = value;
                    OnPropertyChanged("ElapsedLabel");
                }
            }
        }

        public double Elapsed
        {
            get { return elapsed; }
            set
            {
                if (elapsed != value)
                {
                    elapsed = value;
                    OnPropertyChanged("Elapsed");
                }
            }
        }

        public string CachePath
        {
            get { return cachePath; }
            set
            {
                if (cachePath != value)
                {
                    cachePath = value;
                    OnPropertyChanged("CachePath");
                }
            }
        }

        public bool IsCountryBeingEdited
        {
            get { return isCountryBeingEdited; }
            set
            {
                if (isCountryBeingEdited != value)
                {
                    isCountryBeingEdited = value;
                    OnPropertyChanged("IsCountryBeingEdited");
                    OnPropertyChanged("CountryDisplayVisibility");
                    OnPropertyChanged("CountryEditVisibility");
                }
            }
        }

        public System.Windows.Visibility CountryDisplayVisibility
        {
            get { return IsCountryBeingEdited ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible; }
        }

        public System.Windows.Visibility CountryEditVisibility
        {
            get { return IsCountryBeingEdited ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
                return false;

            var track = obj as OldTrack;
            if (object.ReferenceEquals(track, null))
                return false;

            return this == track;
        }

        public override string ToString()
        {
            return string.Format("Track: {0} by {1} from {2}", title, artist, album);
        }

        public static bool operator ==(OldTrack track1, OldTrack track2)
        {
            if (object.ReferenceEquals(track1, null) || object.ReferenceEquals(track2, null))
                return false;

            return track1.id == track2.id;
        }

        public static bool operator !=(OldTrack track1, OldTrack track2)
        {
            if (object.ReferenceEquals(track1, null))
            {
                if (object.ReferenceEquals(track2, null))
                    return false;
                else
                    return true;
            }
            else
            {
                if (object.ReferenceEquals(track2, null))
                    return true;
                else
                    return track1.id != track2.id;
            }
        }
    }
}
