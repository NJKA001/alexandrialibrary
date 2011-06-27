using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Devices
{
    public interface IDeviceItemAttributes : IValue
    {
        bool IsReadOnly { get; }
        bool IsHidden { get; }
        bool IsSystem { get; }
        bool IsDirectory { get; }
        bool IsArchive { get; }
        bool IsNormal { get; }
        bool IsTemporary { get; }
        bool IsSparseFile { get; }
        bool HasReparsePoint { get; }
        bool IsCompressed { get; }
        bool IsOffline { get; }
        bool IsNotContentIndexed { get; }
        bool IsEncrypted { get; }
    }
}
