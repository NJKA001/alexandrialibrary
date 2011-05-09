using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;

namespace Gnosis.Alexandria.Repositories.Tracks
{
    public class TrackRepository
        : RepositoryBase<ITrack>, ITrackRepository
    {
        public TrackRepository(IContext context)
            : base(context)
        {
        }

        protected override ITrack Create()
        {
            return new Track(Context, new Uri("urn:unknown"));
        }

        //protected override string GetInitializeText()
        //{
        //    var track = new Track(Context, new Uri("urn:unknown"));

        //    var trackTable =
        //        new CreateTableBuilder("Track")
        //        .PrimaryKeyText("TrackId")
        //        .TextColumn("Title", track.Title);

        //    var titleIndex =
        //        new CreateIndexBuilder("Track_Title", "Track")
        //        .AscendingColumn("Title");

        //    var artistsIndex =
        //        new CreateIndexBuilder("Track_Artists", "Track")
        //        .AscendingColumn("Artists");

        //    return string.Empty;
        //}

        public ITrack New(Uri location)
        {
            throw new NotImplementedException();
        }

        public ITrack GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITrack GetOne(Uri location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrack> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrack> GetAny(ITrackSearch search)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<ITrack> tracks)
        {
            throw new NotImplementedException();
        }
    }
}
