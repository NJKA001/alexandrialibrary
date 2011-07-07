﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public interface IAtomLink
        : IAtomCommon
    {
        Uri Href { get; }

        string Rel { get; }
        IMediaType MediaType { get; }
        ILanguageTag HrefLang { get; }
        string Title { get; }
        int Length { get; }
    }
}
