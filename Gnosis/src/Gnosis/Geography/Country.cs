using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Geography
{
    public class Country
        : ICountry
    {
        private Country(string alpha2Code, string name, int year, string topLevelDomain)
            : this(alpha2Code, string.Empty, -1, name, year, topLevelDomain)
        {
        }

        private Country(string alpha2Code, string alpha3Code, int number, string name, int year, string topLevelDomain)
        {
            this.alpha2Code = alpha2Code;
            this.alpha3Code = alpha3Code;
            this.number = number;
            this.name = name;
            this.year = year;
            this.topLevelDomain = topLevelDomain;
        }

        private readonly string alpha2Code;
        private readonly string alpha3Code;
        private readonly int number;
        private readonly string name;
        private readonly int year;
        private readonly string topLevelDomain;

        #region ICountry Members

        public string Name
        {
            get { return name; }
        }

        public string Alpha2Code
        {
            get { return alpha2Code; }
        }

        public string Alpha3Code
        {
            get { return alpha3Code; }
        }

        public int Number
        {
            get { return number; }
        }

        public int Year
        {
            get { return year; }
        }

        public string TopLevelDomain
        {
            get { return topLevelDomain; }
        }

        #endregion

        public override string ToString()
        {
            return alpha2Code;
        }

        static Country()
        {
            InitializeCountries();

            foreach (var country in countries)
            {
                byAlpha2Code.Add(country.Alpha2Code, country);
                byName.Add(country.Name.ToUpper(), country);
                byTopLevelDomain.Add(country.TopLevelDomain, country);

                if (!string.IsNullOrEmpty(country.Alpha3Code))
                    byAlpha3Code.Add(country.Alpha3Code, country);

                if (country.Number > -1)
                    byNumber.Add(country.Number, country);
            }
        }

        private static readonly List<ICountry> countries = new List<ICountry>();
        private static readonly IDictionary<string, ICountry> byAlpha2Code = new Dictionary<string, ICountry>();
        private static readonly IDictionary<string, ICountry> byAlpha3Code = new Dictionary<string, ICountry>();
        private static readonly IDictionary<string, ICountry> byTopLevelDomain = new Dictionary<string, ICountry>();
        private static readonly IDictionary<string, ICountry> byName = new Dictionary<string, ICountry>();
        private static readonly IDictionary<int, ICountry> byNumber = new Dictionary<int, ICountry>();

        #region InitializeCountries

        private static void InitializeCountries()
        {
            countries.Add(Unknown);
            countries.Add(Andorra);
            countries.Add(UnitedArabEmirates);
            countries.Add(Afghanistan);
            countries.Add(Antigua);
            countries.Add(Anguilla);
            countries.Add(Albania);
            countries.Add(Armenia);
            countries.Add(Angola);
            countries.Add(Antarctica);
            countries.Add(Argentina);
            countries.Add(AmericanSomoa);
            countries.Add(Austria);
            countries.Add(Australia);
            countries.Add(Aruba);
            countries.Add(AlandIslands);
            countries.Add(Azerbaijan);
            countries.Add(Bosnia);
            countries.Add(Barbados);
            countries.Add(Bangladesh);
            countries.Add(Belgium);
            countries.Add(BurkinaFaso);
            countries.Add(Bulgaria);
            countries.Add(Bahrain);
            countries.Add(Burundi);
            countries.Add(Benin);
            countries.Add(SaintBarthelemy);
            countries.Add(Bermuda);
            countries.Add(Brunei);
            countries.Add(Bolivia);
            countries.Add(Bonaire);
            countries.Add(Brazil);
            countries.Add(Bahamas);
            countries.Add(Bhutan);
            countries.Add(BouvetIsland);
            countries.Add(Botswana);
            countries.Add(Belarus);
            countries.Add(Belize);
            countries.Add(Canada);
            countries.Add(CocosIslands);
            countries.Add(CongoDemocraticRepublic);
            countries.Add(CentralAfricanRepublic);
            countries.Add(Congo);
            countries.Add(Switzerland);
            countries.Add(CoteDIvoire);
            countries.Add(CookIslands);
            countries.Add(Chile);
            countries.Add(Cameroon);
            countries.Add(China);
            countries.Add(Colombia);
            countries.Add(CostaRica);
            countries.Add(Cuba);
            countries.Add(CapeVerde);
            countries.Add(Curacao);
            countries.Add(ChristmasIslands);
            countries.Add(Cyprus);
            countries.Add(CzechRepublic);
            countries.Add(Germany);
            countries.Add(Djibouti);
            countries.Add(Denmark);
            countries.Add(Dominica);
            countries.Add(DominicanRepublic);
            countries.Add(Algeria);
            countries.Add(Ecuador);
            countries.Add(Estonia);
            countries.Add(Egypt);
            countries.Add(WesternSahara);
            countries.Add(Eritrea);
            countries.Add(Spain);
            countries.Add(Ethiopia);
            countries.Add(Finland);
            countries.Add(Fiji);
            countries.Add(FalklandIslands);
            countries.Add(Micronesia);
            countries.Add(FaroeIslands);
            countries.Add(France);
            countries.Add(Gabon);
            countries.Add(UnitedKingdom);
            countries.Add(Grenada);
            countries.Add(Georgia);
            countries.Add(FrenchGuiana);
            countries.Add(Guernsey);
            countries.Add(Ghana);
            countries.Add(Gibraltar);
            countries.Add(Greenland);
            countries.Add(Gambia);
            countries.Add(Guinea);
            countries.Add(Guadeloupe);
            countries.Add(EquatorialGuinea);
            countries.Add(Greece);
            countries.Add(South);
            countries.Add(Guatemala);
            countries.Add(Guam);
            countries.Add(GuineaBissau);
            countries.Add(Guyana);
            countries.Add(HongKong);
            countries.Add(HeardIsland);
            countries.Add(Honduras);
            countries.Add(Croatia);
            countries.Add(Haiti);
            countries.Add(Hungary);
            countries.Add(Indonesia);
            countries.Add(Ireland);
            countries.Add(Israel);
            countries.Add(IsleOfMan);
            countries.Add(India);
            countries.Add(BritishIndianOceanTerritory);
            countries.Add(Iraq);
            countries.Add(Iran);
            countries.Add(Iceland);
            countries.Add(Italy);
            countries.Add(Jersey);
            countries.Add(Jamaica);
            countries.Add(Jordan);
            countries.Add(Japan);
            countries.Add(Kenya);
            countries.Add(Kyrgyzstan);
            countries.Add(Cambodia);
            countries.Add(Kiribati);
            countries.Add(Comoros);
            countries.Add(SaintKittsAndNevis);
            countries.Add(NorthKorea);
            countries.Add(SouthKorea);
            countries.Add(Kuwait);
            countries.Add(CaymanIslands);
            countries.Add(Kazakhstan);
            countries.Add(LaoPeoplesDemocraticRepublic);
            countries.Add(Lebanon);
            countries.Add(SaintLucia);
            countries.Add(Liechtenstein);
            countries.Add(SriLanka);
            countries.Add(Liberia);
            countries.Add(Lesotho);
            countries.Add(Lithuania);
            countries.Add(Luxembourg);
            countries.Add(Latvia);
            countries.Add(Libya);
            countries.Add(Morocco);
            countries.Add(Monaco);
            countries.Add(Moldova);
            countries.Add(Montenego);
            countries.Add(SaintMartin);
            countries.Add(Madagascar);
            countries.Add(MarshallIslands);
            countries.Add(Macedonia);
            countries.Add(Mali);
            countries.Add(Myanmar);
            countries.Add(Mongolia);
            countries.Add(Macao);
            countries.Add(NorthernMarianaIslands);
            countries.Add(Martinique);
            countries.Add(Mauritania);
            countries.Add(Montserrat);
            countries.Add(Malta);
            countries.Add(Mauitius);
            countries.Add(Maldives);
            countries.Add(Malawi);
            countries.Add(Mexico);
            countries.Add(Malasia);
            countries.Add(Mozambique);
            countries.Add(Namibia);
            countries.Add(NewCaledonia);
            countries.Add(Niger);
            countries.Add(NorfolkIsland);
            countries.Add(Nigeria);
            countries.Add(Nicaragua);
            countries.Add(Netherlands);
            countries.Add(Norway);
            countries.Add(Nepal);
            countries.Add(Nauru);
            countries.Add(Niue);
            countries.Add(NewZealand);
            countries.Add(Oman);
            countries.Add(Panama);
            countries.Add(Peru);
            countries.Add(FrenchPolynesia);
            countries.Add(PapuaNewGuinea);
            countries.Add(Philippines);
            countries.Add(Pakistan);
            countries.Add(Poland);
            countries.Add(SaintPierreAndMiquelon);
            countries.Add(Pitcairn);
            countries.Add(PuertoRico);
            countries.Add(OccupiedPalestinianTerritory);
            countries.Add(Portugal);
            countries.Add(Palau);
            countries.Add(Paraguay);
            countries.Add(Qatar);
            countries.Add(Reunion);
            countries.Add(Romania);
            countries.Add(Serbia);
            countries.Add(RussianFederation);
            countries.Add(Rwanda);
            countries.Add(SaudiArabia);
            countries.Add(SolomonIslands);
            countries.Add(Seychelles);
            countries.Add(Sudan);
            countries.Add(Sweden);
            countries.Add(Singapore);
            countries.Add(SaintHelena);
            countries.Add(Slovenia);
            countries.Add(SvalbardAndJanMayen);
            countries.Add(Slovakia);
            countries.Add(SierraLeone);
            countries.Add(SanMarino);
            countries.Add(Senegal);
            countries.Add(Somalia);
            countries.Add(Suriname);
            countries.Add(SaoTomeAndPrincipe);
            countries.Add(ElSalvador);
            countries.Add(SintMaarten);
            countries.Add(SyrianArabRepublic);
            countries.Add(Swaziland);
            countries.Add(TurksAndCaicosIslands);
            countries.Add(Chad);
            countries.Add(FrenchSouthernTerritories);
            countries.Add(Togo);
            countries.Add(Thailand);
            countries.Add(Tajikistan);
            countries.Add(Tokelau);
            countries.Add(TimorLeste);
            countries.Add(Turkmenistan);
            countries.Add(Tunisia);
            countries.Add(Tonga);
            countries.Add(Turkey);
            countries.Add(TrinidadAndTobago);
            countries.Add(Tuvalu);
            countries.Add(Taiwan);
            countries.Add(Tanzania);
            countries.Add(Ukraine);
            countries.Add(Uganda);
            countries.Add(UnitedStatesMinorOutlyingIslands);
            countries.Add(UnitedStates);
            countries.Add(Uruguay);
            countries.Add(Uzbekistan);
            countries.Add(HolySee);
            countries.Add(SaintVincentAndGrenadines);
            countries.Add(Venezuela);
            countries.Add(BritishVirginIslands);
            countries.Add(UnitedStatesVirginIslands);
            countries.Add(VietNam);
            countries.Add(Vanuatu);
            countries.Add(WallisAndFutuna);
            countries.Add(Samoa);
            countries.Add(Yemen);
            countries.Add(Mayotte);
            countries.Add(SouthAfrica);
            countries.Add(Zambia);
            countries.Add(Zimbabwe);
        }

        #endregion

        #region Public Static Methods

        public static ICountry GetCountryByCode(string code)
        {
            if (code == null)
                return Unknown;

            var upper = code.ToUpper();

            if (byAlpha2Code.ContainsKey(upper))
                return byAlpha2Code[upper];

            return byAlpha3Code.ContainsKey(upper) ? byAlpha3Code[upper] : Unknown;
        }

        public static ICountry GetCountryByTopLevelDomain(string topLevelDomain)
        {
            if (topLevelDomain == null)
                return Unknown;

            var lower = topLevelDomain.ToLower();

            return byTopLevelDomain.ContainsKey(lower) ? byTopLevelDomain[lower] : Unknown;
        }

        public static ICountry GetCountryByName(string name)
        {
            if (name == null)
                return Unknown;

            var upper = name.ToUpper();

            return byName.ContainsKey(upper) ? byName[upper] : Unknown;
        }

        public static ICountry GetCountryByNumber(int number)
        {
            return byNumber.ContainsKey(number) ? byNumber[number] : Unknown;
        }

        public static IEnumerable<ICountry> GetCountries()
        {
            return countries;
        }

        #endregion

        #region Countries

        public static readonly ICountry Unknown = new Country("XX", "XXX", 0, "Unknown Country", 1974, ".aa");
        public static readonly ICountry Andorra = new Country("AD", "AND", 20, "Andorra", 1974, ".ad");
        public static readonly ICountry UnitedArabEmirates = new Country("AE", "ARE", 784, "United Arab Emirates", 1974, ".ae");
        public static readonly ICountry Afghanistan = new Country("AF", "AFG", 4, "Afghanistan", 1974, ".af");
        public static readonly ICountry Antigua = new Country("AG", "ATG", 28, "Antigua and Barbuda", 1974, ".ag");
        public static readonly ICountry Anguilla = new Country("AI", "AIA", 660, "Anguilla", 1983, ".ai");
        public static readonly ICountry Albania = new Country("AL", "ALB", 8, "Albania", 1974, ".al");
        public static readonly ICountry Armenia = new Country("AM", "ARM", 51, "Armenia", 1992, ".am");
        public static readonly ICountry Angola = new Country("AO", "AGO", 24, "Angola", 1974, ".ao");
        public static readonly ICountry Antarctica = new Country("AQ", "ATA", 10, "Antarctica", 1974, ".aq");
        public static readonly ICountry Argentina = new Country("AR", "ARG", 32, "Argentina", 1974, ".ar");
        public static readonly ICountry AmericanSomoa = new Country("AS", "ASM", 16, "American Samoa", 1974, ".as");
        public static readonly ICountry Austria = new Country("AT", "AUT", 40, "Austria", 1974, ".at");
        public static readonly ICountry Australia = new Country("AU", "AUS", 36, "Australia", 1974, ".au");
        public static readonly ICountry Aruba = new Country("AW", "ABW", 533, "Aruba", 1986, ".aw");
        public static readonly ICountry AlandIslands = new Country("AX", "ALA", 248, "Åland Islands", 2004, ".ax");
        public static readonly ICountry Azerbaijan = new Country("AZ", "AZE", 31, "Azerbaijan", 1992, ".az");
        public static readonly ICountry Bosnia = new Country("BA", "Bosnia and Herzegovina", 1992, ".ba");
        public static readonly ICountry Barbados = new Country("BB", "Barbados", 1974, ".bb");
        public static readonly ICountry Bangladesh = new Country("BD", "Bangladesh", 1974, ".bd");
        public static readonly ICountry Belgium = new Country("BE", "Belgium", 1974, ".be");
        public static readonly ICountry BurkinaFaso = new Country("BF", "Burkina Faso", 1984, ".bf");
        public static readonly ICountry Bulgaria = new Country("BG", "Bulgaria", 1974, ".bg");
        public static readonly ICountry Bahrain = new Country("BH", "Bahrain", 1974, ".bh");
        public static readonly ICountry Burundi = new Country("BI", "Burundi", 1974, ".bi");
        public static readonly ICountry Benin = new Country("BJ", "Benin", 1977, ".bj");
        public static readonly ICountry SaintBarthelemy = new Country("BL", "Saint Barthélemy", 2007, ".bl");
        public static readonly ICountry Bermuda = new Country("BM", "Bermuda", 1974, ".bm");
        public static readonly ICountry Brunei = new Country("BN", "Brunei Darussalam", 1974, ".bn");
        public static readonly ICountry Bolivia = new Country("BO", "Bolivia, Plurinational State of", 1974, ".bo");
        public static readonly ICountry Bonaire = new Country("BQ", "Bonaire, Sint Eustatius and Saba", 2010, ".bq");
        public static readonly ICountry Brazil = new Country("BR", "Brazil", 1974, ".br");
        public static readonly ICountry Bahamas = new Country("BS", "Bahamas", 1974, ".bs");
        public static readonly ICountry Bhutan = new Country("BT", "Bhutan", 1974, ".bt");
        public static readonly ICountry BouvetIsland = new Country("BV", "Bouvet Island", 1974, ".bv");
        public static readonly ICountry Botswana = new Country("BW", "Botswana", 1974, ".bw");
        public static readonly ICountry Belarus = new Country("BY", "Belarus", 1974, ".by");
        public static readonly ICountry Belize = new Country("BZ", "Belize", 1974, ".bz");
        public static readonly ICountry Canada = new Country("CA", "Canada", 1974, ".ca");
        public static readonly ICountry CocosIslands = new Country("CC", "Cocos (Keeling) Islands", 1974, ".cc");
        public static readonly ICountry CongoDemocraticRepublic = new Country("CD", "Congo, the Democratic Republic of the", 1997, ".cd");
        public static readonly ICountry CentralAfricanRepublic = new Country("CF", "Central African Republic", 1974, ".cf");
        public static readonly ICountry Congo = new Country("CG", "Congo", 1974, ".cg");
        public static readonly ICountry Switzerland = new Country("CH", "Switzerland", 1974, ".ch");
        public static readonly ICountry CoteDIvoire = new Country("CI", "Côte d'Ivoire", 1974, ".ci");
        public static readonly ICountry CookIslands = new Country("CK", "Cook Islands", 1974, ".ck");
        public static readonly ICountry Chile = new Country("CL", "Chile", 1974, ".cl");
        public static readonly ICountry Cameroon = new Country("CM", "Cameroon", 1974, ".cm");
        public static readonly ICountry China = new Country("CN", "China", 1974, ".cn");
        public static readonly ICountry Colombia = new Country("CO", "Colombia", 1974, ".co");
        public static readonly ICountry CostaRica = new Country("CR", "Costa Rica", 1974, ".cr");
        public static readonly ICountry Cuba = new Country("CU", "Cuba", 1974, ".cu");
        public static readonly ICountry CapeVerde = new Country("CV", "Cape Verde", 1974, ".cv");
        public static readonly ICountry Curacao = new Country("CW", "Curaçao", 2010, ".cw");
        public static readonly ICountry ChristmasIslands = new Country("CX", "Christmas Island", 1974, ".cx");
        public static readonly ICountry Cyprus = new Country("CY", "Cyprus", 1974, ".cy");
        public static readonly ICountry CzechRepublic = new Country("CZ", "Czech Republic", 1993, ".cz");
        public static readonly ICountry Germany = new Country("DE", "Germany", 1974, ".de");
        public static readonly ICountry Djibouti = new Country("DJ", "Djibouti", 1977, ".dj");
        public static readonly ICountry Denmark = new Country("DK", "Denmark", 1974, ".dk");
        public static readonly ICountry Dominica = new Country("DM", "Dominica", 1974, ".dm");
        public static readonly ICountry DominicanRepublic = new Country("DO", "Dominican Republic", 1974, ".do");
        public static readonly ICountry Algeria = new Country("DZ", "Algeria", 1974, ".dz");
        public static readonly ICountry Ecuador = new Country("EC", "Ecuador", 1974, ".ec");
        public static readonly ICountry Estonia = new Country("EE", "Estonia", 1992, ".ee");
        public static readonly ICountry Egypt = new Country("EG", "Egypt", 1974, ".eg");
        public static readonly ICountry WesternSahara = new Country("EH", "Western Sahara", 1974, ".eh");
        public static readonly ICountry Eritrea = new Country("ER", "Eritrea", 1993, ".er");
        public static readonly ICountry Spain = new Country("ES", "Spain", 1974, ".es");
        public static readonly ICountry Ethiopia = new Country("ET", "Ethiopia", 1974, ".et");
        public static readonly ICountry Finland = new Country("FI", "Finland", 1974, ".fi");
        public static readonly ICountry Fiji = new Country("FJ", "Fiji", 1974, ".fj");
        public static readonly ICountry FalklandIslands = new Country("FK", "Falkland Islands (Malvinas)", 1974, ".fk");
        public static readonly ICountry Micronesia = new Country("FM", "Micronesia, Federated States of", 1986, ".fm");
        public static readonly ICountry FaroeIslands = new Country("FO", "Faroe Islands", 1974, ".fo");
        public static readonly ICountry France = new Country("FR", "France", 1974, ".fr");
        public static readonly ICountry Gabon = new Country("GA", "Gabon", 1974, ".ga");
        public static readonly ICountry UnitedKingdom = new Country("GB", "United Kingdom", 1974, ".uk");
        public static readonly ICountry Grenada = new Country("GD", "Grenada", 1974, ".gd");
        public static readonly ICountry Georgia = new Country("GE", "Georgia", 1992, ".ge");
        public static readonly ICountry FrenchGuiana = new Country("GF", "French Guiana", 1974, ".gf");
        public static readonly ICountry Guernsey = new Country("GG", "Guernsey", 2006, ".gg");
        public static readonly ICountry Ghana = new Country("GH", "Ghana", 1974, ".gh");
        public static readonly ICountry Gibraltar = new Country("GI", "Gibraltar", 1974, ".gi");
        public static readonly ICountry Greenland = new Country("GL", "Greenland", 1974, ".gl");
        public static readonly ICountry Gambia = new Country("GM", "Gambia", 1974, ".gm");
        public static readonly ICountry Guinea = new Country("GN", "Guinea", 1974, ".gn");
        public static readonly ICountry Guadeloupe = new Country("GP", "Guadeloupe", 1974, ".gp");
        public static readonly ICountry EquatorialGuinea = new Country("GQ", "Equatorial Guinea", 1974, ".gq");
        public static readonly ICountry Greece = new Country("GR", "Greece", 1974, ".gr");
        public static readonly ICountry South = new Country("GS", "South Georgia and the South Sandwich Islands", 1993, ".gs");
        public static readonly ICountry Guatemala = new Country("GT", "Guatemala", 1974, ".gt");
        public static readonly ICountry Guam = new Country("GU", "Guam", 1974, ".gu");
        public static readonly ICountry GuineaBissau = new Country("GW", "Guinea-Bissau", 1974, ".gw");
        public static readonly ICountry Guyana = new Country("GY", "Guyana", 1974, ".gy");
        public static readonly ICountry HongKong = new Country("HK", "Hong Kong", 1974, ".hk");
        public static readonly ICountry HeardIsland = new Country("HM", "Heard Island and McDonald Islands", 1974, ".hm");
        public static readonly ICountry Honduras = new Country("HN", "Honduras", 1974, ".hn");
        public static readonly ICountry Croatia = new Country("HR", "Croatia", 1992, ".hr");
        public static readonly ICountry Haiti = new Country("HT", "Haiti", 1974, ".ht");
        public static readonly ICountry Hungary = new Country("HU", "Hungary", 1974, ".hu");
        public static readonly ICountry Indonesia = new Country("ID", "Indonesia", 1974, ".id");
        public static readonly ICountry Ireland = new Country("IE", "Ireland", 1974, ".ie");
        public static readonly ICountry Israel = new Country("IL", "Israel", 1974, ".il");
        public static readonly ICountry IsleOfMan = new Country("IM", "Isle of Man", 2006, ".im");
        public static readonly ICountry India = new Country("IN", "India", 1974, ".in");
        public static readonly ICountry BritishIndianOceanTerritory = new Country("IO", "British Indian Ocean Territory", 1974, ".io");
        public static readonly ICountry Iraq = new Country("IQ", "Iraq", 1974, ".iq");
        public static readonly ICountry Iran = new Country("IR", "Iran, Islamic Republic of", 1974, ".ir");
        public static readonly ICountry Iceland = new Country("IS", "Iceland", 1974, ".is");
        public static readonly ICountry Italy = new Country("IT", "Italy", 1974, ".it");
        public static readonly ICountry Jersey = new Country("JE", "Jersey", 2006, ".je");
        public static readonly ICountry Jamaica = new Country("JM", "Jamaica", 1974, ".jm");
        public static readonly ICountry Jordan = new Country("JO", "Jordan", 1974, ".jo");
        public static readonly ICountry Japan = new Country("JP", "Japan", 1974, ".jp");
        public static readonly ICountry Kenya = new Country("KE", "Kenya", 1974, ".ke");
        public static readonly ICountry Kyrgyzstan = new Country("KG", "Kyrgyzstan", 1992, ".kg");
        public static readonly ICountry Cambodia = new Country("KH", "Cambodia", 1974, ".kh");
        public static readonly ICountry Kiribati = new Country("KI", "Kiribati", 1979, ".ki");
        public static readonly ICountry Comoros = new Country("KM", "Comoros", 1974, ".km");
        public static readonly ICountry SaintKittsAndNevis = new Country("KN", "Saint Kitts and Nevis", 1974, ".kn");
        public static readonly ICountry NorthKorea = new Country("KP", "Korea, Democratic People's Republic of", 1974, ".kp");
        public static readonly ICountry SouthKorea = new Country("KR", "Korea, Republic of", 1974, ".kr");
        public static readonly ICountry Kuwait = new Country("KW", "Kuwait", 1974, ".kw");
        public static readonly ICountry CaymanIslands = new Country("KY", "Cayman Islands", 1974, ".ky");
        public static readonly ICountry Kazakhstan = new Country("KZ", "Kazakhstan", 1992, ".kz");
        public static readonly ICountry LaoPeoplesDemocraticRepublic = new Country("LA", "Lao People's Democratic Republic", 1974, ".la");
        public static readonly ICountry Lebanon = new Country("LB", "Lebanon", 1974, ".lb");
        public static readonly ICountry SaintLucia = new Country("LC", "Saint Lucia", 1974, ".lc");
        public static readonly ICountry Liechtenstein = new Country("LI", "Liechtenstein", 1974, ".li");
        public static readonly ICountry SriLanka = new Country("LK", "Sri Lanka", 1974, ".lk");
        public static readonly ICountry Liberia = new Country("LR", "Liberia", 1974, ".lr");
        public static readonly ICountry Lesotho = new Country("LS", "Lesotho", 1974, ".ls");
        public static readonly ICountry Lithuania = new Country("LT", "Lithuania", 1992, ".lt");
        public static readonly ICountry Luxembourg = new Country("LU", "Luxembourg", 1974, ".lu");
        public static readonly ICountry Latvia = new Country("LV", "Latvia", 1992, ".lv");
        public static readonly ICountry Libya = new Country("LY", "Libyan Arab Jamahiriya", 1974, ".ly");
        public static readonly ICountry Morocco = new Country("MA", "Morocco", 1974, ".ma");
        public static readonly ICountry Monaco = new Country("MC", "Monaco", 1974, ".mc");
        public static readonly ICountry Moldova = new Country("MD", "Moldova, Republic of", 1992, ".md");
        public static readonly ICountry Montenego = new Country("ME", "Montenegro", 2006, ".me");
        public static readonly ICountry SaintMartin = new Country("MF", "Saint Martin (French part)", 2007, ".mf");
        public static readonly ICountry Madagascar = new Country("MG", "Madagascar", 1974, ".mg");
        public static readonly ICountry MarshallIslands = new Country("MH", "Marshall Islands", 1986, ".mh");
        public static readonly ICountry Macedonia = new Country("MK", "Macedonia, the former Yugoslav Republic of", 1993, ".mk");
        public static readonly ICountry Mali = new Country("ML", "Mali", 1974, ".ml");
        public static readonly ICountry Myanmar = new Country("MM", "Myanmar", 1989, ".mm");
        public static readonly ICountry Mongolia = new Country("MN", "Mongolia", 1974, ".mn");
        public static readonly ICountry Macao = new Country("MO", "Macao", 1974, ".mo");
        public static readonly ICountry NorthernMarianaIslands = new Country("MP", "Northern Mariana Islands", 1986, ".mp");
        public static readonly ICountry Martinique = new Country("MQ", "Martinique", 1974, ".mq");
        public static readonly ICountry Mauritania = new Country("MR", "Mauritania", 1974, ".mr");
        public static readonly ICountry Montserrat = new Country("MS", "Montserrat", 1974, ".ms");
        public static readonly ICountry Malta = new Country("MT", "Malta", 1974, ".mt");
        public static readonly ICountry Mauitius = new Country("MU", "Mauritius", 1974, ".mu");
        public static readonly ICountry Maldives = new Country("MV", "Maldives", 1974, ".mv");
        public static readonly ICountry Malawi = new Country("MW", "Malawi", 1974, ".mw");
        public static readonly ICountry Mexico = new Country("MX", "Mexico", 1974, ".mx");
        public static readonly ICountry Malasia = new Country("MY", "Malaysia", 1974, ".my");
        public static readonly ICountry Mozambique = new Country("MZ", "Mozambique", 1974, ".mz");
        public static readonly ICountry Namibia = new Country("NA", "Namibia", 1974, ".na");
        public static readonly ICountry NewCaledonia = new Country("NC", "New Caledonia", 1974, ".nc");
        public static readonly ICountry Niger = new Country("NE", "Niger", 1974, ".ne");
        public static readonly ICountry NorfolkIsland = new Country("NF", "Norfolk Island", 1974, ".nf");
        public static readonly ICountry Nigeria = new Country("NG", "Nigeria", 1974, ".ng");
        public static readonly ICountry Nicaragua = new Country("NI", "Nicaragua", 1974, ".ni");
        public static readonly ICountry Netherlands = new Country("NL", "Netherlands", 1974, ".nl");
        public static readonly ICountry Norway = new Country("NO", "Norway", 1974, ".no");
        public static readonly ICountry Nepal = new Country("NP", "Nepal", 1974, ".np");
        public static readonly ICountry Nauru = new Country("NR", "Nauru", 1974, ".nr");
        public static readonly ICountry Niue = new Country("NU", "Niue", 1974, ".nu");
        public static readonly ICountry NewZealand = new Country("NZ", "New Zealand", 1974, ".nz");
        public static readonly ICountry Oman = new Country("OM", "Oman", 1974, ".om");
        public static readonly ICountry Panama = new Country("PA", "Panama", 1974, ".pa");
        public static readonly ICountry Peru = new Country("PE", "Peru", 1974, ".pe");
        public static readonly ICountry FrenchPolynesia = new Country("PF", "French Polynesia", 1974, ".pf");
        public static readonly ICountry PapuaNewGuinea = new Country("PG", "Papua New Guinea", 1974, ".pg");
        public static readonly ICountry Philippines = new Country("PH", "Philippines", 1974, ".ph");
        public static readonly ICountry Pakistan = new Country("PK", "Pakistan", 1974, ".pk");
        public static readonly ICountry Poland = new Country("PL", "Poland", 1974, ".pl");
        public static readonly ICountry SaintPierreAndMiquelon = new Country("PM", "Saint Pierre and Miquelon", 1974, ".pm");
        public static readonly ICountry Pitcairn = new Country("PN", "Pitcairn", 1974, ".pn");
        public static readonly ICountry PuertoRico = new Country("PR", "Puerto Rico", 1974, ".pr");
        public static readonly ICountry OccupiedPalestinianTerritory = new Country("PS", "Palestinian Territory, Occupied", 1999, ".ps");
        public static readonly ICountry Portugal = new Country("PT", "Portugal", 1974, ".pt");
        public static readonly ICountry Palau = new Country("PW", "Palau", 1986, ".pw");
        public static readonly ICountry Paraguay = new Country("PY", "Paraguay", 1974, ".py");
        public static readonly ICountry Qatar = new Country("QA", "Qatar", 1974, ".qa");
        public static readonly ICountry Reunion = new Country("RE", "Réunion", 1974, ".re");
        public static readonly ICountry Romania = new Country("RO", "Romania", 1974, ".ro");
        public static readonly ICountry Serbia = new Country("RS", "Serbia", 2006, ".rs");
        public static readonly ICountry RussianFederation = new Country("RU", "Russian Federation", 1992, ".ru");
        public static readonly ICountry Rwanda = new Country("RW", "Rwanda", 1974, ".rw");
        public static readonly ICountry SaudiArabia = new Country("SA", "Saudi Arabia", 1974, ".sa");
        public static readonly ICountry SolomonIslands = new Country("SB", "Solomon Islands", 1974, ".sb");
        public static readonly ICountry Seychelles = new Country("SC", "Seychelles", 1974, ".sc");
        public static readonly ICountry Sudan = new Country("SD", "Sudan", 1974, ".sd");
        public static readonly ICountry Sweden = new Country("SE", "Sweden", 1974, ".se");
        public static readonly ICountry Singapore = new Country("SG", "Singapore", 1974, ".sg");
        public static readonly ICountry SaintHelena = new Country("SH", "Saint Helena, Ascension and Tristan da Cunha", 1974, ".sh");
        public static readonly ICountry Slovenia = new Country("SI", "Slovenia", 1992, ".si");
        public static readonly ICountry SvalbardAndJanMayen = new Country("SJ", "Svalbard and Jan Mayen", 1974, ".sj");
        public static readonly ICountry Slovakia = new Country("SK", "Slovakia", 1993, ".sk");
        public static readonly ICountry SierraLeone = new Country("SL", "Sierra Leone", 1974, ".sl");
        public static readonly ICountry SanMarino = new Country("SM", "San Marino", 1974, ".sm");
        public static readonly ICountry Senegal = new Country("SN", "Senegal", 1974, ".sn");
        public static readonly ICountry Somalia = new Country("SO", "Somalia", 1974, ".so");
        public static readonly ICountry Suriname = new Country("SR", "Suriname", 1974, ".sr");
        public static readonly ICountry SaoTomeAndPrincipe = new Country("ST", "Sao Tome and Principe", 1974, ".st");
        public static readonly ICountry ElSalvador = new Country("SV", "El Salvador", 1974, ".sv");
        public static readonly ICountry SintMaarten = new Country("SX", "Sint Maarten (Dutch part)", 2010, ".sx");
        public static readonly ICountry SyrianArabRepublic = new Country("SY", "Syrian Arab Republic", 1974, ".sy");
        public static readonly ICountry Swaziland = new Country("SZ", "Swaziland", 1974, ".sz");
        public static readonly ICountry TurksAndCaicosIslands = new Country("TC", "Turks and Caicos Islands", 1974, ".tc");
        public static readonly ICountry Chad = new Country("TD", "Chad", 1974, ".td");
        public static readonly ICountry FrenchSouthernTerritories = new Country("TF", "French Southern Territories", 1979, ".tf");
        public static readonly ICountry Togo = new Country("TG", "Togo", 1974, ".tg");
        public static readonly ICountry Thailand = new Country("TH", "Thailand", 1974, ".th");
        public static readonly ICountry Tajikistan = new Country("TJ", "Tajikistan", 1992, ".tj");
        public static readonly ICountry Tokelau = new Country("TK", "Tokelau", 1974, ".tk");
        public static readonly ICountry TimorLeste = new Country("TL", "Timor-Leste", 2002, ".tl");
        public static readonly ICountry Turkmenistan = new Country("TM", "Turkmenistan", 1992, ".tm");
        public static readonly ICountry Tunisia = new Country("TN", "Tunisia", 1974, ".tn");
        public static readonly ICountry Tonga = new Country("TO", "Tonga", 1974, ".to");
        public static readonly ICountry Turkey = new Country("TR", "Turkey", 1974, ".tr");
        public static readonly ICountry TrinidadAndTobago = new Country("TT", "Trinidad and Tobago", 1974, ".tt");
        public static readonly ICountry Tuvalu = new Country("TV", "Tuvalu", 1979, ".tv");
        public static readonly ICountry Taiwan = new Country("TW", "Taiwan, Province of China", 1974, ".tw");
        public static readonly ICountry Tanzania = new Country("TZ", "Tanzania, United Republic of", 1974, ".tz");
        public static readonly ICountry Ukraine = new Country("UA", "Ukraine", 1974, ".ua");
        public static readonly ICountry Uganda = new Country("UG", "Uganda", 1974, ".ug");
        public static readonly ICountry UnitedStatesMinorOutlyingIslands = new Country("UM", "United States Minor Outlying Islands", 1986, ".um");
        public static readonly ICountry UnitedStates = new Country("US", "United States", 1974, ".us");
        public static readonly ICountry Uruguay = new Country("UY", "Uruguay", 1974, ".uy");
        public static readonly ICountry Uzbekistan = new Country("UZ", "Uzbekistan", 1992, ".uz");
        public static readonly ICountry HolySee = new Country("VA", "Holy See (Vatican City State)", 1974, ".va");
        public static readonly ICountry SaintVincentAndGrenadines = new Country("VC", "Saint Vincent and the Grenadines", 1974, ".vc");
        public static readonly ICountry Venezuela = new Country("VE", "Venezuela, Bolivarian Republic of", 1974, ".ve");
        public static readonly ICountry BritishVirginIslands = new Country("VG", "Virgin Islands, British", 1974, ".vg");
        public static readonly ICountry UnitedStatesVirginIslands = new Country("VI", "Virgin Islands, U.S.", 1974, ".vi");
        public static readonly ICountry VietNam = new Country("VN", "Viet Nam", 1974, ".vn");
        public static readonly ICountry Vanuatu = new Country("VU", "Vanuatu", 1980, ".vu");
        public static readonly ICountry WallisAndFutuna = new Country("WF", "Wallis and Futuna", 1974, ".wf");
        public static readonly ICountry Samoa = new Country("WS", "Samoa", 1974, ".ws");
        public static readonly ICountry Yemen = new Country("YE", "Yemen", 1974, ".ye");
        public static readonly ICountry Mayotte = new Country("YT", "Mayotte", 1993, ".yt");
        public static readonly ICountry SouthAfrica = new Country("ZA", "South Africa", 1974, ".za");
        public static readonly ICountry Zambia = new Country("ZM", "Zambia", 1974, ".zm");
        public static readonly ICountry Zimbabwe = new Country("ZW", "Zimbabwe", 1980, ".zw");

        #endregion
    }
}
