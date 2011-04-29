using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackPicture : IChangeableModel
    {
        ITrack Track { get; }
        string TextEncoding { get; set; }
        string MediaType { get; set; }
        TrackPictureType PictureType { get; set; }
        string Description { get; set; }
        byte[] PictureData { get; set; }
    }
}
