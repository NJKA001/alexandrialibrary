using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Iso
{
    public class Language
        : ILanguage
    {
        private Language(string alpha3Code, string name)
            : this(alpha3Code, string.Empty, string.Empty, name)
        {
        }

        private Language(string alpha3Code, string alpha2Code, string name)
            : this(alpha3Code, string.Empty, alpha2Code, name)
        {

        }

        public Language(string alpha3Code, string alpha3TermCode, string alpha2Code, string name)
        {
            this.alpha3Code = alpha3Code;
            this.alpha3TermCode = alpha3TermCode;
            this.alpha2Code = alpha2Code;
            this.name = name;
        }

        private readonly string alpha3Code;
        private readonly string alpha3TermCode;
        private readonly string alpha2Code;
        private readonly string name;

        #region ILanguage Members

        /// <summary>
        /// The 3-letter alphabetical (Bibliographic) code for ISO 639-2
        /// </summary>
        public string Alpha3Code
        {
            get { return alpha3Code; }
        }

        /// <summary>
        /// The 3-letter alphabetical (Terminology) code for ISO 639-2
        /// </summary>
        /// <remarks>optional - an empty string indicates undefined</remarks>
        public string Alpha3TermCode
        {
            get { return alpha3TermCode; }
        }

        /// <summary>
        /// The 2-letter alphabetical code for ISO 639-1
        /// </summary>
        /// <remarks>optional - an empty string indicates undefined</remarks>
        public string Alpha2Code
        {
            get { return alpha2Code; }
        }

        /// <summary>
        /// The English name of the language
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            //NOTE: This algorithm is take from W3C IETF RFC 3066 and their use of ISO 639-1 and 639-2 language codes
            //http://tools.ietf.org/html/rfc3066

            //Use the ISO 639-1 alpha-2 code where present
            if (!string.IsNullOrEmpty(alpha2Code))
                return alpha2Code;

            //Use the ISO 639-2 alpha-3 terminology code where present
            if (!string.IsNullOrEmpty(alpha3TermCode))
                return alpha3TermCode;

            //Use the ISO 639-2 alpha-3 bibliographic code (which will always be defined) in all other cases
            return alpha3Code;
        }

        static Language()
        {
            InitializeLanguages();

            foreach (var language in languages)
            {
                if (!string.IsNullOrEmpty(language.Alpha2Code))
                    byAlpha2Code.Add(language.Alpha2Code, language);

                if (!string.IsNullOrEmpty(language.Alpha3TermCode))
                    byAlpha3TermCode.Add(language.Alpha3TermCode, language);

                byAlpha3Code.Add(language.Alpha3Code, language);
                byName.Add(language.Name.ToUpper(), language);
            }
        }

        private static readonly IList<ILanguage> languages = new List<ILanguage>();
        private static readonly IDictionary<string, ILanguage> byAlpha2Code = new Dictionary<string, ILanguage>();
        private static readonly IDictionary<string, ILanguage> byAlpha3Code = new Dictionary<string, ILanguage>();
        private static readonly IDictionary<string, ILanguage> byAlpha3TermCode = new Dictionary<string, ILanguage>();
        private static readonly IDictionary<string, ILanguage> byName = new Dictionary<string, ILanguage>();

        #region InitializeLanguages

        private static void InitializeLanguages()
        {
            languages.Add(Afar);
            languages.Add(Abkhazian);
            languages.Add(Achinese);
            languages.Add(Acoli);
            languages.Add(Adangme);
            languages.Add(Adyghe);
            languages.Add(AfroAsiatic);
            languages.Add(Afrihili);
            languages.Add(Afrikaans);
            languages.Add(Ainu);
            languages.Add(Akan);
            languages.Add(Akkadian);
            languages.Add(Albanian);
            languages.Add(Aleut);
            languages.Add(Algonquian);
            languages.Add(SouthernAltai);
            languages.Add(Amharic);
            languages.Add(Amis);
            languages.Add(OldEnglish);
            languages.Add(Angika);
            languages.Add(Apache);
            languages.Add(Arabic);
            languages.Add(Aramaic);
            languages.Add(Aragonese);
            languages.Add(Armenian);
            languages.Add(Mapudungun);
            languages.Add(Arapaho);
            languages.Add(ArtificialLanguages);
            languages.Add(Arawak);
            languages.Add(Assamese);
            languages.Add(Asturian);
            languages.Add(AthapascanLanguages);
            languages.Add(AustrialianLanguages);
            languages.Add(Avaric);
            languages.Add(Avestan);
            languages.Add(Awadhia);
            languages.Add(Aymara);
            languages.Add(Azerbaijani);
            languages.Add(Banda);
            languages.Add(Bamileke);
            languages.Add(Bashkir);
            languages.Add(Baluci);
            languages.Add(Bambara);
            languages.Add(Balinese);
            languages.Add(Basque);
            languages.Add(Basa);
            languages.Add(Baltic);
            languages.Add(Beja);
            languages.Add(Belarusian);
            languages.Add(Bemba);
            languages.Add(Bengali);
            languages.Add(Berber);
            languages.Add(Bhojpuri);
            languages.Add(Bihari);
            languages.Add(Bikol);
            languages.Add(Bini);
            languages.Add(Bislama);
            languages.Add(Siksika);
            languages.Add(Bantu);
            languages.Add(Tibetan);
            languages.Add(Bosnian);
            languages.Add(Braj);
            languages.Add(Breton);
            languages.Add(Batak);
            languages.Add(Buriat);
            languages.Add(Buginese);
            languages.Add(Bulgarian);
            languages.Add(Bunun);
            languages.Add(Burmese);
            languages.Add(Bilin);
            languages.Add(Caddo);
            languages.Add(CentralAmericanIndianLanguages);
            languages.Add(Galibi);
            languages.Add(Catalan);
            languages.Add(Caucasian);
            languages.Add(Cebuano);
            languages.Add(Celtic);
            languages.Add(Czech);
            languages.Add(Chamorro);
            languages.Add(Chibcha);
            languages.Add(Chechen);
            languages.Add(Chagatai);
            languages.Add(Chinese);
            languages.Add(Chuukese);
            languages.Add(Mari);
            languages.Add(Chinook);
            languages.Add(Choctaw);
            languages.Add(Chipewyan);
            languages.Add(Cherokee);
            languages.Add(OldBulgarian);
            languages.Add(Chuvash);
            languages.Add(Cheyenne);
            languages.Add(Chamic);
            languages.Add(Coptic);
            languages.Add(Cornish);
            languages.Add(Corsican);
            languages.Add(EnglishCreoles);
            languages.Add(FrenchCreoles);
            languages.Add(PortugueseCreoles);
            languages.Add(Cree);
            languages.Add(CrimeanTatar);
            languages.Add(Creoles);
            languages.Add(Kashubian);
            languages.Add(Cushitic);
            languages.Add(Welsh);
            languages.Add(Dakota);
            languages.Add(Danish);
            languages.Add(Dargwa);
            languages.Add(LandDayak);
            languages.Add(Delaware);
            languages.Add(Slave);
            languages.Add(Dogrib);
            languages.Add(Dinka);
            languages.Add(Divehi);
            languages.Add(Dogri);
            languages.Add(Dravidian);
            languages.Add(LowerSorbian);
            languages.Add(Duala);
            languages.Add(MiddleDutch);
            languages.Add(Dutch);
            languages.Add(Dyula);
            languages.Add(Dzongkha);
            languages.Add(Efik);
            languages.Add(AncientEgyptian);
            languages.Add(Ekajuk);
            languages.Add(Greek);
            languages.Add(Elamite);
            languages.Add(English);
            languages.Add(MiddleEnglish);
            languages.Add(Esperanto);
            languages.Add(Estonian);
            languages.Add(Ewe);
            languages.Add(Ewondo);
            languages.Add(Fang);
            languages.Add(Faroese);
            languages.Add(Persian);
            languages.Add(Fanti);
            languages.Add(Fijian);
            languages.Add(Filipino);
            languages.Add(Finnish);
            languages.Add(FinnoUgurian);
            languages.Add(Fon);
            languages.Add(French);
            languages.Add(MiddleFrench);
            languages.Add(OldFrench);
            languages.Add(NorthernFrisian);
            languages.Add(EasternFrisian);
            languages.Add(WesternFrisian);
            languages.Add(Fulah);
            languages.Add(Friulian);
            languages.Add(Ga);
            languages.Add(Gayo);
            languages.Add(Gbaya);
            languages.Add(GermanicLanguages);
            languages.Add(Georgian);
            languages.Add(German);
            languages.Add(Geez);
            languages.Add(Gilbertese);
            languages.Add(Gaelic);
            languages.Add(Irish);
            languages.Add(Galician);
            languages.Add(Manx);
            languages.Add(MiddleHighGerman);
            languages.Add(OldHighGerman);
            languages.Add(Gondi);
            languages.Add(Gorontalo);
            languages.Add(Gothic);
            languages.Add(Grebo);
            languages.Add(AncientGreek);
            languages.Add(Guarani);
            languages.Add(SwissGerman);
            languages.Add(Gujarati);
            languages.Add(Gwichin);
            languages.Add(Haida);
            languages.Add(Haitian);
            languages.Add(Hakka);
            languages.Add(Hausa);
            languages.Add(Hawaiian);
            languages.Add(Hebrew);
            languages.Add(Herero);
            languages.Add(Hiligaynon);
            languages.Add(Himachali);
            languages.Add(Hindi);
            languages.Add(Hittite);
            languages.Add(Hmong);
            languages.Add(HiriMotu);
            languages.Add(Croatian);
            languages.Add(UpperSorbian);
            languages.Add(Hungarian);
            languages.Add(Hupa);
            languages.Add(Iban);
            languages.Add(Igbo);
            languages.Add(Ido);
            languages.Add(SichuanYi);
            languages.Add(Ijo);
            languages.Add(Inuktitut);
            languages.Add(InterlingueOccidental);
            languages.Add(Iloko);
            languages.Add(Interlingua);
            languages.Add(Indic);
            languages.Add(Indonesian);
            languages.Add(IndoEuropean);
            languages.Add(Ingush);
            languages.Add(Inupiaq);
            languages.Add(Iranian);
            languages.Add(Iroquoian);
            languages.Add(Icelandic);
            languages.Add(Italian);
            languages.Add(Javanese);
            languages.Add(Lojban);
            languages.Add(Japanese);
            languages.Add(JudeoPersian);
            languages.Add(JudeoArabic);
            languages.Add(KaraKalpak);
            languages.Add(Kabyle);
            languages.Add(Kachin);
            languages.Add(Kalallisut);
            languages.Add(Kamba);
            languages.Add(Kannada);
            languages.Add(Karen);
            languages.Add(Kashmiri);
            languages.Add(Kanuri);
            languages.Add(Kawi);
            languages.Add(Kazakh);
            languages.Add(Kabardian);
            languages.Add(Khasi);
            languages.Add(Khoisan);
            languages.Add(CentralKhmer);
            languages.Add(Khotanese);
            languages.Add(Kikuyu);
            languages.Add(Kinyarwanda);
            languages.Add(Kirghiz);
            languages.Add(Kimbundu);
            languages.Add(Konkani);
            languages.Add(Komi);
            languages.Add(Kongo);
            languages.Add(Korean);
            languages.Add(Kosraean);
            languages.Add(Kpelle);
            languages.Add(Karachay);
            languages.Add(Karelian);
            languages.Add(Kru);
            languages.Add(Kurukh);
            languages.Add(Kuanyama);
            languages.Add(Kumyk);
            languages.Add(Kurdish);
            languages.Add(Kutenai);
            languages.Add(Ladino);
            languages.Add(Lahnda);
            languages.Add(Lamba);
            languages.Add(Lao);
            languages.Add(Latin);
            languages.Add(Latvian);
            languages.Add(Lezghian);
            languages.Add(Limburgan);
            languages.Add(Lingala);
            languages.Add(Lithuanian);
            languages.Add(Mongo);
            languages.Add(Lozi);
            languages.Add(Luxembourgish);
            languages.Add(LubaLulua);
            languages.Add(LubaKatanga);
            languages.Add(Ganda);
            languages.Add(Luiseno);
            languages.Add(Lunda);
            languages.Add(Luo);
            languages.Add(Lushai);
            languages.Add(Macedonian);
            languages.Add(Madurese);
            languages.Add(Magahi);
            languages.Add(Marshallese);
            languages.Add(Maithili);
            languages.Add(Makasar);
            languages.Add(Malayalam);
            languages.Add(Mandingo);
            languages.Add(Maori);
            languages.Add(Austronesian);
            languages.Add(Marathi);
            languages.Add(Masai);
            languages.Add(Malay);
            languages.Add(Moksha);
            languages.Add(Mandar);
            languages.Add(Mende);
            languages.Add(MiddleIrish);
            languages.Add(Micmac);
            languages.Add(Minangkabau);
            languages.Add(UncodedLanguages);
            languages.Add(MonKhmer);
            languages.Add(Malagasy);
            languages.Add(Maltese);
            languages.Add(Manchu);
            languages.Add(Manipuri);
            languages.Add(Manobo);
            languages.Add(Mohawk);
            languages.Add(Mongolian);
            languages.Add(Mossi);
            languages.Add(MultipleLanguages);
            languages.Add(MundaLanguages);
            languages.Add(Creek);
            languages.Add(Mirandese);
            languages.Add(Marwari);
            languages.Add(Mayan);
            languages.Add(Erzya);
            languages.Add(Nahuatl);
            languages.Add(NorthAmericanIndian);
            languages.Add(Neapolitan);
            languages.Add(Nauru);
            languages.Add(Navajo);
            languages.Add(SouthNdebele);
            languages.Add(NorthNdebele);
            languages.Add(Ndonga);
            languages.Add(LowGerman);
            languages.Add(Nepali);
            languages.Add(NepalBhasa);
            languages.Add(Nias);
            languages.Add(NigerKordofanian);
            languages.Add(Niuean);
            languages.Add(NorwegianNynorsk);
            languages.Add(NorwegianBokmal);
            languages.Add(Nogai);
            languages.Add(OldNorse);
            languages.Add(Norwegian);
            languages.Add(NKo);
            languages.Add(Pedi);
            languages.Add(Nubian);
            languages.Add(ClassicalNewari);
            languages.Add(Chichewa);
            languages.Add(Nyamwezi);
            languages.Add(Nyankole);
            languages.Add(Nyoro);
            languages.Add(Nzima);
            languages.Add(Occitan);
            languages.Add(Ojibway);
            languages.Add(Oriya);
            languages.Add(Oromo);
            languages.Add(Osage);
            languages.Add(Ossetian);
            languages.Add(OttomanTurkish);
            languages.Add(Otomian);
            languages.Add(Papuan);
            languages.Add(Pangasinan);
            languages.Add(Pahlavi);
            languages.Add(Paiwan);
            languages.Add(Pampanga);
            languages.Add(Panjabi);
            languages.Add(Papiamento);
            languages.Add(Palauan);
            languages.Add(OldPersian);
            languages.Add(PhilippineLanguages);
            languages.Add(Phoenician);
            languages.Add(Pali);
            languages.Add(Polish);
            languages.Add(Pohnpeian);
            languages.Add(Portuguese);
            languages.Add(Prakrit);
            languages.Add(OldProvencal);
            languages.Add(Pushto);
            languages.Add(Quechua);
            languages.Add(Rajasthani);
            languages.Add(Rapanui);
            languages.Add(Rarotongan);
            languages.Add(RomanceLanguages);
            languages.Add(Romansh);
            languages.Add(Romany);
            languages.Add(Romanian);
            languages.Add(Rundi);
            languages.Add(Aromanian);
            languages.Add(Russian);
            languages.Add(Sandawe);
            languages.Add(Sango);
            languages.Add(Yakut);
            languages.Add(SouthAmericanIndianLanguages);
            languages.Add(Salishan);
            languages.Add(SamaritanAramaic);
            languages.Add(Sanskrit);
            languages.Add(Sasak);
            languages.Add(Santali);
            languages.Add(Sicilian);
            languages.Add(Scots);
            languages.Add(Selkup);
            languages.Add(Semitic);
            languages.Add(OldIrish);
            languages.Add(SignLanguages);
            languages.Add(Shan);
            languages.Add(Sidamo);
            languages.Add(Sinhala);
            languages.Add(Siouan);
            languages.Add(SinoTibetan);
            languages.Add(Slavic);
            languages.Add(Slovak);
            languages.Add(Slovenian);
            languages.Add(SouthernSami);
            languages.Add(NorthernSami);
            languages.Add(SamiLanguages);
            languages.Add(LuleSami);
            languages.Add(InariSami);
            languages.Add(Samoan);
            languages.Add(SkoltSami);
            languages.Add(Shona);
            languages.Add(Sindhi);
            languages.Add(Sonike);
            languages.Add(Sogdian);
            languages.Add(Somali);
            languages.Add(Songhai);
            languages.Add(Sotho);
            languages.Add(Spanish);
            languages.Add(Sardinian);
            languages.Add(Sranan);
            languages.Add(Serbian);
            languages.Add(Serer);
            languages.Add(NiloSaharan);
            languages.Add(Swati);
            languages.Add(Sukuma);
            languages.Add(Sudanese);
            languages.Add(Susu);
            languages.Add(Sumerian);
            languages.Add(Swahili);
            languages.Add(Swedish);
            languages.Add(ClassicalSyriac);
            languages.Add(Syriac);
            languages.Add(Tahitian);
            languages.Add(TaiLanguages);
            languages.Add(Tamil);
            languages.Add(Tao);
            languages.Add(Tatar);
            languages.Add(Tayal);
            languages.Add(Teluga);
            languages.Add(Timne);
            languages.Add(Tereno);
            languages.Add(Tetum);
            languages.Add(Tajik);
            languages.Add(Tagalog);
            languages.Add(Thai);
            languages.Add(Tigre);
            languages.Add(Tigrinya);
            languages.Add(Tiv);
            languages.Add(Tokelau);
            languages.Add(Klingon);
            languages.Add(Tlingit);
            languages.Add(Tamashek);
            languages.Add(TongaNyasa);
            languages.Add(Tonga);
            languages.Add(TokPisin);
            languages.Add(Tsimshian);
            languages.Add(Tswana);
            languages.Add(Tsonga);
            languages.Add(Tsou);
            languages.Add(Turkmen);
            languages.Add(Tumbuka);
            languages.Add(Tupi);
            languages.Add(Turkish);
            languages.Add(Altaic);
            languages.Add(Tuvalu);
            languages.Add(Twi);
            languages.Add(Tuvian);
            languages.Add(Udmurt);
            languages.Add(Ugaritic);
            languages.Add(Uighur);
            languages.Add(Ukrainian);
            languages.Add(Umbundu);
            languages.Add(Undetermined);
            languages.Add(Urdu);
            languages.Add(Uzbek);
            languages.Add(Vai);
            languages.Add(Venda);
            languages.Add(Vietnamese);
            languages.Add(Volapuk);
            languages.Add(Votic);
            languages.Add(Wakashan);
            languages.Add(Walaitta);
            languages.Add(Waray);
            languages.Add(Washo);
            languages.Add(Sorbian);
            languages.Add(Walloon);
            languages.Add(Wolof);
            languages.Add(Kalmyk);
            languages.Add(Xhosa);
            languages.Add(Yao);
            languages.Add(Yapese);
            languages.Add(Yiddish);
            languages.Add(Yor);
            languages.Add(Yupik);
            languages.Add(Zapotec);
            languages.Add(Bliss);
            languages.Add(Zenaga);
            languages.Add(Zhuang);
            languages.Add(Zande);
            languages.Add(Zulu);
            languages.Add(Zuni);
            languages.Add(NotApplicable);
            languages.Add(Zaza);

            languages.Add(BelgianFrenchSignLanguage);
            languages.Add(BelgianFlemishSignLanguage);
            languages.Add(SwissGermanSignLanguage);
            languages.Add(MandarinChinese);
            languages.Add(MinanHokkien);
            languages.Add(Xiang);
        }

        #endregion

        #region Public Static Members

        public static ILanguage GetLanguageByCode(string code)
        {
            if (code == null)
                return Undetermined;

            var lower = code.ToLower();

            if (byAlpha2Code.ContainsKey(lower))
                return byAlpha2Code[lower];

            if (byAlpha3TermCode.ContainsKey(lower))
                return byAlpha3TermCode[lower];

            return byAlpha3Code.ContainsKey(lower) ? byAlpha3Code[lower] : Undetermined;
        }

        public static ILanguage GetLanguageByName(string name)
        {
            if (name == null)
                return Undetermined;

            var upper = name.ToUpper();

            return byName.ContainsKey(upper) ? byName[upper] : Undetermined;
        }

        public static IEnumerable<ILanguage> GetLanguages()
        {
            return languages;
        }

        #endregion

        #region Languages

        public static readonly ILanguage Afar = new Language("aar", "aa", "Afar");
        public static readonly ILanguage Abkhazian = new Language("abk", "ab", "Abkhazian");
        public static readonly ILanguage Achinese = new Language("ace", "Achinese");
        public static readonly ILanguage Acoli = new Language("ach","Acoli");
        public static readonly ILanguage Adangme = new Language("ada","Adangme");
        public static readonly ILanguage Adyghe = new Language("ady","Adyghe; Adygei");
        public static readonly ILanguage AfroAsiatic = new Language("afa","Afro-Asiatic languages");
        public static readonly ILanguage Afrihili = new Language("afh","Afrihili");
        public static readonly ILanguage Afrikaans = new Language("afr","af","Afrikaans");
        public static readonly ILanguage Ainu = new Language("ain","Ainu");
        public static readonly ILanguage Akan = new Language("aka","ak","Akan");
        public static readonly ILanguage Akkadian = new Language("akk","Akkadian");
        public static readonly ILanguage Albanian = new Language("alb", "sqi", "sq", "Albanian");
        public static readonly ILanguage Aleut = new Language("ale","Aleut");
        public static readonly ILanguage Algonquian = new Language("alg","Algonquian languages");
        public static readonly ILanguage SouthernAltai = new Language("alt","Southern Altai");
        public static readonly ILanguage Amharic = new Language("amh","am","Amharic");
        public static readonly ILanguage Amis = new Language("ami", "Amis");
        public static readonly ILanguage OldEnglish = new Language("ang", "English, Old (ca.450-1100)");
        public static readonly ILanguage Angika = new Language("anp","Angika");
        public static readonly ILanguage Apache = new Language("apa","Apache languages");
        public static readonly ILanguage Arabic = new Language("ara","ar","Arabic");
        public static readonly ILanguage Aramaic = new Language("arc","Official Aramaic (700-300 BCE)");
        public static readonly ILanguage Aragonese = new Language("arg","an","Aragonese");
        public static readonly ILanguage Armenian = new Language("arm", "hye", "hy", "Armenian");
        public static readonly ILanguage Mapudungun = new Language("arn","Mapudungun; Mapuche");
        public static readonly ILanguage Arapaho = new Language("arp","Arapaho");
        public static readonly ILanguage ArtificialLanguages = new Language("art","Artificial languages");
        public static readonly ILanguage Arawak = new Language("arw","Arawak");
        public static readonly ILanguage Assamese = new Language("asm","as","Assamese");
        public static readonly ILanguage Asturian = new Language("ast","Asturian; Bable; Leonese; Asturleonese");
        public static readonly ILanguage AthapascanLanguages = new Language("ath","Athapascan languages");
        public static readonly ILanguage AustrialianLanguages = new Language("aus","Australian languages");
        public static readonly ILanguage Avaric = new Language("ava","av","Avaric");
        public static readonly ILanguage Avestan = new Language("ave","ae","Avestan");
        public static readonly ILanguage Awadhia = new Language("awa","Awadhi");
        public static readonly ILanguage Aymara = new Language("aym","ay","Aymara");
        public static readonly ILanguage Azerbaijani = new Language("aze","az","Azerbaijani");
        public static readonly ILanguage Banda = new Language("bad","Banda languages");
        public static readonly ILanguage Bamileke = new Language("bai","Bamileke languages");
        public static readonly ILanguage Bashkir = new Language("bak","ba","Bashkir");
        public static readonly ILanguage Baluci = new Language("bal","Baluchi");
        public static readonly ILanguage Bambara = new Language("bam","bm","Bambara");
        public static readonly ILanguage Balinese = new Language("ban","Balinese");
        public static readonly ILanguage Basque = new Language("baq", "eus", "eu", "Basque");
        public static readonly ILanguage Basa = new Language("bas","Basa");
        public static readonly ILanguage Baltic = new Language("bat","Baltic languages");
        public static readonly ILanguage Beja = new Language("bej","Beja; Bedawiyet");
        public static readonly ILanguage Belarusian = new Language("bel","be","Belarusian");
        public static readonly ILanguage Bemba = new Language("bem","Bemba");
        public static readonly ILanguage Bengali = new Language("ben","bn","Bengali");
        public static readonly ILanguage Berber = new Language("ber","Berber languages");
        public static readonly ILanguage Bhojpuri = new Language("bho","Bhojpuri");
        public static readonly ILanguage Bihari = new Language("bih","bh","Bihari languages");
        public static readonly ILanguage Bikol = new Language("bik","Bikol");
        public static readonly ILanguage Bini = new Language("bin","Bini; Edo");
        public static readonly ILanguage Bislama = new Language("bis","bi","Bislama");
        public static readonly ILanguage Siksika = new Language("bla","Siksika");
        public static readonly ILanguage Bantu = new Language("bnt","Bantu languages");
        public static readonly ILanguage Tibetan = new Language("tib", "bod", "bo","Tibetan");
        public static readonly ILanguage Bosnian = new Language("bos","bs","Bosnian");
        public static readonly ILanguage Braj = new Language("bra","Braj");
        public static readonly ILanguage Breton = new Language("bre","br","Breton");
        public static readonly ILanguage Batak = new Language("btk	","Batak languages");
        public static readonly ILanguage Buriat = new Language("bua","Buriat");
        public static readonly ILanguage Buginese = new Language("bug","Buginese");
        public static readonly ILanguage Bulgarian = new Language("bul","bg","Bulgarian");
        public static readonly ILanguage Bunun = new Language("bun", "Bunun");
        public static readonly ILanguage Burmese = new Language("bur","mya", "my",	"Burmese");
        public static readonly ILanguage Bilin = new Language("byn","Blin; Bilin");
        public static readonly ILanguage Caddo = new Language("cad","Caddo");
        public static readonly ILanguage CentralAmericanIndianLanguages = new Language("cai","Central American Indian languages");
        public static readonly ILanguage Galibi = new Language("car","Galibi Carib");
        public static readonly ILanguage Catalan = new Language("cat","ca","Catalan; Valencian");
        public static readonly ILanguage Caucasian = new Language("cau","Caucasian languages");
        public static readonly ILanguage Cebuano = new Language("ceb","Cebuano");
        public static readonly ILanguage Celtic = new Language("cel","Celtic languages");
        public static readonly ILanguage Czech = new Language("cze", "ces", "cs", "Czech");
        public static readonly ILanguage Chamorro = new Language("cha","ch","Chamorro");
        public static readonly ILanguage Chibcha = new Language("chb","Chibcha");
        public static readonly ILanguage Chechen = new Language("che","ce","Chechen");
        public static readonly ILanguage Chagatai = new Language("chg","Chagatai");
        public static readonly ILanguage Chinese = new Language("chi", "zho","zh","Chinese");
        public static readonly ILanguage Chuukese = new Language("chk","Chuukese");
        public static readonly ILanguage Mari = new Language("chm","Mari");
        public static readonly ILanguage Chinook = new Language("chn","Chinook jargon");
        public static readonly ILanguage Choctaw = new Language("cho","Choctaw");
        public static readonly ILanguage Chipewyan = new Language("chp","Chipewyan; Dene Suline");
        public static readonly ILanguage Cherokee = new Language("chr","Cherokee");
        public static readonly ILanguage OldBulgarian = new Language("chu","cu","Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic");
        public static readonly ILanguage Chuvash = new Language("chv","cv","Chuvash");
        public static readonly ILanguage Cheyenne = new Language("chy","Cheyenne");
        public static readonly ILanguage Chamic = new Language("cmc","Chamic languages");
        public static readonly ILanguage Coptic = new Language("cop","Coptic");
        public static readonly ILanguage Cornish = new Language("cor","kw","Cornish");
        public static readonly ILanguage Corsican = new Language("cos","co","Corsican");
        public static readonly ILanguage EnglishCreoles = new Language("cpe","Creoles and pidgins, English based");
        public static readonly ILanguage FrenchCreoles = new Language("cpf","Creoles and pidgins, French-based");
        public static readonly ILanguage PortugueseCreoles = new Language("cpp","Creoles and pidgins, Portuguese-based");
        public static readonly ILanguage Cree = new Language("cre","cr","Cree");
        public static readonly ILanguage CrimeanTatar = new Language("crh","Crimean Tatar; Crimean Turkish");
        public static readonly ILanguage Creoles = new Language("crp","Creoles and pidgins");
        public static readonly ILanguage Kashubian = new Language("csb","Kashubian");
        public static readonly ILanguage Cushitic = new Language("cus","Cushitic languages");
        public static readonly ILanguage Welsh = new Language("wel", "cym","cy","Welsh");
        public static readonly ILanguage Dakota = new Language("dak","Dakota");
        public static readonly ILanguage Danish = new Language("dan","da","Danish");
        public static readonly ILanguage Dargwa = new Language("dar","Dargwa");
        public static readonly ILanguage LandDayak = new Language("day","Land Dayak languages");
        public static readonly ILanguage Delaware = new Language("del","Delaware");
        public static readonly ILanguage Slave = new Language("den","Slave (Athapascan)");
        public static readonly ILanguage Dogrib = new Language("dgr","Dogrib");
        public static readonly ILanguage Dinka = new Language("din","Dinka");
        public static readonly ILanguage Divehi = new Language("div","dv","Divehi; Dhivehi; Maldivian");
        public static readonly ILanguage Dogri = new Language("doi","Dogri");
        public static readonly ILanguage Dravidian = new Language("dra","Dravidian languages");
        public static readonly ILanguage LowerSorbian = new Language("dsb","Lower Sorbian");
        public static readonly ILanguage Duala = new Language("dua","Duala");
        public static readonly ILanguage MiddleDutch = new Language("dum","Dutch, Middle (ca.1050-1350)");
        public static readonly ILanguage Dutch = new Language("dut", "nld", "nl", "Dutch; Flemish");
        public static readonly ILanguage Dyula = new Language("dyu","Dyula");
        public static readonly ILanguage Dzongkha = new Language("dzo","dz","Dzongkha");
        public static readonly ILanguage Efik = new Language("efi","Efik");
        public static readonly ILanguage AncientEgyptian = new Language("egy","Egyptian (Ancient)");
        public static readonly ILanguage Ekajuk = new Language("eka","Ekajuk");
        public static readonly ILanguage Greek = new Language("gre", "ell", "el", "Greek, Modern (1453-)");
        public static readonly ILanguage Elamite = new Language("elx","Elamite");
        public static readonly ILanguage English = new Language("eng","en","English");
        public static readonly ILanguage MiddleEnglish = new Language("enm","English, Middle (1100-1500)");
        public static readonly ILanguage Esperanto = new Language("epo","eo","Esperanto");
        public static readonly ILanguage Estonian = new Language("est","et","Estonian");
        public static readonly ILanguage Ewe = new Language("ewe","ee","Ewe");
        public static readonly ILanguage Ewondo = new Language("ewo","Ewondo");
        public static readonly ILanguage Fang = new Language("fan","Fang");
        public static readonly ILanguage Faroese = new Language("fao","fo","Faroese");
        public static readonly ILanguage Persian = new Language("per", "fas", "fa", "Persian");
        public static readonly ILanguage Fanti = new Language("fat","Fanti");
        public static readonly ILanguage Fijian = new Language("fij","fj","Fijian");
        public static readonly ILanguage Filipino = new Language("fil","Filipino; Pilipino");
        public static readonly ILanguage Finnish = new Language("fin","fi","Finnish");
        public static readonly ILanguage FinnoUgurian = new Language("fiu","Finno-Ugrian languages");
        public static readonly ILanguage Fon = new Language("fon","Fon");
        public static readonly ILanguage French = new Language("fre", "fra", "fr", "French");
        public static readonly ILanguage MiddleFrench = new Language("frm","French, Middle (ca.1400-1600)");
        public static readonly ILanguage OldFrench = new Language("fro","French, Old (842-ca.1400)");
        public static readonly ILanguage NorthernFrisian = new Language("frr","Northern Frisian");
        public static readonly ILanguage EasternFrisian = new Language("frs","Eastern Frisian");
        public static readonly ILanguage WesternFrisian = new Language("fry","fy","Western Frisian");
        public static readonly ILanguage Fulah = new Language("ful","ff","Fulah");
        public static readonly ILanguage Friulian = new Language("fur","Friulian");
        public static readonly ILanguage Ga = new Language("gaa","Ga");
        public static readonly ILanguage Gayo = new Language("gay","Gayo");
        public static readonly ILanguage Gbaya = new Language("gba","Gbaya");
        public static readonly ILanguage GermanicLanguages = new Language("gem", "Germanic languages");
        public static readonly ILanguage Georgian = new Language("geo", "kat", "ka", "Georgian");
        public static readonly ILanguage German = new Language("ger", "deu", "de", "German");
        public static readonly ILanguage Geez = new Language("gez","Geez");
        public static readonly ILanguage Gilbertese = new Language("gil","Gilbertese");
        public static readonly ILanguage Gaelic = new Language("gla","gd","Gaelic; Scottish Gaelic");
        public static readonly ILanguage Irish = new Language("gle","ga","Irish");
        public static readonly ILanguage Galician = new Language("glg","gl","Galician");
        public static readonly ILanguage Manx = new Language("glv","gv","Manx");
        public static readonly ILanguage MiddleHighGerman = new Language("gmh","German, Middle High (ca.1050-1500)");
        public static readonly ILanguage OldHighGerman = new Language("goh","German, Old High (ca.750-1050)");
        public static readonly ILanguage Gondi = new Language("gon","Gondi");
        public static readonly ILanguage Gorontalo = new Language("gor","Gorontalo");
        public static readonly ILanguage Gothic = new Language("got","Gothic");
        public static readonly ILanguage Grebo = new Language("grb","Grebo");
        public static readonly ILanguage AncientGreek = new Language("grc","Greek, Ancient (to 1453)");
        public static readonly ILanguage Guarani = new Language("grn","gn","Guarani");
        public static readonly ILanguage SwissGerman = new Language("gsw","Swiss German; Alemannic; Alsatian");
        public static readonly ILanguage Gujarati = new Language("guj","gu","Gujarati");
        public static readonly ILanguage Gwichin = new Language("gwi","Gwich'in");
        public static readonly ILanguage Haida = new Language("hai","Haida");
        public static readonly ILanguage Haitian = new Language("hat","ht","Haitian; Haitian Creole");
        public static readonly ILanguage Hakka = new Language("hak", "Hakka");
        public static readonly ILanguage Hausa = new Language("hau","ha","Hausa");
        public static readonly ILanguage Hawaiian = new Language("haw","Hawaiian");
        public static readonly ILanguage Hebrew = new Language("heb","he","Hebrew");
        public static readonly ILanguage Herero = new Language("her","hz","Herero");
        public static readonly ILanguage Hiligaynon = new Language("hil","Hiligaynon");
        public static readonly ILanguage Himachali = new Language("him","Himachali languages; Western Pahari languages");
        public static readonly ILanguage Hindi = new Language("hin","hi","Hindi");
        public static readonly ILanguage Hittite = new Language("hit","Hittite");
        public static readonly ILanguage Hmong = new Language("hmn","Hmong; Mong");
        public static readonly ILanguage HiriMotu = new Language("hmo","ho","Hiri Motu");
        public static readonly ILanguage Croatian = new Language("hrv","hr","Croatian");
        public static readonly ILanguage UpperSorbian = new Language("hsb","Upper Sorbian");
        public static readonly ILanguage Hungarian = new Language("hun","hu","Hungarian");
        public static readonly ILanguage Hupa = new Language("hup","Hupa");
        public static readonly ILanguage Iban = new Language("iba","Iban");
        public static readonly ILanguage Igbo = new Language("ibo","ig","Igbo");
        public static readonly ILanguage Ido = new Language("ido","io","Ido");
        public static readonly ILanguage SichuanYi = new Language("iii","ii","Sichuan Yi; Nuosu");
        public static readonly ILanguage Ijo = new Language("ijo","Ijo languages");
        public static readonly ILanguage Inuktitut = new Language("iku","iu","Inuktitut");
        public static readonly ILanguage InterlingueOccidental = new Language("ile","ie","Interlingue; Occidental");
        public static readonly ILanguage Iloko = new Language("ilo","Iloko");
        public static readonly ILanguage Interlingua = new Language("ina","ia","Interlingua (International Auxiliary Language Association)");
        public static readonly ILanguage Indic = new Language("inc","Indic languages");
        public static readonly ILanguage Indonesian = new Language("ind","id","Indonesian");
        public static readonly ILanguage IndoEuropean = new Language("ine","Indo-European languages");
        public static readonly ILanguage Ingush = new Language("inh","Ingush");
        public static readonly ILanguage Inupiaq = new Language("ipk","ik","Inupiaq");
        public static readonly ILanguage Iranian = new Language("ira","Iranian languages");
        public static readonly ILanguage Iroquoian = new Language("iro","Iroquoian languages");
        public static readonly ILanguage Icelandic = new Language("ice","isl","is","Icelandic");
        public static readonly ILanguage Italian = new Language("ita","it","Italian");
        public static readonly ILanguage Javanese = new Language("jav","jv","Javanese");
        public static readonly ILanguage Lojban = new Language("jbo","Lojban");
        public static readonly ILanguage Japanese = new Language("jpn","ja","Japanese");
        public static readonly ILanguage JudeoPersian = new Language("jpr","Judeo-Persian");
        public static readonly ILanguage JudeoArabic = new Language("jrb","Judeo-Arabic");
        public static readonly ILanguage KaraKalpak = new Language("kaa","Kara-Kalpak");
        public static readonly ILanguage Kabyle = new Language("kab","Kabyle");
        public static readonly ILanguage Kachin = new Language("kac","Kachin; Jingpho");
        public static readonly ILanguage Kalallisut = new Language("kal","kl","Kalaallisut; Greenlandic");
        public static readonly ILanguage Kamba = new Language("kam","Kamba");
        public static readonly ILanguage Kannada = new Language("kan","kn","Kannada");
        public static readonly ILanguage Karen = new Language("kar","Karen languages");
        public static readonly ILanguage Kashmiri = new Language("kas","ks","Kashmiri");
        public static readonly ILanguage Kanuri = new Language("kau","kr","Kanuri");
        public static readonly ILanguage Kawi = new Language("kaw","Kawi");
        public static readonly ILanguage Kazakh = new Language("kaz","kk","Kazakh");
        public static readonly ILanguage Kabardian = new Language("kbd","Kabardian");
        public static readonly ILanguage Khasi = new Language("kha","Khasi");
        public static readonly ILanguage Khoisan = new Language("khi","Khoisan languages");
        public static readonly ILanguage CentralKhmer = new Language("khm","km","Central Khmer");
        public static readonly ILanguage Khotanese = new Language("kho","Khotanese; Sakan");
        public static readonly ILanguage Kikuyu = new Language("kik","ki","Kikuyu; Gikuyu");
        public static readonly ILanguage Kinyarwanda = new Language("kin","rw","Kinyarwanda");
        public static readonly ILanguage Kirghiz = new Language("kir","ky","Kirghiz; Kyrgyz");
        public static readonly ILanguage Kimbundu = new Language("kmb","Kimbundu");
        public static readonly ILanguage Konkani = new Language("kok","Konkani");
        public static readonly ILanguage Komi = new Language("kom","kv","Komi");
        public static readonly ILanguage Kongo = new Language("kon","kg","Kongo");
        public static readonly ILanguage Korean = new Language("kor","ko","Korean");
        public static readonly ILanguage Kosraean = new Language("kos","Kosraean");
        public static readonly ILanguage Kpelle = new Language("kpe","Kpelle");
        public static readonly ILanguage Karachay = new Language("krc","Karachay-Balkar");
        public static readonly ILanguage Karelian = new Language("krl","Karelian");
        public static readonly ILanguage Kru = new Language("kro","Kru languages");
        public static readonly ILanguage Kurukh = new Language("kru","Kurukh");
        public static readonly ILanguage Kuanyama = new Language("kua","kj","Kuanyama; Kwanyama");
        public static readonly ILanguage Kumyk = new Language("kum","Kumyk");
        public static readonly ILanguage Kurdish = new Language("kur","ku","Kurdish");
        public static readonly ILanguage Kutenai = new Language("kut","Kutenai");
        public static readonly ILanguage Ladino = new Language("lad","Ladino");
        public static readonly ILanguage Lahnda = new Language("lah","Lahnda");
        public static readonly ILanguage Lamba = new Language("lam","Lamba");
        public static readonly ILanguage Lao = new Language("lao","lo","Lao");
        public static readonly ILanguage Latin = new Language("lat","la","Latin");
        public static readonly ILanguage Latvian = new Language("lav","lv","Latvian");
        public static readonly ILanguage Lezghian = new Language("lez","Lezghian");
        public static readonly ILanguage Limburgan = new Language("lim","li","Limburgan; Limburger; Limburgish");
        public static readonly ILanguage Lingala = new Language("lin","ln","Lingala");
        public static readonly ILanguage Lithuanian = new Language("lit","lt","Lithuanian");
        public static readonly ILanguage Mongo = new Language("lol","Mongo");
        public static readonly ILanguage Lozi = new Language("loz","Lozi");
        public static readonly ILanguage Luxembourgish = new Language("ltz","lb","Luxembourgish; Letzeburgesch");
        public static readonly ILanguage LubaLulua = new Language("lua","Luba-Lulua");
        public static readonly ILanguage LubaKatanga = new Language("lub","lu","Luba-Katanga");
        public static readonly ILanguage Ganda = new Language("lug","lg","Ganda");
        public static readonly ILanguage Luiseno = new Language("lui","Luiseno");
        public static readonly ILanguage Lunda = new Language("lun","Lunda");
        public static readonly ILanguage Luo = new Language("luo","Luo (Kenya and Tanzania)");
        public static readonly ILanguage Lushai = new Language("lus","Lushai");
        public static readonly ILanguage Macedonian = new Language("mac","mkd","mk","Macedonian");
        public static readonly ILanguage Madurese = new Language("mad","Madurese");
        public static readonly ILanguage Magahi = new Language("mag","Magahi");
        public static readonly ILanguage Marshallese = new Language("mah","mh","Marshallese");
        public static readonly ILanguage Maithili = new Language("mai","Maithili");
        public static readonly ILanguage Makasar = new Language("mak","Makasar");
        public static readonly ILanguage Malayalam = new Language("mal","ml","Malayalam");
        public static readonly ILanguage Mandingo = new Language("man","Mandingo");
        public static readonly ILanguage Maori = new Language("mao","mri","mi","Maori");
        public static readonly ILanguage Austronesian = new Language("map","Austronesian languages");
        public static readonly ILanguage Marathi = new Language("mar","mr","Marathi");
        public static readonly ILanguage Masai = new Language("mas","Masai");
        public static readonly ILanguage Malay = new Language("may","msa","ms","Malay");
        public static readonly ILanguage Moksha = new Language("mdf","Moksha");
        public static readonly ILanguage Mandar = new Language("mdr","Mandar");
        public static readonly ILanguage Mende = new Language("men","Mende");
        public static readonly ILanguage MiddleIrish = new Language("mga","Irish, Middle (900-1200)");
        public static readonly ILanguage Micmac = new Language("mic","Mi'kmaq; Micmac");
        public static readonly ILanguage Minangkabau = new Language("min","Minangkabau");
        public static readonly ILanguage UncodedLanguages = new Language("mis","Uncoded languages");
        public static readonly ILanguage MonKhmer = new Language("mkh","Mon-Khmer languages");
        public static readonly ILanguage Malagasy = new Language("mlg","mg","Malagasy");
        public static readonly ILanguage Maltese = new Language("mlt","mt","Maltese");
        public static readonly ILanguage Manchu = new Language("mnc","Manchu");
        public static readonly ILanguage Manipuri = new Language("mni","Manipuri");
        public static readonly ILanguage Manobo = new Language("mno","Manobo languages");
        public static readonly ILanguage Mohawk = new Language("moh","Mohawk");
        public static readonly ILanguage Mongolian = new Language("mon","mn","Mongolian");
        public static readonly ILanguage Mossi = new Language("mos","Mossi");
        public static readonly ILanguage MultipleLanguages = new Language("mul","Multiple languages");
        public static readonly ILanguage MundaLanguages = new Language("mun","Munda languages");
        public static readonly ILanguage Creek = new Language("mus","Creek");
        public static readonly ILanguage Mirandese = new Language("mwl","Mirandese");
        public static readonly ILanguage Marwari = new Language("mwr","Marwari");
        public static readonly ILanguage Mayan = new Language("myn","Mayan languages");
        public static readonly ILanguage Erzya = new Language("myv","Erzya");
        public static readonly ILanguage Nahuatl = new Language("nah","Nahuatl languages");
        public static readonly ILanguage NorthAmericanIndian = new Language("nai","North American Indian languages");
        public static readonly ILanguage Neapolitan = new Language("nap","Neapolitan");
        public static readonly ILanguage Nauru = new Language("nau","na","Nauru");
        public static readonly ILanguage Navajo = new Language("nav","nv","Navajo; Navaho");
        public static readonly ILanguage SouthNdebele = new Language("nbl","nr","Ndebele, South; South Ndebele");
        public static readonly ILanguage NorthNdebele = new Language("nde","nd","Ndebele, North; North Ndebele");
        public static readonly ILanguage Ndonga = new Language("ndo","ng","Ndonga");
        public static readonly ILanguage LowGerman = new Language("nds","Low German; Low Saxon; German, Low; Saxon, Low");
        public static readonly ILanguage Nepali = new Language("nep","ne","Nepali");
        public static readonly ILanguage NepalBhasa = new Language("new","Nepal Bhasa; Newari");
        public static readonly ILanguage Nias = new Language("nia","Nias");
        public static readonly ILanguage NigerKordofanian = new Language("nic","Niger-Kordofanian languages");
        public static readonly ILanguage Niuean = new Language("niu","Niuean");
        public static readonly ILanguage NorwegianNynorsk = new Language("nno","nn","Norwegian Nynorsk; Nynorsk, Norwegian");
        public static readonly ILanguage NorwegianBokmal = new Language("nob","nb","Bokmål, Norwegian; Norwegian Bokmål");
        public static readonly ILanguage Nogai = new Language("nog","Nogai");
        public static readonly ILanguage OldNorse = new Language("non","Norse, Old");
        public static readonly ILanguage Norwegian = new Language("nor","no","Norwegian");
        public static readonly ILanguage NKo = new Language("nqo","N'Ko");
        public static readonly ILanguage Pedi = new Language("nso","Pedi; Sepedi; Northern Sotho");
        public static readonly ILanguage Nubian = new Language("nub","Nubian languages");
        public static readonly ILanguage ClassicalNewari = new Language("nwc","Classical Newari; Old Newari; Classical Nepal Bhasa");
        public static readonly ILanguage Chichewa = new Language("nya","ny","Chichewa; Chewa; Nyanja");
        public static readonly ILanguage Nyamwezi = new Language("nym","Nyamwezi");
        public static readonly ILanguage Nyankole = new Language("nyn","Nyankole");
        public static readonly ILanguage Nyoro = new Language("nyo","Nyoro");
        public static readonly ILanguage Nzima = new Language("nzi","Nzima");
        public static readonly ILanguage Occitan = new Language("oci","oc","Occitan (post 1500)");
        public static readonly ILanguage Ojibway = new Language("oji","oj","Ojibwa");
        public static readonly ILanguage Oriya = new Language("ori","or","Oriya");
        public static readonly ILanguage Oromo = new Language("orm","om","Oromo");
        public static readonly ILanguage Osage = new Language("osa","Osage");
        public static readonly ILanguage Ossetian = new Language("oss","os","Ossetian; Ossetic");
        public static readonly ILanguage OttomanTurkish = new Language("ota","Turkish, Ottoman (1500-1928)");
        public static readonly ILanguage Otomian = new Language("oto","Otomian languages");
        public static readonly ILanguage Papuan = new Language("paa","Papuan languages");
        public static readonly ILanguage Pangasinan = new Language("pag","Pangasinan");
        public static readonly ILanguage Pahlavi = new Language("pal","Pahlavi");
        public static readonly ILanguage Paiwan = new Language("pwn", "Paiwan");
        public static readonly ILanguage Pampanga = new Language("pam","Pampanga; Kapampangan");
        public static readonly ILanguage Panjabi = new Language("pan","pa","Panjabi; Punjabi");
        public static readonly ILanguage Papiamento = new Language("pap","Papiamento");
        public static readonly ILanguage Palauan= new Language("pau","Palauan");
        public static readonly ILanguage OldPersian = new Language("peo","Persian, Old (ca.600-400 B.C.)");
        public static readonly ILanguage PhilippineLanguages = new Language("phi","Philippine languages");
        public static readonly ILanguage Phoenician = new Language("phn","Phoenician");
        public static readonly ILanguage Pali = new Language("pli","pi","Pali");
        public static readonly ILanguage Polish = new Language("pol","pl","Polish");
        public static readonly ILanguage Pohnpeian = new Language("pon", "Pohnpeian");
        public static readonly ILanguage Portuguese = new Language("por", "pt", "Portuguese");
        public static readonly ILanguage Prakrit = new Language("pra", "Prakrit languages");
        public static readonly ILanguage OldProvencal = new Language("pro", "Provençal, Old (to 1500); Occitan, Old (to 1500)");
        public static readonly ILanguage Pushto = new Language("pus", "ps", "Pushto; Pashto");
        public static readonly ILanguage Quechua = new Language("que", "qu", "Quechua");
        public static readonly ILanguage Rajasthani = new Language("raj", "Rajasthani");
        public static readonly ILanguage Rapanui = new Language("rap", "Rapanui");
        public static readonly ILanguage Rarotongan = new Language("rar", "Rarotongan; Cook Islands Maori");
        public static readonly ILanguage RomanceLanguages = new Language("roa", "Romance languages");
        public static readonly ILanguage Romansh = new Language("roh", "rm", "Romansh");
        public static readonly ILanguage Romany = new Language("rom", "Romany");
        public static readonly ILanguage Romanian = new Language("rum", "ron", "ro", "Romanian; Moldavian; Moldovan");
        public static readonly ILanguage Rundi = new Language("run", "rn", "Rundi");
        public static readonly ILanguage Aromanian = new Language("rup", "Aromanian; Arumanian; Macedo-Romanian");
        public static readonly ILanguage Russian = new Language("rus", "ru", "Russian");
        public static readonly ILanguage Sandawe = new Language("sad", "Sandawe");
        public static readonly ILanguage Sango = new Language("sag", "sg", "Sango");
        public static readonly ILanguage Yakut = new Language("sah", "Yakut");
        public static readonly ILanguage SouthAmericanIndianLanguages = new Language("sai", "South American Indian languages");
        public static readonly ILanguage Salishan = new Language("sal", "Salishan languages");
        public static readonly ILanguage SamaritanAramaic = new Language("sam", "Samaritan Aramaic");
        public static readonly ILanguage Sanskrit = new Language("san", "sa", "Sanskrit");
        public static readonly ILanguage Sasak = new Language("sas", "Sasak");
        public static readonly ILanguage Santali = new Language("sat", "Santali");
        public static readonly ILanguage Sicilian = new Language("scn", "Sicilian");
        public static readonly ILanguage Scots = new Language("sco", "Scots");
        public static readonly ILanguage Selkup = new Language("sel", "Selkup");
        public static readonly ILanguage Semitic = new Language("sem", "Semitic languages");
        public static readonly ILanguage OldIrish = new Language("sga", "Irish, Old (to 900)");
        public static readonly ILanguage SignLanguages = new Language("sgn", "Sign Languages");
        public static readonly ILanguage Shan = new Language("shn", "Shan");
        public static readonly ILanguage Sidamo = new Language("sid", "Sidamo");
        public static readonly ILanguage Sinhala = new Language("sin", "si", "Sinhala; Sinhalese");
        public static readonly ILanguage Siouan = new Language("sio", "Siouan languages");
        public static readonly ILanguage SinoTibetan = new Language("sit", "Sino-Tibetan languages");
        public static readonly ILanguage Slavic = new Language("sla", "Slavic languages");
        public static readonly ILanguage Slovak = new Language("slo", "slk", "sk", "Slovak");
        public static readonly ILanguage Slovenian = new Language("slv", "sl", "Slovenian");
        public static readonly ILanguage SouthernSami = new Language("sma", "Southern Sami");
        public static readonly ILanguage NorthernSami = new Language("sme", "se", "Northern Sami");
        public static readonly ILanguage SamiLanguages = new Language("smi", "Sami languages");
        public static readonly ILanguage LuleSami = new Language("smj", "Lule Sami");
        public static readonly ILanguage InariSami = new Language("smn", "Inari Sami");
        public static readonly ILanguage Samoan = new Language("smo", "sm", "Samoan");
        public static readonly ILanguage SkoltSami = new Language("sms", "Skolt Sami");
        public static readonly ILanguage Shona = new Language("sna", "sn", "Shona");
        public static readonly ILanguage Sindhi = new Language("snd", "sd", "Sindhi");
        public static readonly ILanguage Sonike = new Language("snk", "Soninke");
        public static readonly ILanguage Sogdian = new Language("sog", "Sogdian");
        public static readonly ILanguage Somali = new Language("som", "so", "Somali");
        public static readonly ILanguage Songhai = new Language("son", "Songhai languages");
        public static readonly ILanguage Sotho = new Language("sot", "st", "Sotho, Southern");
        public static readonly ILanguage Spanish = new Language("spa", "es", "Spanish; Castilian");
        public static readonly ILanguage Sardinian = new Language("srd", "sc", "Sardinian");
        public static readonly ILanguage Sranan = new Language("srn", "Sranan Tongo");
        public static readonly ILanguage Serbian = new Language("srp", "sr", "Serbian");
        public static readonly ILanguage Serer = new Language("srr", "Serer");
        public static readonly ILanguage NiloSaharan = new Language("ssa", "Nilo-Saharan languages");
        public static readonly ILanguage Swati = new Language("ssw", "ss", "Swati");
        public static readonly ILanguage Sukuma = new Language("suk", "Sukuma");
        public static readonly ILanguage Sudanese = new Language("sun", "su", "Sundanese");
        public static readonly ILanguage Susu = new Language("sus", "Susu");
        public static readonly ILanguage Sumerian = new Language("sux", "Sumerian");
        public static readonly ILanguage Swahili = new Language("swa", "sw", "Swahili");
        public static readonly ILanguage Swedish = new Language("swe", "sv", "Swedish");
        public static readonly ILanguage ClassicalSyriac = new Language("syc", "Classical Syriac");
        public static readonly ILanguage Syriac = new Language("syr", "Syriac");
        public static readonly ILanguage Tahitian = new Language("tah","ty","Tahitian");
        public static readonly ILanguage TaiLanguages = new Language("tai","Tai languages");
        public static readonly ILanguage Tamil = new Language("tam","ta","Tamil");
        public static readonly ILanguage Tao = new Language("tao", "Tao");
        public static readonly ILanguage Tatar = new Language("tat","tt","Tatar");
        public static readonly ILanguage Teluga = new Language("tel","te","Telugu");
        public static readonly ILanguage Timne = new Language("tem","Timne");
        public static readonly ILanguage Tereno = new Language("ter","Tereno");
        public static readonly ILanguage Tetum = new Language("tet","Tetum");
        public static readonly ILanguage Tajik = new Language("tgk","tg","Tajik");
        public static readonly ILanguage Tagalog = new Language("tgl","tl","Tagalog");
        public static readonly ILanguage Tayal = new Language("tay", "Tayal");
        public static readonly ILanguage Thai = new Language("tha","th","Thai");
        public static readonly ILanguage Tigre = new Language("tig","Tigre");
        public static readonly ILanguage Tigrinya = new Language("tir","ti","Tigrinya");
        public static readonly ILanguage Tiv = new Language("tiv","Tiv");
        public static readonly ILanguage Tokelau = new Language("tkl","Tokelau");
        public static readonly ILanguage Klingon = new Language("tlh","Klingon; tlhIngan-Hol");
        public static readonly ILanguage Tlingit = new Language("tli","Tlingit");
        public static readonly ILanguage Tamashek = new Language("tmh","Tamashek");
        public static readonly ILanguage TongaNyasa = new Language("tog","Tonga (Nyasa)");
        public static readonly ILanguage Tonga = new Language("ton","to","Tonga (Tonga Islands)");
        public static readonly ILanguage TokPisin = new Language("tpi","Tok Pisin");
        public static readonly ILanguage Tsimshian = new Language("tsi","Tsimshian");
        public static readonly ILanguage Tswana = new Language("tsn","tn","Tswana");
        public static readonly ILanguage Tsonga = new Language("tso","ts","Tsonga");
        public static readonly ILanguage Tsou = new Language("tsu", "Tsou");
        public static readonly ILanguage Turkmen = new Language("tuk","tk","Turkmen");
        public static readonly ILanguage Tumbuka = new Language("tum","Tumbuka");
        public static readonly ILanguage Tupi = new Language("tup","Tupi languages");
        public static readonly ILanguage Turkish = new Language("tur","tr","Turkish");
        public static readonly ILanguage Altaic = new Language("tut","Altaic languages");
        public static readonly ILanguage Tuvalu = new Language("tvl","Tuvalu");
        public static readonly ILanguage Twi = new Language("twi","tw","Twi");
        public static readonly ILanguage Tuvian = new Language("tyv","Tuvinian");
        public static readonly ILanguage Udmurt = new Language("udm","Udmurt");
        public static readonly ILanguage Ugaritic = new Language("uga","Ugaritic");
        public static readonly ILanguage Uighur = new Language("uig","ug","Uighur; Uyghur");
        public static readonly ILanguage Ukrainian = new Language("ukr","uk","Ukrainian");
        public static readonly ILanguage Umbundu = new Language("umb","Umbundu");
        public static readonly ILanguage Undetermined = new Language("und","Undetermined");
        public static readonly ILanguage Urdu = new Language("urd","ur","Urdu");
        public static readonly ILanguage Uzbek = new Language("uzb","uz","Uzbek");
        public static readonly ILanguage Vai = new Language("vai","Vai");
        public static readonly ILanguage Venda = new Language("ven","ve","Venda");
        public static readonly ILanguage Vietnamese = new Language("vie","vi","Vietnamese");
        public static readonly ILanguage Volapuk = new Language("vol","vo","Volapük");
        public static readonly ILanguage Votic = new Language("vot","Votic");          
        public static readonly ILanguage Wakashan = new Language("wak","Wakashan languages");
        public static readonly ILanguage Walaitta = new Language("wal","Wolaitta; Wolaytta");
        public static readonly ILanguage Waray = new Language("war","Waray");
        public static readonly ILanguage Washo = new Language("was","Washo");
        public static readonly ILanguage Sorbian = new Language("wen","Sorbian languages");
        public static readonly ILanguage Walloon = new Language("wln","wa","Walloon");
        public static readonly ILanguage Wolof = new Language("wol","wo","Wolof");
        public static readonly ILanguage Kalmyk = new Language("xal","Kalmyk; Oirat");
        public static readonly ILanguage Xhosa = new Language("xho","xh","Xhosa");
        public static readonly ILanguage Yao = new Language("yao","Yao");
        public static readonly ILanguage Yapese = new Language("yap","Yapese");
        public static readonly ILanguage Yiddish = new Language("yid","yi","Yiddish");
        public static readonly ILanguage Yor = new Language("yor","yo","Yoruba");
        public static readonly ILanguage Yupik = new Language("ypk","Yupik languages");
        public static readonly ILanguage Zapotec = new Language("zap","Zapotec");
        public static readonly ILanguage Bliss = new Language("zbl","Blissymbols; Blissymbolics; Bliss");
        public static readonly ILanguage Zenaga = new Language("zen","Zenaga");
        public static readonly ILanguage Zhuang = new Language("zha","za","Zhuang; Chuang");
        public static readonly ILanguage Zande = new Language("znd","Zande languages");
        public static readonly ILanguage Zulu = new Language("zul","zu","Zulu");
        public static readonly ILanguage Zuni = new Language("zun","Zuni");
        public static readonly ILanguage NotApplicable = new Language("zxx","No linguistic content; Not applicable");
        public static readonly ILanguage Zaza = new Language("zza", "Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki");

        public static readonly ILanguage BelgianFrenchSignLanguage = new Language("sfb", "Belgian French Sign Language");
        public static readonly ILanguage BelgianFlemishSignLanguage = new Language("vgt", "Belgian Flemish Sign Language");
        public static readonly ILanguage SwissGermanSignLanguage = new Language("sgg", "Swiss German Sign Language");
        public static readonly ILanguage MandarinChinese = new Language("cmn", "Mandarin Chinese");
        public static readonly ILanguage MinanHokkien = new Language("nan", "Minan Hokkien");
        public static readonly ILanguage Xiang = new Language("hsn", "Xiang");

        #endregion
    }
}
