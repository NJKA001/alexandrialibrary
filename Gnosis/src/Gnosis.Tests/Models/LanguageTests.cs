using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class LanguageTests
    {
        //private ILogger logger = new DebugLogger();

        [Test]
        public void LookupLanguage()
        {
            const int totalAlph2 = 184;
            const int totalAlpha3 = 497;
            const int totalAlpha3Term = 20;

            var map2 = new Dictionary<string, ILanguage>();
            var map3 = new Dictionary<string, ILanguage>();
            var map3t = new Dictionary<string, ILanguage>();

            foreach (var lang in Language.GetLanguages())
            {
                //logger.Debug("lang name=" + lang.Name + " alpha2=" + lang.Alpha2Code + " alpha3=" + lang.Alpha3Code + " alpha3term=" + lang.Alpha3TermCode);

                if (!string.IsNullOrEmpty(lang.Alpha2Code))
                {
                    map2.Add(lang.Alpha2Code, lang);
                    Assert.AreEqual(lang, Language.GetLanguageByCode(lang.Alpha2Code));
                }

                if (!string.IsNullOrEmpty(lang.Alpha3TermCode))
                {
                    map3t.Add(lang.Alpha3TermCode, lang);
                    Assert.AreEqual(lang, Language.GetLanguageByCode(lang.Alpha3TermCode));
                }

                map3.Add(lang.Alpha3Code, lang);
                Assert.AreEqual(lang, Language.GetLanguageByCode(lang.Alpha3Code));
                Assert.AreEqual(lang, Language.GetLanguageByName(lang.Name));
            }

            Assert.AreEqual(Language.Undetermined, Language.GetLanguageByCode("xyz"));
            Assert.AreEqual(Language.Undetermined, Language.GetLanguageByCode(null));
            Assert.AreEqual(Language.Undetermined, Language.GetLanguageByName("Ancient Elven"));

            Assert.AreEqual(totalAlpha3, map3.Count);
            Assert.AreEqual(totalAlpha3Term, map3t.Count);
            Assert.AreEqual(totalAlph2, map2.Count);
        }

    }
}
