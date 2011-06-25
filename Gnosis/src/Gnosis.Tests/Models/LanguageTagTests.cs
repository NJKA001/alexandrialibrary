using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.Iso;
using Gnosis.Core.UN;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class LanguageTagTests
    {
        [Test]
        public void ParseGrandfatheredNormalizedTag()
        {
            const string original = "EN-GB-oed";
            const string normalized = "en-GB";

            var tag = LanguageTag.Parse(original);
            Assert.IsNotNull(tag);
            Assert.AreEqual(Language.English, tag.PrimaryLanguage);
            Assert.AreEqual(Country.UnitedKingdom, tag.Country);
            Assert.IsNull(tag.Region);
            Assert.IsNull(tag.PrivateUse);
            Assert.AreEqual(0, tag.Extensions.Count());
            Assert.AreEqual(0, tag.Variants.Count());
            Assert.AreEqual(normalized, tag.ToString());
        }

        [Test]
        public void ParseGrandfatheredAliasedTags()
        {
            const string original1 = "i-hak";
            const string original2 = "zh-hakka";
            const string normalized = "hak";

            var tag1 = LanguageTag.Parse(original1);
            var tag2 = LanguageTag.Parse(original2);

            Assert.IsNotNull(tag1);
            Assert.IsNotNull(tag2);
            Assert.AreEqual(Language.Hakka, tag1.PrimaryLanguage);
            Assert.AreEqual(Language.Hakka, tag2.PrimaryLanguage);
            Assert.IsNull(tag1.ExtendedLanguage);
            Assert.IsNull(tag2.ExtendedLanguage);
            Assert.AreEqual(normalized, tag1.ToString());
            Assert.AreEqual(normalized, tag2.ToString());
        }

        [Test]
        public void ParseInvalidTag()
        {
            const string original = "xyz-us-Latn";
            var tag = LanguageTag.Parse(original);
            Assert.IsNotNull(tag);
            Assert.AreEqual(Language.Undetermined, tag.PrimaryLanguage);
            Assert.AreEqual(string.Empty, tag.ToString());
        }

        [Test]
        public void ParseLanguageScriptRegionTag()
        {
            const string original = "ja-Hrkt-035";
            var tag = LanguageTag.Parse(original);
            Assert.IsNotNull(tag);
            Assert.AreEqual(Language.Japanese, tag.PrimaryLanguage);
            Assert.AreEqual(Script.HiraganaKatakana, tag.Script);
            Assert.AreEqual(Region.SouthEasternAsia, tag.Region);
            Assert.AreEqual(original, tag.ToString());
            Assert.IsNull(tag.Country);
        }

        [Test]
        public void ParseLanguageScriptCountryTag()
        {
            const string original = "AZE-CYRL-ir";
            const string normalized = "az-Cyrl-IR";
            var tag = LanguageTag.Parse(original);
            Assert.IsNotNull(tag);
            Assert.AreEqual(Language.Azerbaijani, tag.PrimaryLanguage);
            Assert.AreEqual(Script.Cyrillic, tag.Script);
            Assert.AreEqual(Country.Iran, tag.Country);
            Assert.AreEqual(normalized, tag.ToString());
            Assert.IsNull(tag.Region);
        }

        [Test]
        public void ParseLanguageExtensionScriptCountryVariantsExtensionsPrivateUseTag()
        {
            const string original = "pol-gft-Latf-DE-slang-grafitto-2code-p-pxcode1-g-code2-x-secret";
            const string normalized = "pl-gft-Latf-DE-slang-grafitto-2code-p-pxcode1-g-code2-x-secret";
            var tag = LanguageTag.Parse(original);
            Assert.IsNotNull(tag);
            Assert.AreEqual(Language.Polish, tag.PrimaryLanguage);
            Assert.AreEqual(Script.LatinFraktur, tag.Script);
            Assert.AreEqual(Country.Germany, tag.Country);
            Assert.IsNull(tag.Region);
            Assert.AreEqual("gft", tag.ExtendedLanguage);
            Assert.AreEqual(3, tag.Variants.Count());
            Assert.AreEqual("slang", tag.Variants.First());
            Assert.AreEqual("2code", tag.Variants.Last());
            Assert.IsTrue(tag.Variants.Contains("grafitto"));
            Assert.AreEqual(2, tag.Extensions.Count());
            Assert.AreEqual("p-pxcode1", tag.Extensions.First());
            Assert.AreEqual("g-code2", tag.Extensions.Last());
            Assert.AreEqual("x-secret", tag.PrivateUse);
            Assert.AreEqual(normalized, tag.ToString());
        }
    }
}
