using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICountry : IImmutable
    {
        string Name { get; }
        string NameHash { get; }
        string Abbreviation { get; }
        string Code { get; }
        DateTime FromDate { get; }
        DateTime ToDate { get; }
    }
}
