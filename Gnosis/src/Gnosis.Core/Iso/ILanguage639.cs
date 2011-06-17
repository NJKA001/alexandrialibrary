using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Iso
{
    /// <summary>
    /// Defines a language based on the ISO 639-1 and 639-2 standards
    /// </summary>
    public interface ILanguage639
    {
        string Alpha2Code { get; }
        string Alpha3Code { get; }
        string Name { get; }
    }
}
