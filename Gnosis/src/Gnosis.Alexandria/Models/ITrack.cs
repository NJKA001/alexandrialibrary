using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gnosis.Alexandria.Models
{
    public interface ITrack : INotifyPropertyChanged
    {
        Guid Id { get; }
        string Path { get; set; }
        string ImagePath { get; set; }
        ICollection<byte> ImageData { get; set; }
        object ImageSource { get; }
        string Title { get; set; }
        string TitleHash { get; }
        string TitleMetaphone { get; }
        string Artist { get; set; }
        string ArtistHash { get; }
        string ArtistMetaphone { get; }
        string Album { get; set; }
        string AlbumHash { get; }
        string AlbumMetaphone { get; }
        uint TrackNumber { get; set; }
        uint DiscNumber { get; set; }
        string Genre { get; set; }
        DateTime ReleaseDate { get; set; }
        string Country { get; set; }
        string Comment { get; set; }
        string Lyrics { get; set; }
        string Grouping { get; set; }
        IEnumerable<Tuple<TimeSpan, TimeSpan>> Clips { get; }
        string CountryImagePath { get; }
        int ReleaseYear { get; set; }
        bool IsSelected { get; set; }
        bool IsHovered { get; set; }
        string Background { get; }
        string PlaybackStatus { get; set; }
        string DurationLabel { get; set; }
        string ElapsedLabel { get; set; }
        double Elapsed { get; set; }
        bool HasClipAt(TimeSpan elapsed);
        Tuple<TimeSpan, TimeSpan> GetNextClipFrom(TimeSpan elapsed);
        string CachePath { get; set; }
        bool IsCountryBeingEdited { get; set; }
        Visibility CountryDisplayVisibility { get; }
        Visibility CountryEditVisibility { get; }
    }
}
