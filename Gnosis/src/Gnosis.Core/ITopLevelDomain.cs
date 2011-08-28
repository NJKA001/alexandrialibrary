using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITopLevelDomain
    {
        string Name { get; }
        string Description { get; }
        ICountry Country { get; }
        bool IsGeneric { get; }
        bool IsInternational { get; }
        //.परीक्षा
    }
}
