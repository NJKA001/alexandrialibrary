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
        string Name { get; }
        string CharSet { get; }
        string Boundary { get; }
        MediaSupertype Supertype { get; }
    }
}
