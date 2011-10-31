using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tags.Id3;
using Gnosis.Tags.Id3.Id3v2;

namespace Gnosis.Audio
{
    public interface IMpegAudio
        : IAudio
    {
        IEnumerable<IId3Tag> GetId3Tags();

        void SetTag(IId3Tag tag);
        void RemoveTag(IId3Tag tag);
        void Save();
    }
}
