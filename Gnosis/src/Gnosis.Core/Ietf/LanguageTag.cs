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
        private LanguageTag(ILanguage primaryLanguage)
        {
            this.primaryLanguage = primaryLanguage;
        }

        private LanguageTag(ILanguage primaryLanguage, ICountry country)
        {
            this.primaryLanguage = primaryLanguage;
            this.country = country;
        }

        private readonly ILanguage primaryLanguage;
        private readonly string extendedLanguage;
        private readonly IScript script;
        private readonly ICountry country;
        private readonly IRegion region;
        private readonly IEnumerable<string> variants;
        private readonly IEnumerable<string> extensions;
        private readonly string privateUse;
        private readonly IDictionary<string, ILanguage> grandfatheredTags = new Dictionary<string, ILanguage>();
        //private readonly IDictionary<string, ILanguage> redundantTags = new Dictionary<string, ILanguage>();

        #region Grandfathered And Redundant Tags

        /// <summary>
        /// Initialize the list of valid grandfathered tags mapped to their preferred language value
        /// </summary>
        /// <remarks>Taken from the IANA registry at: http://www.iana.org/assignments/language-subtag-registry </remarks>
        private void InitializeGrandfatheredTags()
        {
            grandfatheredTags.Add("art-lojban",  Language.Lojban);
            grandfatheredTags.Add("cel-gualish", Language.Celtic);
            grandfatheredTags.Add("en-gb-oed", Language.English);
            grandfatheredTags.Add("i-ami", Language.Amis);
            grandfatheredTags.Add("i-bnn", Language.Bunun);
            grandfatheredTags.Add("i-default", Language.Undetermined);
            grandfatheredTags.Add("i-enochian", Language.Sumerian);
            grandfatheredTags.Add("i-hak", Language.Hakka);
            grandfatheredTags.Add("i-klingon", Language.Klingon);
            grandfatheredTags.Add("i-lux", Language.Luxembourgish);
            grandfatheredTags.Add("i-mingo", Language.Iroquoian);
            grandfatheredTags.Add("i-navajo", Language.Navajo);
            grandfatheredTags.Add("i-pwn", Language.Paiwan);
            grandfatheredTags.Add("i-tao", Language.Tao);
            grandfatheredTags.Add("i-tay", Language.Tayal);
            grandfatheredTags.Add("i-tsu", Language.Tsou);
            grandfatheredTags.Add("no-bok", Language.NorwegianBokmal);
            grandfatheredTags.Add("no-nyn", Language.NorwegianNynorsk);
            grandfatheredTags.Add("sgn-be-fr", Language.BelgianFrenchSignLanguage);
            grandfatheredTags.Add("sgn-be-nl", Language.BelgianFlemishSignLanguage);
            grandfatheredTags.Add("sgn-ch-de", Language.SwissGermanSignLanguage);
            grandfatheredTags.Add("zh-guoyu", Language.MandarinChinese);
            grandfatheredTags.Add("zh-hakka", Language.Hakka);
            grandfatheredTags.Add("zh-min", Language.Chinese);
            grandfatheredTags.Add("zh-min-nan", Language.MinanHokkien);
            grandfatheredTags.Add("zh-xiang", Language.Xiang);
        }

        #endregion

        #region ILanguageTag Members

        public ILanguage PrimaryLanguage
        {
            get { return primaryLanguage; }
        }

        public string ExtendedLanguage
        {
            get { return extendedLanguage; }
        }

        public IScript Script
        {
            get { return script; }
        }

        public ICountry Country
        {
            get { return country; }
        }

        public IRegion Region
        {
            get { return region; }
        }

        public IEnumerable<string> Variants
        {
            get { return variants; }
        }

        public IEnumerable<string> Extensions
        {
            get { return extensions; }
        }

        public string PrivateUse
        {
            get { return privateUse; }
        }

        #endregion

        public override string ToString()
        {
 	        var builder = new StringBuilder();

            return builder.ToString();
        }
    }
}
