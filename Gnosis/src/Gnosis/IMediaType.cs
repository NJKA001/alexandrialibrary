using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    /// <summary>
    /// Defines an internet media type based on IETF RFC 2046
    /// </summary>
    /// <remarks>http://tools.ietf.org/html/rfc2046</remarks>
    public interface IMediaType
    {
        bool IsDefault { get; }
        MediaSupertype Supertype { get; }
        string Subtype { get; }
        IEnumerable<string> FileExtensions { get; }
        IEnumerable<string> LegacyTypes { get; }
        IEnumerable<byte[]> MagicNumbers { get; }

        IMedia CreateMedia(Uri location);
    }
}
