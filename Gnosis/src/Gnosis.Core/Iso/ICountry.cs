using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Iso
{
    /// <summary>
    /// Defines a country based on the ISO 3166-1 and 3166-2 standards
    /// </summary>
    public interface ICountry
    {
        string Name { get; }
        string Alpha2Code { get; }
        string Alpha3Code { get; }
        int Number { get; }
        int Year { get; }
        string TopLevelDomain { get; }
    }
}
