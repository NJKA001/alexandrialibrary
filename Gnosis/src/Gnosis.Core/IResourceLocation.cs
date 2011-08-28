using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResourceLocation
        : IResourceIdentifier
    {
        string Host { get; }
        string User { get; }
        string Password { get; }
        int Port { get; }
        string Path { get; }
    }
}
