using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Culture
{
    public class Script
        : IScript
    {
        private Script(string code, int number, string name)
        {
            this.code = code;
            this.number = number;
            this.name = name;
        }

        private readonly string code;
        private readonly int number;
        private readonly string name;

        #region IScript Members

        public string Code
        {
            get { return code; }
        }

        public int Number
        {
            get { return number; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            return code;
        }

        static Script()
        {
            InitializeScripts();

            foreach (var script in scripts)
            {
                byCode.Add(script.Code.ToUpper(), script);
                byName.Add(script.Name.ToUpper(), script);
                byNumber.Add(script.Number, script);
            }
        }

        private static readonly IList<IScript> scripts = new List<IScript>();
        private static readonly IDictionary<string, IScript> byCode = new Dictionary<string, IScript>();
        private static readonly IDictionary<string, IScript> byName = new Dictionary<string, IScript>();
        private static readonly IDictionary<int, IScript> byNumber = new Dictionary<int, IScript>();

        #region InitializeScripts

        private static void InitializeScripts()
        {
            scripts.Add(Afaka);
            scripts.Add(Arabic);
            scripts.Add(ImperialAramaic);
            scripts.Add(Armenian);
            scripts.Add(Avestan);
            scripts.Add(Balinese);
            scripts.Add(Bamum);
            scripts.Add(BassaVah);
            scripts.Add(Batak);
            scripts.Add(Bengali);
            scripts.Add(Blissymbols);
            scripts.Add(Bopomofo);
            scripts.Add(Brahmi);
            scripts.Add(Braille);
            scripts.Add(Burginese);
            scripts.Add(Buhid);
            scripts.Add(Chakma);
            scripts.Add(UnifiedCanadianAboriginal);
            scripts.Add(Carian);
            scripts.Add(Cham);
            scripts.Add(Cherokee);
            scripts.Add(Cirth);
            scripts.Add(Coptic);
            scripts.Add(Cypriot);
            scripts.Add(Cyrillic);
            scripts.Add(OldChurchCyrillic);
            scripts.Add(Devanagari);
            scripts.Add(Deseret);
            scripts.Add(Duployan);
            scripts.Add(EgyptianDemotic);
            scripts.Add(EgyptianHieratic);
            scripts.Add(EgyptianHieroglyphics);
            scripts.Add(Elba);
            scripts.Add(Ethiopic);
            scripts.Add(Khutsuri);
            scripts.Add(Georgian);
            scripts.Add(Glagolitic);
            scripts.Add(Gothic);
            scripts.Add(Grantha);
            scripts.Add(Greek);
            scripts.Add(Gujarati);
            scripts.Add(Gurmukhi);
            scripts.Add(Hangul);
            scripts.Add(HanziKanjiHanja);
            scripts.Add(Hanunoo);
            scripts.Add(HanSimplified);
            scripts.Add(HanTraditional);
            scripts.Add(Hebrew);
            scripts.Add(Hiragana);
            scripts.Add(PahawhHmong);
            scripts.Add(HiraganaKatakana);
            scripts.Add(OldHungarian);
            scripts.Add(Indus);
            scripts.Add(OldItalic);
            scripts.Add(Java);
            scripts.Add(Japanese);
            scripts.Add(Jurchen);
            scripts.Add(KayahLi);
            scripts.Add(Katakana);
            scripts.Add(Kharishthi);
            scripts.Add(Khmer);
            scripts.Add(Kannada);
            scripts.Add(Korean);
            scripts.Add(Kpelle);
            scripts.Add(Kaithi);
            scripts.Add(TaiTham);
            scripts.Add(Lao);
            scripts.Add(LatinFraktur);
            scripts.Add(LatinGaelic);
            scripts.Add(Latin);
            scripts.Add(Lepcha);
            scripts.Add(Limbu);
            scripts.Add(LinearA);
            scripts.Add(LinearB);
            scripts.Add(Lisu);
            scripts.Add(Loma);
            scripts.Add(Lycian);
            scripts.Add(Lydian);
            scripts.Add(Mandaic);
            scripts.Add(Manichaean);
            scripts.Add(MyanHieroglyphs);
            scripts.Add(Mende);
            scripts.Add(MeroiticCursive);
            scripts.Add(MeroiticHieroglyphs);
            scripts.Add(Malayalam);
            scripts.Add(Mongolian);
            scripts.Add(Moon);
            scripts.Add(Mro);
            scripts.Add(MeiteiMayek);
            scripts.Add(Myanmar);
            scripts.Add(OldNorthArabian);
            scripts.Add(Nabataean);
            scripts.Add(NakhiGeba);
            scripts.Add(NKo);
            scripts.Add(Nushu);
            scripts.Add(Ogham);
            scripts.Add(OlChiki);
            scripts.Add(OldTurkic);
            scripts.Add(Oriya);
            scripts.Add(Osmanya);
            scripts.Add(Palmyrene);
            scripts.Add(OldPermic);
            scripts.Add(PhagsPa);
            scripts.Add(InscriptionalPahlavi);
            scripts.Add(PsalterPahlavi);
            scripts.Add(BookPahlavi);
            scripts.Add(Phoenician);
            scripts.Add(Miao);
            scripts.Add(InscriptionalParthian);
            scripts.Add(Rejang);
            scripts.Add(Rongorongo);
            scripts.Add(Runic);
            scripts.Add(Samaritan);
            scripts.Add(Sarati);
            scripts.Add(OldSouthArabian);
            scripts.Add(Saurashtra);
            scripts.Add(SignWriting);
            scripts.Add(Shavian);
            scripts.Add(Sharada);
            scripts.Add(Sindhi);
            scripts.Add(Sinhala);
            scripts.Add(SoraSompeng);
            scripts.Add(Sudanese);
            scripts.Add(Syloti);
            scripts.Add(Syriac);
            scripts.Add(EstrangeloSyriac);
            scripts.Add(WesternSyriac);
            scripts.Add(EasternSyriac);
            scripts.Add(Tagbanwa);
            scripts.Add(Takri);
            scripts.Add(TaiLe);
            scripts.Add(NewTaiLue);
            scripts.Add(Tamil);
            scripts.Add(Tangut);
            scripts.Add(TaiViet);
            scripts.Add(Telugu);
            scripts.Add(Tengwar);
            scripts.Add(Tifinagh);
            scripts.Add(Tagalog);
            scripts.Add(Thaana);
            scripts.Add(Thai);
            scripts.Add(Tibetan);
            scripts.Add(Ugaritic);
            scripts.Add(Vai);
            scripts.Add(VisibleSpeech);
            scripts.Add(WarangCiti);
            scripts.Add(Woleai);
            scripts.Add(OldPersian);
            scripts.Add(Cuneiform);
            scripts.Add(Yi);
            scripts.Add(Inherited);
            scripts.Add(MathematicalNotation);
            scripts.Add(Symbols);
            scripts.Add(Unwritten);
            scripts.Add(Undetermined);
            scripts.Add(Uncoded);
        }

        #endregion

        #region Public Static Methods

        public static IScript GetScriptByCode(string code)
        {
            if (code == null)
                return Undetermined;

            var upper = code.ToUpper();

            return byCode.ContainsKey(upper) ? byCode[upper] : Undetermined;
        }

        public static IScript GetScriptByName(string name)
        {
            if (name == null)
                return Undetermined;

            var upper = name.ToUpper();

            return byName.ContainsKey(upper) ? byName[upper] : Undetermined;
        }

        public static IScript GetScriptByNumber(int number)
        {
            return byNumber.ContainsKey(number) ? byNumber[number] : Undetermined;
        }

        public static IEnumerable<IScript> GetScripts()
        {
            return scripts;
        }

        #endregion

        #region Scripts

        public static readonly IScript Afaka = new Script("Afak", 439, "Afaka");
        public static readonly IScript Arabic = new Script("Arab", 160, "Arabic");
        public static readonly IScript ImperialAramaic = new Script("Armi", 124, "Imperial Aramaic");
        public static readonly IScript Armenian = new Script("Armn", 230, "Armenian");
        public static readonly IScript Avestan = new Script("Avst", 134, "Avestan");
        public static readonly IScript Balinese = new Script("Bali", 360, "Balinese");
        public static readonly IScript Bamum = new Script("Bamu", 435, "Bamum");
        public static readonly IScript BassaVah = new Script("Bass", 259, "Bassa Vah");
        public static readonly IScript Batak = new Script("Batk", 365, "Batak");
        public static readonly IScript Bengali = new Script("Beng", 325, "Bengali");
        public static readonly IScript Blissymbols = new Script("Blis", 550, "Blissymbols");
        public static readonly IScript Bopomofo = new Script("Bopo", 285, "Bopomofo");
        public static readonly IScript Brahmi = new Script("Brah", 300, "Brahmi");
        public static readonly IScript Braille = new Script("Brai", 570, "Braille");
        public static readonly IScript Burginese = new Script("Bugi", 367, "Buginese");
        public static readonly IScript Buhid = new Script("Buhd", 372, "Buhid");
        public static readonly IScript Chakma = new Script("Cakm", 349, "Chakma");
        public static readonly IScript UnifiedCanadianAboriginal = new Script("Cans", 440, "Unified Canadian Aboriginal Syllabics");
        public static readonly IScript Carian = new Script("Cari", 201, "Carian");
        public static readonly IScript Cham = new Script("Cham", 358, "Cham");
        public static readonly IScript Cherokee = new Script("Cher", 445, "Cherokee");
        public static readonly IScript Cirth = new Script("Cirt", 291, "Cirth");
        public static readonly IScript Coptic = new Script("Copt", 204, "Coptic");
        public static readonly IScript Cypriot = new Script("Cprt", 403, "Cypriot");
        public static readonly IScript Cyrillic = new Script("Cyrl", 220, "Cyrillic");
        public static readonly IScript OldChurchCyrillic = new Script("Cyrs", 221, "Cyrillic (Old Church Slavonic variant)");
        public static readonly IScript Devanagari = new Script("Deva", 315, "Devanagari (Nagari)");
        public static readonly IScript Deseret = new Script("Dsrt", 250, "Deseret (Mormon)");
        public static readonly IScript Duployan = new Script("Dupl", 755, "Duployan shorthand");
        public static readonly IScript EgyptianDemotic = new Script("Egyd", 70, "Egyptian demotic");
        public static readonly IScript EgyptianHieratic = new Script("Egyh", 60, "Egyptian hieratic");
        public static readonly IScript EgyptianHieroglyphics = new Script("Egyp", 50, "Egyptian hieroglyphs");
        public static readonly IScript Elba = new Script("Elba", 226, "Elbasan");
        public static readonly IScript Ethiopic = new Script("Ethi", 430, "Ethiopic (Ge'ez)");
        public static readonly IScript Khutsuri = new Script("Geok", 241, "Khutsuri (Asomtavruli and Nuskhuri)");
        public static readonly IScript Georgian = new Script("Geor", 240, "Georgian (Mkhedruli)");
        public static readonly IScript Glagolitic = new Script("Glag", 225, "Glagolitic");
        public static readonly IScript Gothic = new Script("Goth", 206, "Gothic");
        public static readonly IScript Grantha = new Script("Gran", 343, "Grantha");
        public static readonly IScript Greek = new Script("Grek", 200, "Greek");
        public static readonly IScript Gujarati = new Script("Gujr", 320, "Gujarati");
        public static readonly IScript Gurmukhi = new Script("Guru", 310, "Gurmukhi");
        public static readonly IScript Hangul = new Script("Hang", 286, "Hangul (Hangul, Hangeul)");
        public static readonly IScript HanziKanjiHanja = new Script("Hani", 500, "Han (Hanzi, Kanji, Hanja)");
        public static readonly IScript Hanunoo = new Script("Hano", 371, "Hanunoo (Hanunóo)");
        public static readonly IScript HanSimplified = new Script("Hans", 501, "Han (Simplified variant)");
        public static readonly IScript HanTraditional = new Script("Hant", 502, "Han (Traditional variant)");
        public static readonly IScript Hebrew = new Script("Hebr", 125, "Hebrew");
        public static readonly IScript Hiragana = new Script("Hira", 410, "Hiragana");
        public static readonly IScript PahawhHmong = new Script("Hmng", 450, "Pahawh Hmong");
        public static readonly IScript HiraganaKatakana = new Script("Hrkt", 412, "(alias for Hiragana + Katakana)");
        public static readonly IScript OldHungarian = new Script("Hung", 176, "Old Hungarian");
        public static readonly IScript Indus = new Script("Inds", 610, "Indus (Harappan)");
        public static readonly IScript OldItalic = new Script("Ital", 210, "Old Italic (Etruscan, Oscan, etc.)");
        public static readonly IScript Java = new Script("Java", 361, "Javanese");
        public static readonly IScript Japanese = new Script("Jpan", 413, "Japanese (alias for Han + Hiragana + Katakana)");
        public static readonly IScript Jurchen = new Script("Jurc", 510, "Jurchen");
        public static readonly IScript KayahLi = new Script("Kali", 357, "Kayah Li");
        public static readonly IScript Katakana = new Script("Kana", 411, "Katakana");
        public static readonly IScript Kharishthi = new Script("Khar", 305, "Kharoshthi");
        public static readonly IScript Khmer = new Script("Khmr", 355, "Khmer");
        public static readonly IScript Kannada = new Script("Knda", 345, "Kannada");
        public static readonly IScript Korean = new Script("Kore", 287, "Korean (alias for Hangul + Han)");
        public static readonly IScript Kpelle = new Script("Kpel", 436, "Kpelle");
        public static readonly IScript Kaithi = new Script("Kthi", 317, "Kaithi");
        public static readonly IScript TaiTham = new Script("Lana", 351, "Tai Tham (Lanna)");
        public static readonly IScript Lao = new Script("Laoo", 356, "Lao");
        public static readonly IScript LatinFraktur = new Script("Latf", 217, "Latin (Fraktur variant)");
        public static readonly IScript LatinGaelic = new Script("Latg", 216, "Latin (Gaelic variant)");
        public static readonly IScript Latin = new Script("Latn", 215, "Latin");
        public static readonly IScript Lepcha = new Script("Lepc", 335, "Lepcha (Róng)");
        public static readonly IScript Limbu = new Script("Limb", 336, "Limbu");
        public static readonly IScript LinearA = new Script("Lina", 400, "Linear A");
        public static readonly IScript LinearB = new Script("Linb", 401, "Linear B");
        public static readonly IScript Lisu = new Script("Lisu", 399, "Lisu (Fraser)");
        public static readonly IScript Loma = new Script("Loma", 437, "Loma");
        public static readonly IScript Lycian = new Script("Lyci", 202, "Lycian");
        public static readonly IScript Lydian = new Script("Lydi", 116, "Lydian");
        public static readonly IScript Mandaic = new Script("Mand", 140, "Mandaic, Mandaean");
        public static readonly IScript Manichaean = new Script("Mani", 139, "Manichaean");
        public static readonly IScript MyanHieroglyphs = new Script("Maya", 90, "Mayan hieroglyphs");
        public static readonly IScript Mende = new Script("Mend", 438, "Mende script");
        public static readonly IScript MeroiticCursive = new Script("Merc", 101, "Meroitic Cursive");
        public static readonly IScript MeroiticHieroglyphs = new Script("Mero", 100, "Meroitic Hieroglyphs");
        public static readonly IScript Malayalam = new Script("Mlym", 347, "Malayalam");
        public static readonly IScript Mongolian = new Script("Mong", 145, "Mongolian");
        public static readonly IScript Moon = new Script("Moon", 218, "Moon (Moon code, Moon script, Moon type)");
        public static readonly IScript Mro = new Script("Mroo", 199, "Mro");
        public static readonly IScript MeiteiMayek = new Script("Mtei", 337, "Meitei Mayek (Meithei, Meetei)");
        public static readonly IScript Myanmar = new Script("Mymr", 350, "Myanmar (Burmese)");
        public static readonly IScript OldNorthArabian = new Script("Narb", 106, "Old North Arabian (Ancient North Arabian)");
        public static readonly IScript Nabataean = new Script("Nbat", 159, "Nabataean");
        public static readonly IScript NakhiGeba = new Script("Nkgb", 420, "Nakhi Geba ('Na-'Khi ²Ggo-¹baw, Naxi Geba)");
        public static readonly IScript NKo = new Script("Nkoo", 165, "N’Ko");
        public static readonly IScript Nushu = new Script("Nshu", 499, "Nüshu");
        public static readonly IScript Ogham = new Script("Ogam", 212, "Ogham");
        public static readonly IScript OlChiki = new Script("Olck", 261, "Ol Chiki (Ol Cemet’, Ol, Santali)");
        public static readonly IScript OldTurkic = new Script("Orkh", 175, "Old Turkic, Orkhon Runic");
        public static readonly IScript Oriya = new Script("Orya", 327, "Oriya");
        public static readonly IScript Osmanya = new Script("Osma", 260, "Osmanya");
        public static readonly IScript Palmyrene = new Script("Palm", 126, "Palmyrene");
        public static readonly IScript OldPermic = new Script("Perm", 227, "Old Permic");
        public static readonly IScript PhagsPa = new Script("Phag", 331, "Phags-pa");
        public static readonly IScript InscriptionalPahlavi = new Script("Phli", 131, "Inscriptional Pahlavi");
        public static readonly IScript PsalterPahlavi = new Script("Phlp", 132, "Psalter Pahlavi");
        public static readonly IScript BookPahlavi = new Script("Phlv", 133, "Book Pahlavi");
        public static readonly IScript Phoenician = new Script("Phnx", 115, "Phoenician");
        public static readonly IScript Miao = new Script("Plrd", 282, "Miao (Pollard)");
        public static readonly IScript InscriptionalParthian = new Script("Prti", 130, "Inscriptional Parthian");
        public static readonly IScript Rejang = new Script("Rjng", 363, "Rejang (Redjang, Kaganga)");
        public static readonly IScript Rongorongo = new Script("Roro", 620, "Rongorongo");
        public static readonly IScript Runic = new Script("Runr", 211, "Runic");
        public static readonly IScript Samaritan = new Script("Samr", 123, "Samaritan");
        public static readonly IScript Sarati = new Script("Sara", 292, "Sarati");
        public static readonly IScript OldSouthArabian = new Script("Sarb", 105, "Old South Arabian");
        public static readonly IScript Saurashtra = new Script("Saur", 344, "Saurashtra");
        public static readonly IScript SignWriting = new Script("Sgnw", 95, "SignWriting");
        public static readonly IScript Shavian = new Script("Shaw", 281, "Shavian (Shaw)");
        public static readonly IScript Sharada = new Script("Shrd", 319, "Sharada");
        public static readonly IScript Sindhi = new Script("Sind", 318, "Sindhi, Khudawadi");
        public static readonly IScript Sinhala = new Script("Sinh", 348, "Sinhala");
        public static readonly IScript SoraSompeng = new Script("Sora", 398, "Sora Sompeng");
        public static readonly IScript Sudanese = new Script("Sund", 362, "Sundanese");
        public static readonly IScript Syloti = new Script("Sylo", 316, "Syloti Nagri");
        public static readonly IScript Syriac = new Script("Syrc", 135, "Syriac");
        public static readonly IScript EstrangeloSyriac = new Script("Syre", 138, "Syriac (Estrangelo variant)");
        public static readonly IScript WesternSyriac = new Script("Syrj", 137, "Syriac (Western variant)");
        public static readonly IScript EasternSyriac = new Script("Syrn", 136, "Syriac (Eastern variant)");
        public static readonly IScript Tagbanwa = new Script("Tagb", 373, "Tagbanwa");
        public static readonly IScript Takri = new Script("Takr", 321, "Takri");
        public static readonly IScript TaiLe = new Script("Tale", 353, "Tai Le");
        public static readonly IScript NewTaiLue = new Script("Talu", 354, "New Tai Lue");
        public static readonly IScript Tamil = new Script("Taml", 346, "Tamil");
        public static readonly IScript Tangut = new Script("Tang",	520, "Tangut");
        public static readonly IScript TaiViet = new Script("Tavt", 359, "Tai Viet");
        public static readonly IScript Telugu = new Script("Telu", 340, "Telugu");
        public static readonly IScript Tengwar = new Script("Teng", 290, "Tengwar");
        public static readonly IScript Tifinagh = new Script("Tfng", 120, "Tifinagh (Berber)");
        public static readonly IScript Tagalog = new Script("Tglg", 370, "Tagalog (Baybayin, Alibata)");
        public static readonly IScript Thaana = new Script("Thaa", 170, "Thaana");
        public static readonly IScript Thai = new Script("Thai", 352, "Thai");
        public static readonly IScript Tibetan = new Script("Tibt", 330, "Tibetan");
        public static readonly IScript Ugaritic = new Script("Ugar", 40, "Ugaritic");
        public static readonly IScript Vai = new Script("Vaii", 470, "Vai");
        public static readonly IScript VisibleSpeech = new Script("Visp", 280, "Visible Speech");
        public static readonly IScript WarangCiti = new Script("Wara", 262, "Warang Citi (Varang Kshiti)");
        public static readonly IScript Woleai = new Script("Wole", 480, "Woleai");
        public static readonly IScript OldPersian = new Script("Xpeo", 30, "Old Persian");
        public static readonly IScript Cuneiform = new Script("Xsux", 20, "Cuneiform, Sumero-Akkadian");
        public static readonly IScript Yi = new Script("Yiii", 460, "Yi");
        public static readonly IScript Inherited = new Script("Zinh", 994, "Code for inherited script");
        public static readonly IScript MathematicalNotation = new Script("Zmth", 995, "Mathematical notation");
        public static readonly IScript Symbols = new Script("Zsym", 996, "Symbols");
        public static readonly IScript Unwritten = new Script("Zxxx", 997, "Code for unwritten documents");
        public static readonly IScript Undetermined = new Script("Zyyy", 998, "Code for undetermined script");
        public static readonly IScript Uncoded = new Script("Zzzz", 999, "Code for uncoded script");

        #endregion
    }
}
