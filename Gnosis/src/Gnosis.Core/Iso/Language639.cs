using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Iso
{
    public class Language639
        : ILanguage639
    {
        private Language639(string alpha3Code, string name)
            : this(alpha3Code, string.Empty, string.Empty, name)
        {
        }

        private Language639(string alpha3Code, string alpha2Code, string name)
            : this(alpha3Code, string.Empty, alpha2Code, name)
        {

        }

        public Language639(string alpha3Code, string alpha3TermCode, string alpha2Code, string name)
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

        #region ILanguage 639 Members

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
            languages.Add(Mao);
            languages.Add(May);
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
            languages.Add(Tatar);
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
        }

        static Language639()
        {
            InitializeLanguages();

            foreach (var language in languages)
            {
                if (!string.IsNullOrEmpty(language.Alpha2Code))
                    byAlpha2Code.Add(language.Alpha2Code, language);

                if (!string.IsNullOrEmpty(language.Alpha3TermCode))
                    byAlpha3TermCode.Add(language.Alpha3TermCode, language);

                byAlpha3Code.Add(language.Alpha3Code, language);
                byName.Add(language.Name, language);
            }
        }

        private static readonly IList<ILanguage639> languages = new List<ILanguage639>();
        private static readonly IDictionary<string, ILanguage639> byAlpha2Code = new Dictionary<string, ILanguage639>();
        private static readonly IDictionary<string, ILanguage639> byAlpha3Code = new Dictionary<string, ILanguage639>();
        private static readonly IDictionary<string, ILanguage639> byAlpha3TermCode = new Dictionary<string, ILanguage639>();
        private static readonly IDictionary<string, ILanguage639> byName = new Dictionary<string, ILanguage639>();

        #region Public Static Members

        public ILanguage639 GetLanguageByAlpha2Code(string alpha2Code)
        {
            if (string.IsNullOrEmpty(alpha2Code))
                throw new ArgumentNullException("alpha2Code");

            if (alpha2Code.Length != 2)
                throw new ArgumentException("invalid code: alpha-2 code must be exactly two characters");

            return byAlpha2Code.ContainsKey(alpha2Code) ? byAlpha2Code[alpha2Code] : null;
        }

        public ILanguage639 GetLanguageByAlpha3Code(string alpha3Code)
        {
            if (string.IsNullOrEmpty(alpha3Code))
                throw new ArgumentNullException("alpha3Code");

            if (alpha2Code.Length != 3)
                throw new ArgumentException("invalid code: alpha-3 code must be exactly three characters");

            if (byAlpha3TermCode.ContainsKey(alpha3Code))
                return byAlpha3TermCode[alpha3Code];

            return byAlpha3Code.ContainsKey(alpha2Code) ? byAlpha3Code[alpha3Code] : null;
        }

        public ILanguage639 GetLanguageByName(string name)
        {
            return byName.ContainsKey(name) ? byName[name] : null;
        }

        public static IEnumerable<ILanguage639> GetLanguages()
        {
            return languages;
        }

        #endregion

        #region Languages

        public static readonly Language639 Afar = new Language639("aar", "aa", "Afar");
        public static readonly Language639 Abkhazian = new Language639("abk", "ab", "Abkhazian");
        public static readonly Language639 Achinese = new Language639("ace", "Achinese");
        public static readonly Language639 Acoli = new Language639("ach","Acoli");
        public static readonly Language639 Adangme = new Language639("ada","Adangme");
        public static readonly Language639 Adyghe = new Language639("ady","Adyghe; Adygei");
        public static readonly Language639 AfroAsiatic = new Language639("afa","Afro-Asiatic languages");
        public static readonly Language639 Afrihili = new Language639("afh","Afrihili");
        public static readonly Language639 Afrikaans = new Language639("afr","af","Afrikaans");
        public static readonly Language639 Ainu = new Language639("ain","Ainu");
        public static readonly Language639 Akan = new Language639("aka","ak","Akan");
        public static readonly Language639 Akkadian = new Language639("akk","Akkadian");
        public static readonly Language639 Albanian = new Language639("alb", "sqi", "sq", "Albanian");
        public static readonly Language639 Aleut = new Language639("ale","Aleut");
        public static readonly Language639 Algonquian = new Language639("alg","Algonquian languages");
        public static readonly Language639 SouthernAltai = new Language639("alt","Southern Altai");
        public static readonly Language639 Amharic = new Language639("amh","am","Amharic");
        public static readonly Language639 OldEnglish = new Language639("ang", "English, Old (ca.450-1100)");
        public static readonly Language639 Angika = new Language639("anp","Angika");
        public static readonly Language639 Apache = new Language639("apa","Apache languages");
        public static readonly Language639 Arabic = new Language639("ara","ar","Arabic");
        public static readonly Language639 Aramaic = new Language639("arc","Official Aramaic (700-300 BCE)");
        public static readonly Language639 Aragonese = new Language639("arg","an","Aragonese");
        public static readonly Language639 Armenian = new Language639("arm", "hye", "hy", "Armenian");
        public static readonly Language639 Mapudungun = new Language639("arn","Mapudungun; Mapuche");
        public static readonly Language639 Arapaho = new Language639("arp","Arapaho");
        public static readonly Language639 ArtificialLanguages = new Language639("art","Artificial languages");
        public static readonly Language639 Arawak = new Language639("arw","Arawak");
        public static readonly Language639 Assamese = new Language639("asm","as","Assamese");
        public static readonly Language639 Asturian = new Language639("ast","Asturian; Bable; Leonese; Asturleonese");
        public static readonly Language639 AthapascanLanguages = new Language639("ath","Athapascan languages");
        public static readonly Language639 AustrialianLanguages = new Language639("aus","Australian languages");
        public static readonly Language639 Avaric = new Language639("ava","av","Avaric");
        public static readonly Language639 Avestan = new Language639("ave","ae","Avestan");
        public static readonly Language639 Awadhia = new Language639("awa","Awadhi");
        public static readonly Language639 Aymara = new Language639("aym","ay","Aymara");
        public static readonly Language639 Azerbaijani = new Language639("aze","az","Azerbaijani");
        public static readonly Language639 Banda = new Language639("bad","Banda languages");
        public static readonly Language639 Bamileke = new Language639("bai","Bamileke languages");
        public static readonly Language639 Bashkir = new Language639("bak","ba","Bashkir");
        public static readonly Language639 Baluci = new Language639("bal","Baluchi");
        public static readonly Language639 Bambara = new Language639("bam","bm","Bambara");
        public static readonly Language639 Balinese = new Language639("ban","Balinese");
        public static readonly Language639 Basque = new Language639("baq", "eus", "eu", "Basque");
        public static readonly Language639 Basa = new Language639("bas","Basa");
        public static readonly Language639 Baltic = new Language639("bat","Baltic languages");
        public static readonly Language639 Beja = new Language639("bej","Beja; Bedawiyet");
        public static readonly Language639 Belarusian = new Language639("bel","be","Belarusian");
        public static readonly Language639 Bemba = new Language639("bem","Bemba");
        public static readonly Language639 Bengali = new Language639("ben","bn","Bengali");
        public static readonly Language639 Berber = new Language639("ber","Berber languages");
        public static readonly Language639 Bhojpuri = new Language639("bho","Bhojpuri");
        public static readonly Language639 Bihari = new Language639("bih","bh","Bihari languages");
        public static readonly Language639 Bikol = new Language639("bik","Bikol");
        public static readonly Language639 Bini = new Language639("bin","Bini; Edo");
        public static readonly Language639 Bislama = new Language639("bis","bi","Bislama");
        public static readonly Language639 Siksika = new Language639("bla","Siksika");
        public static readonly Language639 Bantu = new Language639("bnt","Bantu languages");
        public static readonly Language639 Tibetan = new Language639("tib", "bod", "bo","Tibetan");
        public static readonly Language639 Bosnian = new Language639("bos","bs","Bosnian");
        public static readonly Language639 Braj = new Language639("bra","Braj");
        public static readonly Language639 Breton = new Language639("bre","br","Breton");
        public static readonly Language639 Batak = new Language639("btk	","Batak languages");
        public static readonly Language639 Buriat = new Language639("bua","Buriat");
        public static readonly Language639 Buginese = new Language639("bug","Buginese");
        public static readonly Language639 Bulgarian = new Language639("bul","bg","Bulgarian");
        public static readonly Language639 Burmese = new Language639("bur","mya", "my",	"Burmese");
        public static readonly Language639 Bilin = new Language639("byn","Blin; Bilin");
        public static readonly Language639 Caddo = new Language639("cad","Caddo");
        public static readonly Language639 CentralAmericanIndianLanguages = new Language639("cai","Central American Indian languages");
        public static readonly Language639 Galibi = new Language639("car","Galibi Carib");
        public static readonly Language639 Catalan = new Language639("cat","ca","Catalan; Valencian");
        public static readonly Language639 Caucasian = new Language639("cau","Caucasian languages");
        public static readonly Language639 Cebuano = new Language639("ceb","Cebuano");
        public static readonly Language639 Celtic = new Language639("cel","Celtic languages");
        public static readonly Language639 Czech = new Language639("cze", "ces", "cs", "Czech");
        public static readonly Language639 Chamorro = new Language639("cha","ch","Chamorro");
        public static readonly Language639 Chibcha = new Language639("chb","Chibcha");
        public static readonly Language639 Chechen = new Language639("che","ce","Chechen");
        public static readonly Language639 Chagatai = new Language639("chg","Chagatai");
        public static readonly Language639 Chinese = new Language639("chi", "zho","zh","Chinese");
        public static readonly Language639 Chuukese = new Language639("chk","Chuukese");
        public static readonly Language639 Mari = new Language639("chm","Mari");
        public static readonly Language639 Chinook = new Language639("chn","Chinook jargon");
        public static readonly Language639 Choctaw = new Language639("cho","Choctaw");
        public static readonly Language639 Chipewyan = new Language639("chp","Chipewyan; Dene Suline");
        public static readonly Language639 Cherokee = new Language639("chr","Cherokee");
        public static readonly Language639 OldBulgarian = new Language639("chu","cu","Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic");
        public static readonly Language639 Chuvash = new Language639("chv","cv","Chuvash");
        public static readonly Language639 Cheyenne = new Language639("chy","Cheyenne");
        public static readonly Language639 Chamic = new Language639("cmc","Chamic languages");
        public static readonly Language639 Coptic = new Language639("cop","Coptic");
        public static readonly Language639 Cornish = new Language639("cor","kw","Cornish");
        public static readonly Language639 Corsican = new Language639("cos","co","Corsican");
        public static readonly Language639 EnglishCreoles = new Language639("cpe","Creoles and pidgins, English based");
        public static readonly Language639 FrenchCreoles = new Language639("cpf","Creoles and pidgins, French-based");
        public static readonly Language639 PortugueseCreoles = new Language639("cpp","Creoles and pidgins, Portuguese-based");
        public static readonly Language639 Cree = new Language639("cre","cr","Cree");
        public static readonly Language639 CrimeanTatar = new Language639("crh","Crimean Tatar; Crimean Turkish");
        public static readonly Language639 Creoles = new Language639("crp","Creoles and pidgins");
        public static readonly Language639 Kashubian = new Language639("csb","Kashubian");
        public static readonly Language639 Cushitic = new Language639("cus","Cushitic languages");
        public static readonly Language639 Welsh = new Language639("wel", "cym","cy","Welsh");
        public static readonly Language639 Dakota = new Language639("dak","Dakota");
        public static readonly Language639 Danish = new Language639("dan","da","Danish");
        public static readonly Language639 Dargwa = new Language639("dar","Dargwa");
        public static readonly Language639 LandDayak = new Language639("day","Land Dayak languages");
        public static readonly Language639 Delaware = new Language639("del","Delaware");
        public static readonly Language639 Slave = new Language639("den","Slave (Athapascan)");
        public static readonly Language639 Dogrib = new Language639("dgr","Dogrib");
        public static readonly Language639 Dinka = new Language639("din","Dinka");
        public static readonly Language639 Divehi = new Language639("div","dv","Divehi; Dhivehi; Maldivian");
        public static readonly Language639 Dogri = new Language639("doi","Dogri");
        public static readonly Language639 Dravidian = new Language639("dra","Dravidian languages");
        public static readonly Language639 LowerSorbian = new Language639("dsb","Lower Sorbian");
        public static readonly Language639 Duala = new Language639("dua","Duala");
        public static readonly Language639 MiddleDutch = new Language639("dum","Dutch, Middle (ca.1050-1350)");
        public static readonly Language639 Dutch = new Language639("dut", "nld", "nl", "Dutch; Flemish");
        public static readonly Language639 Dyula = new Language639("dyu","Dyula");
        public static readonly Language639 Dzongkha = new Language639("dzo","dz","Dzongkha");
        public static readonly Language639 Efik = new Language639("efi","Efik");
        public static readonly Language639 AncientEgyptian = new Language639("egy","Egyptian (Ancient)");
        public static readonly Language639 Ekajuk = new Language639("eka","Ekajuk");
        public static readonly Language639 Greek = new Language639("gre", "ell", "el", "Greek, Modern (1453-)");
        public static readonly Language639 Elamite = new Language639("elx","Elamite");
        public static readonly Language639 English = new Language639("eng","en","English");
        public static readonly Language639 MiddleEnglish = new Language639("enm","English, Middle (1100-1500)");
        public static readonly Language639 Esperanto = new Language639("epo","eo","Esperanto");
        public static readonly Language639 Estonian = new Language639("est","et","Estonian");
        public static readonly Language639 Ewe = new Language639("ewe","ee","Ewe");
        public static readonly Language639 Ewondo = new Language639("ewo","Ewondo");
        public static readonly Language639 Fang = new Language639("fan","Fang");
        public static readonly Language639 Faroese = new Language639("fao","fo","Faroese");
        public static readonly Language639 Persian = new Language639("per", "fas", "fa", "Persian");
        public static readonly Language639 Fanti = new Language639("fat","Fanti");
        public static readonly Language639 Fijian = new Language639("fij","fj","Fijian");
        public static readonly Language639 Filipino = new Language639("fil","Filipino; Pilipino");
        public static readonly Language639 Finnish = new Language639("fin","fi","Finnish");
        public static readonly Language639 FinnoUgurian = new Language639("fiu","Finno-Ugrian languages");
        public static readonly Language639 Fon = new Language639("fon","Fon");
        public static readonly Language639 French = new Language639("fre", "fra", "fr", "French");
        public static readonly Language639 MiddleFrench = new Language639("frm","French, Middle (ca.1400-1600)");
        public static readonly Language639 OldFrench = new Language639("fro","French, Old (842-ca.1400)");
        public static readonly Language639 NorthernFrisian = new Language639("frr","Northern Frisian");
        public static readonly Language639 EasternFrisian = new Language639("frs","Eastern Frisian");
        public static readonly Language639 WesternFrisian = new Language639("fry","fy","Western Frisian");
        public static readonly Language639 Fulah = new Language639("ful","ff","Fulah");
        public static readonly Language639 Friulian = new Language639("fur","Friulian");
        public static readonly Language639 Ga = new Language639("gaa","Ga");
        public static readonly Language639 Gayo = new Language639("gay","Gayo");
        public static readonly Language639 Gbaya = new Language639("gba","Gbaya");
        public static readonly Language639 GermanicLanguages = new Language639("gem", "Germanic languages");
        public static readonly Language639 Georgian = new Language639("geo", "kat", "ka", "Georgian");
        public static readonly Language639 German = new Language639("ger", "deu", "de", "German");
        public static readonly Language639 Geez = new Language639("gez","Geez");
        public static readonly Language639 Gilbertese = new Language639("gil","Gilbertese");
        public static readonly Language639 Gaelic = new Language639("gla","gd","Gaelic; Scottish Gaelic");
        public static readonly Language639 Irish = new Language639("gle","ga","Irish");
        public static readonly Language639 Galician = new Language639("glg","gl","Galician");
        public static readonly Language639 Manx = new Language639("glv","gv","Manx");
        public static readonly Language639 MiddleHighGerman = new Language639("gmh","German, Middle High (ca.1050-1500)");
        public static readonly Language639 OldHighGerman = new Language639("goh","German, Old High (ca.750-1050)");
        public static readonly Language639 Gondi = new Language639("gon","Gondi");
        public static readonly Language639 Gorontalo = new Language639("gor","Gorontalo");
        public static readonly Language639 Gothic = new Language639("got","Gothic");
        public static readonly Language639 Grebo = new Language639("grb","Grebo");
        public static readonly Language639 AncientGreek = new Language639("grc","Greek, Ancient (to 1453)");
        public static readonly Language639 Guarani = new Language639("grn","gn","Guarani");
        public static readonly Language639 SwissGerman = new Language639("gsw","Swiss German; Alemannic; Alsatian");
        public static readonly Language639 Gujarati = new Language639("guj","gu","Gujarati");
        public static readonly Language639 Gwichin = new Language639("gwi","Gwich'in");
        public static readonly Language639 Haida = new Language639("hai","Haida");
        public static readonly Language639 Haitian = new Language639("hat","ht","Haitian; Haitian Creole");
        public static readonly Language639 Hausa = new Language639("hau","ha","Hausa");
        public static readonly Language639 Hawaiian = new Language639("haw","Hawaiian");
        public static readonly Language639 Hebrew = new Language639("heb","he","Hebrew");
        public static readonly Language639 Herero = new Language639("her","hz","Herero");
        public static readonly Language639 Hiligaynon = new Language639("hil","Hiligaynon");
        public static readonly Language639 Himachali = new Language639("him","Himachali languages; Western Pahari languages");
        public static readonly Language639 Hindi = new Language639("hin","hi","Hindi");
        public static readonly Language639 Hittite = new Language639("hit","Hittite");
        public static readonly Language639 Hmong = new Language639("hmn","Hmong; Mong");
        public static readonly Language639 HiriMotu = new Language639("hmo","ho","Hiri Motu");
        public static readonly Language639 Croatian = new Language639("hrv","hr","Croatian");
        public static readonly Language639 UpperSorbian = new Language639("hsb","Upper Sorbian");
        public static readonly Language639 Hungarian = new Language639("hun","hu","Hungarian");
        public static readonly Language639 Hupa = new Language639("hup","Hupa");
        public static readonly Language639 Iban = new Language639("iba","Iban");
        public static readonly Language639 Igbo = new Language639("ibo","ig","Igbo");
        public static readonly Language639 Ido = new Language639("ido","io","Ido");
        public static readonly Language639 SichuanYi = new Language639("iii","ii","Sichuan Yi; Nuosu");
        public static readonly Language639 Ijo = new Language639("ijo","Ijo languages");
        public static readonly Language639 Inuktitut = new Language639("iku","iu","Inuktitut");
        public static readonly Language639 InterlingueOccidental = new Language639("ile","ie","Interlingue; Occidental");
        public static readonly Language639 Iloko = new Language639("ilo","Iloko");
        public static readonly Language639 Interlingua = new Language639("ina","ia","Interlingua (International Auxiliary Language Association)");
        public static readonly Language639 Indic = new Language639("inc","Indic languages");
        public static readonly Language639 Indonesian = new Language639("ind","id","Indonesian");
        public static readonly Language639 IndoEuropean = new Language639("ine","Indo-European languages");
        public static readonly Language639 Ingush = new Language639("inh","Ingush");
        public static readonly Language639 Inupiaq = new Language639("ipk","ik","Inupiaq");
        public static readonly Language639 Iranian = new Language639("ira","Iranian languages");
        public static readonly Language639 Iroquoian = new Language639("iro","Iroquoian languages");
        public static readonly Language639 Icelandic = new Language639("ice","isl","is","Icelandic");
        public static readonly Language639 Italian = new Language639("ita","it","Italian");
        public static readonly Language639 Javanese = new Language639("jav","jv","Javanese");
        public static readonly Language639 Lojban = new Language639("jbo","Lojban");
        public static readonly Language639 Japanese = new Language639("jpn","ja","Japanese");
        public static readonly Language639 JudeoPersian = new Language639("jpr","Judeo-Persian");
        public static readonly Language639 JudeoArabic = new Language639("jrb","Judeo-Arabic");
        public static readonly Language639 KaraKalpak = new Language639("kaa","Kara-Kalpak");
        public static readonly Language639 Kabyle = new Language639("kab","Kabyle");
        public static readonly Language639 Kachin = new Language639("kac","Kachin; Jingpho");
        public static readonly Language639 Kalallisut = new Language639("kal","kl","Kalaallisut; Greenlandic");
        public static readonly Language639 Kamba = new Language639("kam","Kamba");
        public static readonly Language639 Kannada = new Language639("kan","kn","Kannada");
        public static readonly Language639 Karen = new Language639("kar","Karen languages");
        public static readonly Language639 Kashmiri = new Language639("kas","ks","Kashmiri");
        public static readonly Language639 Kanuri = new Language639("kau","kr","Kanuri");
        public static readonly Language639 Kawi = new Language639("kaw","Kawi");
        public static readonly Language639 Kazakh = new Language639("kaz","kk","Kazakh");
        public static readonly Language639 Kabardian = new Language639("kbd","Kabardian");
        public static readonly Language639 Khasi = new Language639("kha","Khasi");
        public static readonly Language639 Khoisan = new Language639("khi","Khoisan languages");
        public static readonly Language639 CentralKhmer = new Language639("khm","km","Central Khmer");
        public static readonly Language639 Khotanese = new Language639("kho","Khotanese; Sakan");
        public static readonly Language639 Kikuyu = new Language639("kik","ki","Kikuyu; Gikuyu");
        public static readonly Language639 Kinyarwanda = new Language639("kin","rw","Kinyarwanda");
        public static readonly Language639 Kirghiz = new Language639("kir","ky","Kirghiz; Kyrgyz");
        public static readonly Language639 Kimbundu = new Language639("kmb","Kimbundu");
        public static readonly Language639 Konkani = new Language639("kok","Konkani");
        public static readonly Language639 Komi = new Language639("kom","kv","Komi");
        public static readonly Language639 Kongo = new Language639("kon","kg","Kongo");
        public static readonly Language639 Korean = new Language639("kor","ko","Korean");
        public static readonly Language639 Kosraean = new Language639("kos","Kosraean");
        public static readonly Language639 Kpelle = new Language639("kpe","Kpelle");
        public static readonly Language639 Karachay = new Language639("krc","Karachay-Balkar");
        public static readonly Language639 Karelian = new Language639("krl","Karelian");
        public static readonly Language639 Kru = new Language639("kro","Kru languages");
        public static readonly Language639 Kurukh = new Language639("kru","Kurukh");
        public static readonly Language639 Kuanyama = new Language639("kua","kj","Kuanyama; Kwanyama");
        public static readonly Language639 Kumyk = new Language639("kum","Kumyk");
        public static readonly Language639 Kurdish = new Language639("kur","ku","Kurdish");
        public static readonly Language639 Kutenai = new Language639("kut","Kutenai");
        public static readonly Language639 Ladino = new Language639("lad","Ladino");
        public static readonly Language639 Lahnda = new Language639("lah","Lahnda");
        public static readonly Language639 Lamba = new Language639("lam","Lamba");
        public static readonly Language639 Lao = new Language639("lao","lo","Lao");
        public static readonly Language639 Latin = new Language639("lat","la","Latin");
        public static readonly Language639 Latvian = new Language639("lav","lv","Latvian");
        public static readonly Language639 Lezghian = new Language639("lez","Lezghian");
        public static readonly Language639 Limburgan = new Language639("lim","li","Limburgan; Limburger; Limburgish");
        public static readonly Language639 Lingala = new Language639("lin","ln","Lingala");
        public static readonly Language639 Lithuanian = new Language639("lit","lt","Lithuanian");
        public static readonly Language639 Mongo = new Language639("lol","Mongo");
        public static readonly Language639 Lozi = new Language639("loz","Lozi");
        public static readonly Language639 Luxembourgish = new Language639("ltz","lb","Luxembourgish; Letzeburgesch");
        public static readonly Language639 LubaLulua = new Language639("lua","Luba-Lulua");
        public static readonly Language639 LubaKatanga = new Language639("lub","lu","Luba-Katanga");
        public static readonly Language639 Ganda = new Language639("lug","lg","Ganda");
        public static readonly Language639 Luiseno = new Language639("lui","Luiseno");
        public static readonly Language639 Lunda = new Language639("lun","Lunda");
        public static readonly Language639 Luo = new Language639("luo","Luo (Kenya and Tanzania)");
        public static readonly Language639 Lushai = new Language639("lus","Lushai");
        public static readonly Language639 Macedonian = new Language639("mac","mkd","mk","Macedonian");
        public static readonly Language639 Madurese = new Language639("mad","Madurese");
        public static readonly Language639 Magahi = new Language639("mag","Magahi");
        public static readonly Language639 Marshallese = new Language639("mah","mh","Marshallese");
        public static readonly Language639 Maithili = new Language639("mai","Maithili");
        public static readonly Language639 Makasar = new Language639("mak","Makasar");
        public static readonly Language639 Malayalam = new Language639("mal","ml","Malayalam");
        public static readonly Language639 Mandingo = new Language639("man","Mandingo");
        public static readonly Language639 Maori = new Language639("mao","mri","mi","Maori");
        public static readonly Language639 Austronesian = new Language639("map","Austronesian languages");
        public static readonly Language639 Marathi = new Language639("mar","mr","Marathi");
        public static readonly Language639 Masai = new Language639("mas","Masai");
        public static readonly Language639 Malay = new Language639("may","msa","ms","Malay");
        public static readonly Language639 Moksha = new Language639("mdf","Moksha");
        public static readonly Language639 Mandar = new Language639("mdr","Mandar");
        public static readonly Language639 Mende = new Language639("men","Mende");
        public static readonly Language639 MiddleIrish = new Language639("mga","Irish, Middle (900-1200)");
        public static readonly Language639 Micmac = new Language639("mic","Mi'kmaq; Micmac");
        public static readonly Language639 Minangkabau = new Language639("min","Minangkabau");
        public static readonly Language639 UncodedLanguages = new Language639("mis","Uncoded languages");
        public static readonly Language639 MonKhmer = new Language639("mkh","Mon-Khmer languages");
        public static readonly Language639 Malagasy = new Language639("mlg","mg","Malagasy");
        public static readonly Language639 Maltese = new Language639("mlt","mt","Maltese");
        public static readonly Language639 Manchu = new Language639("mnc","Manchu");
        public static readonly Language639 Manipuri = new Language639("mni","Manipuri");
        public static readonly Language639 Manobo = new Language639("mno","Manobo languages");
        public static readonly Language639 Mohawk = new Language639("moh","Mohawk");
        public static readonly Language639 Mongolian = new Language639("mon","mn","Mongolian");
        public static readonly Language639 Mossi = new Language639("mos","Mossi");
        public static readonly Language639 Mao = new Language639("mao","mri","mi","Maori");
        public static readonly Language639 May = new Language639("may","msa","ms","Malay");
        public static readonly Language639 MultipleLanguages = new Language639("mul","Multiple languages");
        public static readonly Language639 MundaLanguages = new Language639("mun","Munda languages");
        public static readonly Language639 Creek = new Language639("mus","Creek");
        public static readonly Language639 Mirandese = new Language639("mwl","Mirandese");
        public static readonly Language639 Marwari = new Language639("mwr","Marwari");
        public static readonly Language639 Mayan = new Language639("myn","Mayan languages");
        public static readonly Language639 Erzya = new Language639("myv","Erzya");
        public static readonly Language639 Nahuatl = new Language639("nah","Nahuatl languages");
        public static readonly Language639 NorthAmericanIndian = new Language639("nai","North American Indian languages");
        public static readonly Language639 Neapolitan = new Language639("nap","Neapolitan");
        public static readonly Language639 Nauru = new Language639("nau","na","Nauru");
        public static readonly Language639 Navajo = new Language639("nav","nv","Navajo; Navaho");
        public static readonly Language639 SouthNdebele = new Language639("nbl","nr","Ndebele, South; South Ndebele");
        public static readonly Language639 NorthNdebele = new Language639("nde","nd","Ndebele, North; North Ndebele");
        public static readonly Language639 Ndonga = new Language639("ndo","ng","Ndonga");
        public static readonly Language639 LowGerman = new Language639("nds","Low German; Low Saxon; German, Low; Saxon, Low");
        public static readonly Language639 Nepali = new Language639("nep","ne","Nepali");
        public static readonly Language639 NepalBhasa = new Language639("new","Nepal Bhasa; Newari");
        public static readonly Language639 Nias = new Language639("nia","Nias");
        public static readonly Language639 NigerKordofanian = new Language639("nic","Niger-Kordofanian languages");
        public static readonly Language639 Niuean = new Language639("niu","Niuean");
        public static readonly Language639 NorwegianNynorsk = new Language639("nno","nn","Norwegian Nynorsk; Nynorsk, Norwegian");
        public static readonly Language639 NorwegianBokmal = new Language639("nob","nb","Bokmål, Norwegian; Norwegian Bokmål");
        public static readonly Language639 Nogai = new Language639("nog","Nogai");
        public static readonly Language639 OldNorse = new Language639("non","Norse, Old");
        public static readonly Language639 Norwegian = new Language639("nor","no","Norwegian");
        public static readonly Language639 NKo = new Language639("nqo","N'Ko");
        public static readonly Language639 Pedi = new Language639("nso","Pedi; Sepedi; Northern Sotho");
        public static readonly Language639 Nubian = new Language639("nub","Nubian languages");
        public static readonly Language639 ClassicalNewari = new Language639("nwc","Classical Newari; Old Newari; Classical Nepal Bhasa");
        public static readonly Language639 Chichewa = new Language639("nya","ny","Chichewa; Chewa; Nyanja");
        public static readonly Language639 Nyamwezi = new Language639("nym","Nyamwezi");
        public static readonly Language639 Nyankole = new Language639("nyn","Nyankole");
        public static readonly Language639 Nyoro = new Language639("nyo","Nyoro");
        public static readonly Language639 Nzima = new Language639("nzi","Nzima");
        public static readonly Language639 Occitan = new Language639("oci","oc","Occitan (post 1500)");
        public static readonly Language639 Ojibway = new Language639("oji","oj","Ojibwa");
        public static readonly Language639 Oriya = new Language639("ori","or","Oriya");
        public static readonly Language639 Oromo = new Language639("orm","om","Oromo");
        public static readonly Language639 Osage = new Language639("osa","Osage");
        public static readonly Language639 Ossetian = new Language639("oss","os","Ossetian; Ossetic");
        public static readonly Language639 OttomanTurkish = new Language639("ota","Turkish, Ottoman (1500-1928)");
        public static readonly Language639 Otomian = new Language639("oto","Otomian languages");
        public static readonly Language639 Papuan = new Language639("paa","Papuan languages");
        public static readonly Language639 Pangasinan = new Language639("pag","Pangasinan");
        public static readonly Language639 Pahlavi = new Language639("pal","Pahlavi");
        public static readonly Language639 Pampanga = new Language639("pam","Pampanga; Kapampangan");
        public static readonly Language639 Panjabi = new Language639("pan","pa","Panjabi; Punjabi");
        public static readonly Language639 Papiamento = new Language639("pap","Papiamento");
        public static readonly Language639 Palauan= new Language639("pau","Palauan");
        public static readonly Language639 OldPersian = new Language639("peo","Persian, Old (ca.600-400 B.C.)");
        public static readonly Language639 PhilippineLanguages = new Language639("phi","Philippine languages");
        public static readonly Language639 Phoenician = new Language639("phn","Phoenician");
        public static readonly Language639 Pali = new Language639("pli","pi","Pali");
        public static readonly Language639 Polish = new Language639("pol","pl","Polish");
        public static readonly Language639 Pohnpeian = new Language639("pon", "Pohnpeian");
        public static readonly Language639 Portuguese = new Language639("por", "pt", "Portuguese");
        public static readonly Language639 Prakrit = new Language639("pra", "Prakrit languages");
        public static readonly Language639 OldProvencal = new Language639("pro", "Provençal, Old (to 1500); Occitan, Old (to 1500)");
        public static readonly Language639 Pushto = new Language639("pus", "ps", "Pushto; Pashto");
        public static readonly Language639 Quechua = new Language639("que", "qu", "Quechua");
        public static readonly Language639 Rajasthani = new Language639("raj", "Rajasthani");
        public static readonly Language639 Rapanui = new Language639("rap", "Rapanui");
        public static readonly Language639 Rarotongan = new Language639("rar", "Rarotongan; Cook Islands Maori");
        public static readonly Language639 RomanceLanguages = new Language639("roa", "Romance languages");
        public static readonly Language639 Romansh = new Language639("roh", "rm", "Romansh");
        public static readonly Language639 Romany = new Language639("rom", "Romany");
        public static readonly Language639 Romanian = new Language639("rum", "ron", "ro", "Romanian; Moldavian; Moldovan");
        public static readonly Language639 Rundi = new Language639("run", "rn", "Rundi");
        public static readonly Language639 Aromanian = new Language639("rup", "Aromanian; Arumanian; Macedo-Romanian");
        public static readonly Language639 Russian = new Language639("rus", "ru", "Russian");
        public static readonly Language639 Sandawe = new Language639("sad", "Sandawe");
        public static readonly Language639 Sango = new Language639("sag", "sg", "Sango");
        public static readonly Language639 Yakut = new Language639("sah", "Yakut");
        public static readonly Language639 SouthAmericanIndianLanguages = new Language639("sai", "South American Indian languages");
        public static readonly Language639 Salishan = new Language639("sal", "Salishan languages");
        public static readonly Language639 SamaritanAramaic = new Language639("sam", "Samaritan Aramaic");
        public static readonly Language639 Sanskrit = new Language639("san", "sa", "Sanskrit");
        public static readonly Language639 Sasak = new Language639("sas", "Sasak");
        public static readonly Language639 Santali = new Language639("sat", "Santali");
        public static readonly Language639 Sicilian = new Language639("scn", "Sicilian");
        public static readonly Language639 Scots = new Language639("sco", "Scots");
        public static readonly Language639 Selkup = new Language639("sel", "Selkup");
        public static readonly Language639 Semitic = new Language639("sem", "Semitic languages");
        public static readonly Language639 OldIrish = new Language639("sga", "Irish, Old (to 900)");
        public static readonly Language639 SignLanguages = new Language639("sgn", "Sign Languages");
        public static readonly Language639 Shan = new Language639("shn", "Shan");
        public static readonly Language639 Sidamo = new Language639("sid", "Sidamo");
        public static readonly Language639 Sinhala = new Language639("sin", "si", "Sinhala; Sinhalese");
        public static readonly Language639 Siouan = new Language639("sio", "Siouan languages");
        public static readonly Language639 SinoTibetan = new Language639("sit", "Sino-Tibetan languages");
        public static readonly Language639 Slavic = new Language639("sla", "Slavic languages");
        public static readonly Language639 Slovak = new Language639("slo", "slk", "sk", "Slovak");
        public static readonly Language639 Slovenian = new Language639("slv", "sl", "Slovenian");
        public static readonly Language639 SouthernSami = new Language639("sma", "Southern Sami");
        public static readonly Language639 NorthernSami = new Language639("sme", "se", "Northern Sami");
        public static readonly Language639 SamiLanguages = new Language639("smi", "Sami languages");
        public static readonly Language639 LuleSami = new Language639("smj", "Lule Sami");
        public static readonly Language639 InariSami = new Language639("smn", "Inari Sami");
        public static readonly Language639 Samoan = new Language639("smo", "sm", "Samoan");
        public static readonly Language639 SkoltSami = new Language639("sms", "Skolt Sami");
        public static readonly Language639 Shona = new Language639("sna", "sn", "Shona");
        public static readonly Language639 Sindhi = new Language639("snd", "sd", "Sindhi");
        public static readonly Language639 Sonike = new Language639("snk", "Soninke");
        public static readonly Language639 Sogdian = new Language639("sog", "Sogdian");
        public static readonly Language639 Somali = new Language639("som", "so", "Somali");
        public static readonly Language639 Songhai = new Language639("son", "Songhai languages");
        public static readonly Language639 Sotho = new Language639("sot", "st", "Sotho, Southern");
        public static readonly Language639 Spanish = new Language639("spa", "es", "Spanish; Castilian");
        public static readonly Language639 Sardinian = new Language639("srd", "sc", "Sardinian");
        public static readonly Language639 Sranan = new Language639("srn", "Sranan Tongo");
        public static readonly Language639 Serbian = new Language639("srp", "sr", "Serbian");
        public static readonly Language639 Serer = new Language639("srr", "Serer");
        public static readonly Language639 NiloSaharan = new Language639("ssa", "Nilo-Saharan languages");
        public static readonly Language639 Swati = new Language639("ssw", "ss", "Swati");
        public static readonly Language639 Sukuma = new Language639("suk", "Sukuma");
        public static readonly Language639 Sudanese = new Language639("sun", "su", "Sundanese");
        public static readonly Language639 Susu = new Language639("sus", "Susu");
        public static readonly Language639 Sumerian = new Language639("sux", "Sumerian");
        public static readonly Language639 Swahili = new Language639("swa", "sw", "Swahili");
        public static readonly Language639 Swedish = new Language639("swe", "sv", "Swedish");
        public static readonly Language639 ClassicalSyriac = new Language639("syc", "Classical Syriac");
        public static readonly Language639 Syriac = new Language639("syr", "Syriac");
        public static readonly Language639 Tahitian = new Language639("tah","ty","Tahitian");
        public static readonly Language639 TaiLanguages = new Language639("tai","Tai languages");
        public static readonly Language639 Tamil = new Language639("tam","ta","Tamil");
        public static readonly Language639 Tatar = new Language639("tat","tt","Tatar");
        public static readonly Language639 Teluga = new Language639("tel","te","Telugu");
        public static readonly Language639 Timne = new Language639("tem","Timne");
        public static readonly Language639 Tereno = new Language639("ter","Tereno");
        public static readonly Language639 Tetum = new Language639("tet","Tetum");
        public static readonly Language639 Tajik = new Language639("tgk","tg","Tajik");
        public static readonly Language639 Tagalog = new Language639("tgl","tl","Tagalog");
        public static readonly Language639 Thai = new Language639("tha","th","Thai");
        public static readonly Language639 Tigre = new Language639("tig","Tigre");
        public static readonly Language639 Tigrinya = new Language639("tir","ti","Tigrinya");
        public static readonly Language639 Tiv = new Language639("tiv","Tiv");
        public static readonly Language639 Tokelau = new Language639("tkl","Tokelau");
        public static readonly Language639 Klingon = new Language639("tlh","Klingon; tlhIngan-Hol");
        public static readonly Language639 Tlingit = new Language639("tli","Tlingit");
        public static readonly Language639 Tamashek = new Language639("tmh","Tamashek");
        public static readonly Language639 TongaNyasa = new Language639("tog","Tonga (Nyasa)");
        public static readonly Language639 Tonga = new Language639("ton","to","Tonga (Tonga Islands)");
        public static readonly Language639 TokPisin = new Language639("tpi","Tok Pisin");
        public static readonly Language639 Tsimshian = new Language639("tsi","Tsimshian");
        public static readonly Language639 Tswana = new Language639("tsn","tn","Tswana");
        public static readonly Language639 Tsonga = new Language639("tso","ts","Tsonga");
        public static readonly Language639 Turkmen = new Language639("tuk","tk","Turkmen");
        public static readonly Language639 Tumbuka = new Language639("tum","Tumbuka");
        public static readonly Language639 Tupi = new Language639("tup","Tupi languages");
        public static readonly Language639 Turkish = new Language639("tur","tr","Turkish");
        public static readonly Language639 Altaic = new Language639("tut","Altaic languages");
        public static readonly Language639 Tuvalu = new Language639("tvl","Tuvalu");
        public static readonly Language639 Twi = new Language639("twi","tw","Twi");
        public static readonly Language639 Tuvian = new Language639("tyv","Tuvinian");
        public static readonly Language639 Udmurt = new Language639("udm","Udmurt");
        public static readonly Language639 Ugaritic = new Language639("uga","Ugaritic");
        public static readonly Language639 Uighur = new Language639("uig","ug","Uighur; Uyghur");
        public static readonly Language639 Ukrainian = new Language639("ukr","uk","Ukrainian");
        public static readonly Language639 Umbundu = new Language639("umb","Umbundu");
        public static readonly Language639 Undetermined = new Language639("und","Undetermined");
        public static readonly Language639 Urdu = new Language639("urd","ur","Urdu");
        public static readonly Language639 Uzbek = new Language639("uzb","uz","Uzbek");
        public static readonly Language639 Vai = new Language639("vai","Vai");
        public static readonly Language639 Venda = new Language639("ven","ve","Venda");
        public static readonly Language639 Vietnamese = new Language639("vie","vi","Vietnamese");
        public static readonly Language639 Volapuk = new Language639("vol","vo","Volapük");
        public static readonly Language639 Votic = new Language639("vot","Votic");          
        public static readonly Language639 Wakashan = new Language639("wak","Wakashan languages");
        public static readonly Language639 Walaitta = new Language639("wal","Wolaitta; Wolaytta");
        public static readonly Language639 Waray = new Language639("war","Waray");
        public static readonly Language639 Washo = new Language639("was","Washo");
        public static readonly Language639 Sorbian = new Language639("wen","Sorbian languages");
        public static readonly Language639 Walloon = new Language639("wln","wa","Walloon");
        public static readonly Language639 Wolof = new Language639("wol","wo","Wolof");
        public static readonly Language639 Kalmyk = new Language639("xal","Kalmyk; Oirat");
        public static readonly Language639 Xhosa = new Language639("xho","xh","Xhosa");
        public static readonly Language639 Yao = new Language639("yao","Yao");
        public static readonly Language639 Yapese = new Language639("yap","Yapese");
        public static readonly Language639 Yiddish = new Language639("yid","yi","Yiddish");
        public static readonly Language639 Yor = new Language639("yor","yo","Yoruba");
        public static readonly Language639 Yupik = new Language639("ypk","Yupik languages");
        public static readonly Language639 Zapotec = new Language639("zap","Zapotec");
        public static readonly Language639 Bliss = new Language639("zbl","Blissymbols; Blissymbolics; Bliss");
        public static readonly Language639 Zenaga = new Language639("zen","Zenaga");
        public static readonly Language639 Zhuang = new Language639("zha","za","Zhuang; Chuang");
        public static readonly Language639 Zande = new Language639("znd","Zande languages");
        public static readonly Language639 Zulu = new Language639("zul","zu","Zulu");
        public static readonly Language639 Zuni = new Language639("zun","Zuni");
        public static readonly Language639 NotApplicable = new Language639("zxx","No linguistic content; Not applicable");
        public static readonly Language639 Zaza = new Language639("zza", "Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki");

        #endregion
    }
}
