using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags.Id3;
using Gnosis.Core.Tags.Id3.Id3v2;

namespace Gnosis.Core.Audio
{
    public interface IMpegAudio
        : IAudio
    {
        IEnumerable<IId3Tag> GetTags();

        void SetTag(IId3Tag tag);
        void RemoveTag(IId3Tag tag);
        void Save();
    }
}
