﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Culture
{
    /// <summary>
    /// Defines a language based on the ISO 639-1 and 639-2 standards
    /// </summary>
    public interface ILanguage
    {
        string Alpha3Code { get; }
        string Alpha3TermCode { get; }
        string Alpha2Code { get; }
        string Name { get; }
    }
}
