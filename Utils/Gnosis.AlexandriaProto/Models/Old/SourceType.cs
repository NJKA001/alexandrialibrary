using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public enum SourceType
    {
        None = 0,
        Folder = 1,
        Media = 2,
        Playlist = 3,
        PlaylistItem = 4,
        FileSystem = 5,
        Directory = 6,
        Podcast = 7,
        Spider = 8,
        DeviceCatalog = 9,
        HardDisk = 10,
        OpticalDisc = 11,
        YouTubeUser = 12,
        YouTubeUserFavorites = 13,
        YouTubeUserPlaylists = 14,
        YouTubePlaylist = 15,
        YouTubeVideo = 16
    }
}
