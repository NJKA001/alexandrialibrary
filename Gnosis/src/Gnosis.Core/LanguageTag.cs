using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class LanguageTag
        : ILanguageTag
    {
        private LanguageTag(ILanguage primaryLanguage, ICountry country)
            : this(primaryLanguage, null, null, country, null, new List<string>(), new List<string>(), null)
        {
        }

        private LanguageTag(ILanguage primaryLanguage, IRegion region)
            : this(primaryLanguage, null, null, null, region, new List<string>(), new List<string>(), null)
        {
        }

        private LanguageTag(ILanguage primaryLanguage, string extendedLanguage, IScript script, ICountry country, IRegion region, IEnumerable<string> variants, IEnumerable<string> extensions, string privateUse)
        {
            if (primaryLanguage == null)
                throw new ArgumentNullException("primaryLanguage");

            this.primaryLanguage = primaryLanguage;
            this.extendedLanguage = extendedLanguage;
            this.script = script;
            this.country = country;
            this.region = region;
            this.variants = variants;
            this.extensions = extensions;
            this.privateUse = privateUse;
        }

        private readonly ILanguage primaryLanguage;
        private readonly string extendedLanguage;
        private readonly IScript script;
        private readonly ICountry country;
        private readonly IRegion region;
        private readonly IEnumerable<string> variants;
        private readonly IEnumerable<string> extensions;
        private readonly string privateUse;
        
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

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
                return false;

            var lang = obj as ILanguageTag;
            if (lang == null)
                return false;

            return this == lang;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            if (primaryLanguage == Language.Undetermined)
                return string.Empty;

            const string alphaFormat = "-{0}";
            const string numFormat = "-{0:000}";

 	        var builder = new StringBuilder();

            builder.Append(primaryLanguage.ToString());

            if (!string.IsNullOrEmpty(extendedLanguage))
                builder.AppendFormat(alphaFormat, extendedLanguage);

            if (script != null)
                builder.AppendFormat(alphaFormat, script.ToString());

            if (country != null)
                builder.AppendFormat(alphaFormat, country.ToString());

            if (region != null)
                builder.AppendFormat(numFormat, region.Code);

            if (variants != null && variants.Count() > 0)
            {
                foreach (var variant in variants)
                    builder.AppendFormat(alphaFormat, variant);
            }

            if (extensions != null && extensions.Count() > 0)
            {
                foreach (var extension in extensions)
                    builder.AppendFormat(alphaFormat, extension);
            }

            if (!string.IsNullOrEmpty(privateUse))
                builder.AppendFormat(alphaFormat, privateUse);

            return builder.ToString();
        }

        public static bool operator ==(LanguageTag lang1, ILanguageTag lang2)
        {
            if (object.ReferenceEquals(lang1, lang2))
                return true;

            if (object.ReferenceEquals(lang1, null) || object.ReferenceEquals(lang2, null))
                return false;

            return lang1.ToString() == lang2.ToString();
        }

        public static bool operator !=(LanguageTag lang1, ILanguageTag lang2)
        {
            return !(lang1 == lang2);
        }

        static LanguageTag()
        {
            InitializeGrandfatheredTags();
        }

        private static readonly IDictionary<string, Tuple<ILanguage, ICountry>> grandfatheredTags = new Dictionary<string, Tuple<ILanguage, ICountry>>();

        public static readonly ILanguageTag Empty = new LanguageTag(Language.Undetermined, Core.Country.Unknown);

        #region InitializeGrandfatheredTags

        /// <summary>
        /// Initialize the list of valid grandfathered tags mapped to their preferred language value and (optionally) country
        /// </summary>
        /// <remarks>Taken from the IANA registry at: http://www.iana.org/assignments/language-subtag-registry </remarks>
        private static void InitializeGrandfatheredTags()
        {
            grandfatheredTags.Add("art-lojban", new Tuple<ILanguage, ICountry>(Language.Lojban, null));
            grandfatheredTags.Add("cel-gualish", new Tuple<ILanguage, ICountry>(Language.Celtic, null));
            grandfatheredTags.Add("en-gb-oed", new Tuple<ILanguage, ICountry>(Language.English, Core.Country.UnitedKingdom));
            grandfatheredTags.Add("i-ami", new Tuple<ILanguage, ICountry>(Language.Amis, null));
            grandfatheredTags.Add("i-bnn", new Tuple<ILanguage, ICountry>(Language.Bunun, null));
            grandfatheredTags.Add("i-default", new Tuple<ILanguage, ICountry>(Language.UncodedLanguages, null));
            grandfatheredTags.Add("i-enochian", new Tuple<ILanguage, ICountry>(Language.UncodedLanguages, null));
            grandfatheredTags.Add("i-hak", new Tuple<ILanguage, ICountry>(Language.Hakka, null));
            grandfatheredTags.Add("i-klingon", new Tuple<ILanguage, ICountry>(Language.Klingon, null));
            grandfatheredTags.Add("i-lux", new Tuple<ILanguage, ICountry>(Language.Luxembourgish, null));
            grandfatheredTags.Add("i-mingo", new Tuple<ILanguage, ICountry>(Language.Iroquoian, null));
            grandfatheredTags.Add("i-navajo", new Tuple<ILanguage, ICountry>(Language.Navajo, null));
            grandfatheredTags.Add("i-pwn", new Tuple<ILanguage, ICountry>(Language.Paiwan, null));
            grandfatheredTags.Add("i-tao", new Tuple<ILanguage, ICountry>(Language.Tao, null));
            grandfatheredTags.Add("i-tay", new Tuple<ILanguage, ICountry>(Language.Tayal, null));
            grandfatheredTags.Add("i-tsu", new Tuple<ILanguage, ICountry>(Language.Tsou, null));
            grandfatheredTags.Add("no-bok", new Tuple<ILanguage, ICountry>(Language.NorwegianBokmal, null));
            grandfatheredTags.Add("no-nyn", new Tuple<ILanguage, ICountry>(Language.NorwegianNynorsk, null));
            grandfatheredTags.Add("sgn-be-fr", new Tuple<ILanguage, ICountry>(Language.BelgianFrenchSignLanguage, null));
            grandfatheredTags.Add("sgn-be-nl", new Tuple<ILanguage, ICountry>(Language.BelgianFlemishSignLanguage, null));
            grandfatheredTags.Add("sgn-ch-de", new Tuple<ILanguage, ICountry>(Language.SwissGermanSignLanguage, null));
            grandfatheredTags.Add("zh-guoyu", new Tuple<ILanguage, ICountry>(Language.MandarinChinese, null));
            grandfatheredTags.Add("zh-hakka", new Tuple<ILanguage, ICountry>(Language.Hakka, null));
            grandfatheredTags.Add("zh-min", new Tuple<ILanguage, ICountry>(Language.Chinese, Core.Country.Taiwan));
            grandfatheredTags.Add("zh-min-nan", new Tuple<ILanguage, ICountry>(Language.MinanHokkien, null));
            grandfatheredTags.Add("zh-xiang", new Tuple<ILanguage, ICountry>(Language.Xiang, null));
        }

        #endregion

        #region Public Static Methods

        public static ILanguageTag Create(ILanguage primaryLanguage, ICountry country)
        {
            if (primaryLanguage == null)
                throw new ArgumentNullException("primaryLanguage");
            if (country == null)
                throw new ArgumentNullException("country");

            return new LanguageTag(primaryLanguage, country);
        }

        public static ILanguageTag Create(ILanguage primaryLanguage, IRegion region)
        {
            if (primaryLanguage == null)
                throw new ArgumentNullException("primaryLanguage");
            if (region == null)
                throw new ArgumentNullException("region");

            return new LanguageTag(primaryLanguage, region);
        }

        public static ILanguageTag Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Empty;

            ILanguage primaryLanguage = null;
            string extendedLanguage = null;
            IScript script = null;
            ICountry country = null;
            var regionCode = -1;
            IRegion region = null;
            var variants = new List<string>();
            var extensions = new List<string>();
            string privateUse = null;

            var lower = value.ToLower();
            if (grandfatheredTags.ContainsKey(lower))
            {
                primaryLanguage = grandfatheredTags[lower].Item1;
                country = grandfatheredTags[lower].Item2;
                return new LanguageTag(primaryLanguage, country);
            }

            var count = 1;
            var singleton = string.Empty;
            foreach (var token in value.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (count == 1)
                {
                    primaryLanguage = Language.GetLanguageByCode(token);
                }
                else
                {
                    if (string.IsNullOrEmpty(singleton))
                    {
                        switch (token.Length)
                        {
                            case 1:
                                singleton = token;
                                break;
                            case 2:
                                {
                                    if (token.IsMixedAlphaNumeric())
                                    {
                                        variants.Add(token);
                                    }
                                    else
                                    {
                                        country = Core.Country.GetCountryByCode(token);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (token.IsMixedAlphaNumeric())
                                    {
                                        variants.Add(token);
                                    }
                                    else
                                    {
                                        if (int.TryParse(token, out regionCode))
                                            region = Core.Region.GetRegionByCode(regionCode);
                                        else
                                            extendedLanguage = token;
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    if (token.IsMixedAlphaNumeric())
                                    {
                                        variants.Add(token);
                                    }
                                    else
                                    {
                                        script = Core.Script.GetScriptByCode(token);
                                    }
                                    break;
                                }
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                {
                                    variants.Add(token);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (singleton.ToLower() == "x")
                        {
                            if (token.Length >= 1 && token.Length <= 8)
                            {
                                privateUse = string.Format("{0}-{1}", singleton, token);
                            }
                        }
                        else if (token.Length >= 2 && token.Length <= 8)
                        {
                            extensions.Add(string.Format("{0}-{1}", singleton, token));
                        }
                        singleton = string.Empty;
                    }
                }

                count++;
            }

            return (primaryLanguage != null) ? new LanguageTag(primaryLanguage, extendedLanguage, script, country, region, variants, extensions, privateUse) : Empty;
        }

        #endregion
    }
}
