using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class CharacterSet
        : ICharacterSet
    {
        public CharacterSet(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        private readonly string name;
        private readonly string description;

        #region ICharacterSet Members

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        static CharacterSet()
        {
            InitializeCharacterSets();

            foreach (var characterSet in characterSets)
                byName.Add(characterSet.Name, characterSet);
        }

        private static void InitializeCharacterSets()
        {
            characterSets.Add(Ascii);
            characterSets.Add(Utf7);
            characterSets.Add(Utf8);
            characterSets.Add(Utf16);
            characterSets.Add(Utf16Be);
            characterSets.Add(Utf32);
            characterSets.Add(Unknown);
        }

        private static readonly IList<ICharacterSet> characterSets = new List<ICharacterSet>();
        private static readonly IDictionary<string, ICharacterSet> byName = new Dictionary<string, ICharacterSet>();

        #region Public Static Methods

        public static ICharacterSet Parse(string name)
        {
            return byName.ContainsKey(name) ? byName[name] : Unknown;
        }

        public static ICharacterSet GetCharacterSet(Encoding encoding)
        {
            if (encoding == Encoding.ASCII)
                return Ascii;
            else if (encoding == Encoding.UTF7)
                return Utf7;
            else if (encoding == Encoding.UTF8)
                return Utf8;
            else if (encoding == Encoding.Unicode)
                return Utf16;
            else if (encoding == Encoding.BigEndianUnicode)
                return Utf16Be;
            else if (encoding == Encoding.UTF32)
                return Utf32;

            return Unknown;
        }

        public static IEnumerable<ICharacterSet> GetCharacterSets()
        {
            return characterSets;
        }

        #endregion

        #region Character Sets

        public static readonly ICharacterSet Ascii = new CharacterSet("US-ASCII", "ANSI X3.4-1968");
        public static readonly ICharacterSet Utf7 = new CharacterSet("UTF-7", "Unicode UTF-7: RFC 2152");
        public static readonly ICharacterSet Utf8 = new CharacterSet("UTF-8", "Unicode UTF-8: RFC 3629");
        public static readonly ICharacterSet Utf16 = new CharacterSet("UTF-16", "Unicode UTF-16: Little Endian RFC 2781");
        public static readonly ICharacterSet Utf16Be = new CharacterSet("UTF-16BE", "Unicode UTF-16: Big Endian RFC 2781");
        public static readonly ICharacterSet Utf32 = new CharacterSet("UTF-32", "Unicode UTF-32: Little Endian");
        public static readonly ICharacterSet Unknown = new CharacterSet("UNKNOWN", "Unknown Character Set");

        #endregion
    }
}
