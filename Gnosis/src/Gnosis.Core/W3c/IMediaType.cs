using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public interface IMediaType
    {
        string Type { get; }
        string SubType { get; }
        IEnumerable<string> FileExtensions { get; }
        IEnumerable<string> LegacyTypes { get; }
        IEnumerable<string> MagicNumbers { get; }
    }
}
