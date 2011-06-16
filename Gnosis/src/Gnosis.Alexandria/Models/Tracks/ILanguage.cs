using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ILanguage
        : IValue
    {
        string Code { get; }
        string Name { get; }
    }
}
