using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IMediaType
    {
        MediaSupertype Supertype { get; }
        string Subtype { get; }

        IEnumerable<string> FileExtensions { get; }
        IEnumerable<byte[]> MagicNumbers { get; }
    }
}
