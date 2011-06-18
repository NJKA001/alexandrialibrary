using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Iso;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class Iso639LanguageTests
    {
        private ILogger logger = new DebugLogger();

        [Test]
        public void LookupLanguage()
        {
            var map2 = new Dictionary<string, IIso639Language>();
            var map3 = new Dictionary<string, IIso639Language>();
            var map3t = new Dictionary<string, IIso639Language>();

            foreach (var lang in Iso639Language.GetLanguages())
            {
                logger.Debug("lang name=" + lang.Name + " alpha2=" + lang.Alpha2Code + " alpha3=" + lang.Alpha3Code + " alpha3term=" + lang.Alpha3TermCode);

                if (!string.IsNullOrEmpty(lang.Alpha2Code))
                {
                    map2.Add(lang.Alpha2Code, lang);
                    Assert.AreEqual(lang, Iso639Language.GetLanguageByCode(lang.Alpha2Code));
                }

                if (!string.IsNullOrEmpty(lang.Alpha3TermCode))
                {
                    map3t.Add(lang.Alpha3TermCode, lang);
                    Assert.AreEqual(lang, Iso639Language.GetLanguageByCode(lang.Alpha3TermCode));
                }

                map3.Add(lang.Alpha3Code, lang);
                Assert.AreEqual(lang, Iso639Language.GetLanguageByCode(lang.Alpha3Code));
                Assert.AreEqual(lang, Iso639Language.GetLanguageByName(lang.Name));
            }

            Assert.AreEqual(484, map3.Count);
            Assert.AreEqual(20, map3t.Count);
            Assert.AreEqual(184, map2.Count);
        }

    }
}
