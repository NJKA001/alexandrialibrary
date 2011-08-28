using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResourceLocation
        : IResourceIdentifier
    {
        IResourceUserInfo UserInfo { get; }
        IHost Host { get; }
        int Port { get; }
        IResourcePath Path { get; }
        IResourceQuery Query { get; }
        string Fragment { get; }
    }
}
