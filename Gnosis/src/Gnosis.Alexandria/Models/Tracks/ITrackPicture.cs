using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackPicture : IEntity
    {
        string TextEncoding { get; set; }
        string MediaType { get; set; }
        TrackPictureType PictureType { get; set; }
        string Description { get; set; }
        byte[] PictureData { get; set; }
    }
}
