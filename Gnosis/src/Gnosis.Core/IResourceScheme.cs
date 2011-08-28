using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    /// <summary>
    /// Defines the Scheme part of a Uniform Resource Identifier
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/URI_scheme
    /// </remarks>
    public interface IResourceScheme
    {
        string Name { get; }
        string Description { get; }
        bool IsOfficial { get; }
        bool IsRecognized { get; }
    }
}
