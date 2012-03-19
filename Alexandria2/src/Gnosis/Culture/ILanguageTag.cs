using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Geography;

namespace Gnosis.Culture
{
    /// <summary>
    /// Defines a language tag based on IETF standard BCP 47
    /// </summary>
    public interface ILanguageTag
    {
        ILanguage PrimaryLanguage { get; }
        string ExtendedLanguage { get; }
        IScript Script { get; }
        ICountry Country { get; }
        IRegion Region { get; }
        IEnumerable<string> Variants { get; }
        IEnumerable<string> Extensions { get; }
        string PrivateUse { get; }
    }
}
