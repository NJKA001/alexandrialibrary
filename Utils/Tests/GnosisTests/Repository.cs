using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

using GnosisTests.Entities;
using GnosisTests.Serialization;

namespace GnosisTests
{
    public class Repository
    {
        private readonly ObservableCollection<Artist> artists = new ObservableCollection<Artist>();
        private readonly ObservableCollection<Album> albums = new ObservableCollection<Album>();
        private readonly ObservableCollection<Track> tracks = new ObservableCollection<Track>();

        private readonly IDictionary<uint, Artist> artistsById = new Dictionary<uint, Artist>();
        private readonly IDictionary<uint, Album> albumsById = new Dictionary<uint, Album>();
        private readonly IDictionary<uint, Track> tracksById = new Dictionary<uint, Track>();

        private readonly ArtistSerializer artistSerializer = new ArtistSerializer();
        private readonly AlbumSerializer albumSerializer = new AlbumSerializer();
        private readonly TrackSerializer trackSerializer = new TrackSerializer();

        private const string createdLogFormat = "{0}-Created.txt";
        private const string deletedLogFormat = "{0}-Deleted.txt";
        private const string updatedLogFormat = "{0}-Updated.txt";

        private void AddArtist(Artist artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            if (artistsById.ContainsKey(artist.Id))
                return;

            artistsById.Add(artist.Id, artist);
            artists.Add(artist);
        }

        private void AddAlbum(Album album)
        {
            if (album == null)
                throw new ArgumentNullException("album");

            if (albumsById.ContainsKey(album.Id))
                return;

            albumsById.Add(album.Id, album);
            albums.Add(album);
        }

        private void AddTrack(Track track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (tracksById.ContainsKey(track.Id))
                return;

            tracksById.Add(track.Id, track);
            tracks.Add(track);
        }

        public IEnumerable<Artist> Artists { get { return artists; } }
        public IEnumerable<Album> Albums { get { return albums; } }
        public IEnumerable<Track> Tracks { get { return tracks; } }

        private void InitializeEntity(string type, Action<string[]> action)
        {
            var createdLog = string.Format(createdLogFormat, type);

            if (File.Exists(createdLog))
            {
                using (var reader = new StreamReader(createdLog))
                {
                    var line = string.Empty;
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var data = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                            if (data == null || data.Length < 3)
                                continue;

                            action(data);
                        }
                    }
                }
            }
        }

        public void Initialize()
        {
            InitializeEntity(EntityType.Artist, data => AddArtist(artistSerializer.Deserialize(data)));
            InitializeEntity(EntityType.Album, data => AddAlbum(albumSerializer.Deserialize(data)));
            InitializeEntity(EntityType.Track, data => AddTrack(trackSerializer.Deserialize(data)));
        }

        public void Persist()
        {
        }
    }
}
