using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackPicture : IChild
    {
        TextEncoding TextEncoding { get; set; }
        string MediaType { get; set; }
        TrackPictureType PictureType { get; set; }
        string Description { get; set; }
        byte[] Data { get; set; }
    }
}
