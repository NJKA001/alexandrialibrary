using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("TrackPicture")]
    [UniqueIndex("TrackPicture_Track_Description", "Track", "Description")]
    [Index("TrackPicture_Track_PictureType", "Track", "PictureType")]
    public interface ITrackPicture : IEntity
    {
        ITrack Track { get; }
        string TextEncoding { get; set; }
        string MediaType { get; set; }
        [Column("PictureType", 0)]
        TrackPictureType PictureType { get; set; }
        string Description { get; set; }
        byte[] PictureData { get; set; }
    }
}
