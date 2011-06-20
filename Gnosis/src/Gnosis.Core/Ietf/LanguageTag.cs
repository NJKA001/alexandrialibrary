using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Iso;
using Gnosis.Core.UN;

namespace Gnosis.Core.Ietf
{
    public class LanguageTag
        : ILanguageTag
    {
        #region ILanguageTag Members

        public ILanguage Language
        {
            get { throw new NotImplementedException(); }
        }

        public string ExtendedLanguage
        {
            get { throw new NotImplementedException(); }
        }

        public IScript Script
        {
            get { throw new NotImplementedException(); }
        }

        public ICountry Country
        {
            get { throw new NotImplementedException(); }
        }

        public IRegion Region
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<string> Variants
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<string> Extensions
        {
            get { throw new NotImplementedException(); }
        }

        public string PrivateUse
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
